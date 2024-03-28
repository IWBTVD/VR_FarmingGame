using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
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
    public string[] EventName;
    public string[] Dialog;
    void Start()
    {
        //첫줄은 헤더이므로 제외하고 데이터만 가져옵니다.
        string[] data = dialogCSV.text.Split(new char[] { '\n' });

        for (int i = 0; i < data.Length; i++)
        {
            EventName = new string[data.Length];
            Dialog = new string[data.Length];

            string[] row = data[i].Split(new char[] { ',' });
            EventName[i] = row[0];
            Dialog[i] = row[1];
            Debug.Log(EventName[i] + " : " + Dialog[i]);
        }

    }

    public string GetDialog(string eventName)
    {
        for (int i = 0; i < EventName.Length; i++)
        {
            if (eventName.Equals(this.EventName[i]))
            {
                return Dialog[i];
            }
        }

        // Add a default return statement
        return null;
    }



}
