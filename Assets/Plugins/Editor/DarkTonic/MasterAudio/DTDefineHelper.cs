using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DarkTonic.MasterAudio.EditorScripts
{
    public static class DTDefineHelper
    {

        /// <summary>
        /// Checks if a symbol exists in the project's Scripting Define Symbols for the current build target.
        /// </summary>
        public static bool DoesScriptingDefineSymbolExist(string symbol)
        {
#if UNITY_2023_1_OR_NEWER
            var defines = PlayerSettings.GetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup)).Split(';');
#else
            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup).Split(';');
#endif
            for (int i = 0; i < defines.Length; i++)
            {
                if (string.Equals(symbol, defines[i].Trim())) return true;
            }
            return false;
        }

        public static HashSet<BuildTargetGroup> GetInstalledBuildTargetGroups()
        {
            var result = new HashSet<BuildTargetGroup>();
            foreach (BuildTarget target in (BuildTarget[])Enum.GetValues(typeof(BuildTarget)))
            {
                BuildTargetGroup group = BuildPipeline.GetBuildTargetGroup(target);
                if (BuildPipeline.IsBuildTargetSupported(group, target))
                {
                    result.Add(group);
                }
            }
            return result;
        }

        /// <summary>
        /// Try to add a symbol to the project's Scripting Define Symbols for all build targets.
        /// </summary>
        public static void TryAddScriptingDefineSymbols(string symbol, bool touchFiles = false)
        {
            foreach (var group in GetInstalledBuildTargetGroups())
            {
                try
                {
#if UNITY_2023_1_OR_NEWER
                    var defines = PlayerSettings.GetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup));
#else
                    var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
#endif
                    if (!string.IsNullOrEmpty(defines)) defines += ";";
                    defines += symbol;

#if UNITY_2023_1_OR_NEWER
                    PlayerSettings.SetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(group), defines);
#else
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(group, defines);
#endif
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            if (touchFiles) TouchScriptsWithScriptingSymbol(symbol);
            RecompileScripts();
        }

        /// <summary>
        /// Try to remove a symbol from the project's Scripting Define Symbols for all build targets.
        /// </summary>
        public static void TryRemoveScriptingDefineSymbols(string symbol)
        {
            foreach (var group in GetInstalledBuildTargetGroups())
            {
                try
                {
#if UNITY_2023_1_OR_NEWER
                    var symbols = new List<string>(PlayerSettings.GetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup)).Split(';'));
#else
                    var symbols = new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(group).Split(';'));
#endif
                    symbols.Remove(symbol);
                    var defines = string.Join(";", symbols.ToArray());

#if UNITY_2023_1_OR_NEWER
                    PlayerSettings.SetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(group), defines);
#else
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(group, defines);
#endif
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            RecompileScripts();
        }

        /// <summary>
        /// Add or remove a scripting define symbol.
        /// </summary>
        public static void ToggleScriptingDefineSymbol(string define, bool value, bool touchFiles = false)
        {
            if (value == true) TryAddScriptingDefineSymbols(define, touchFiles);
            else TryRemoveScriptingDefineSymbols(define);
        }

        /// <summary>
        /// Triggers a script recompile.
        /// </summary>
        public static void RecompileScripts()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#if UNITY_2019_3_OR_NEWER
        UnityEditor.EditorUtility.RequestScriptReload();
#else
            UnityEditorInternal.InternalEditorUtility.RequestScriptReload();
#endif
        }

        /// <summary>
        /// The only reliable way to force a recompile and get the editor to recognize
        /// MonoBehaviour scripts and wrappers in Plugins is to actually change those
        /// files. :/
        /// </summary>
        /// <param name="symbol">Touch files that cehck this scripting symbol.</param>
        public static void TouchScriptsWithScriptingSymbol(string symbol)
        {
            var path = Application.dataPath + "/Plugins/Pixel Crushers/";
            path = path.Replace("/", "\\");
            string[] filenames = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
            var found = string.Empty;
            var recompileAtText = "// Recompile at " + DateTime.Now + "\r\n";
            var searchString = "#if " + symbol;
            foreach (string filename in filenames)
            {
                var text = File.ReadAllText(filename);
                if (text.Contains(searchString))
                {
                    found += filename + "\n";
                    if (text.StartsWith("// Recompile at "))
                    {
                        var lines = File.ReadAllLines(filename);
                        lines[0] = recompileAtText;
                        File.WriteAllLines(filename, lines);
                    }
                    else
                    {
                        text = recompileAtText + text;
                        File.WriteAllText(filename, text);
                    }
                }
            }
            //Debug.Log("Touched: " + found);
        }
    }
}