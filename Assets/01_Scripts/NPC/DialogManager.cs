using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviourPun
{
    private static DialogManager instance;
    public static DialogManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "DialogManager";
                    instance = obj.AddComponent<DialogManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //store csv file using public
    public TextAsset dialogCSV;
    // Start is called before the first frame update
    public Dictionary<string, string> DialogDictionary;
    void Start()
    {
        DialogDictionary = new Dictionary<string, string>();
        //첫줄은 헤더이므로 제외하고 데이터만 가져옵니다.
        string[] data = dialogCSV.text.Split(new char[] { '\n' });

        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == "")
            {
                continue;
            }
            string[] row = data[i].Split(new char[] { ',' });
            DialogDictionary.Add(row[0], row[1]);
        }

    }
    // 대사가 많아지면 미리 분리해놓고 사용하기도 고려.
    public string GetDialog(string eventName)
    {
        // If the dictionary contains the key, return the value
        if (DialogDictionary.ContainsKey(eventName))
        {
            return DialogDictionary[eventName];
        }
        // Add a default return statement
        return null;
    }

}
