using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class DailyStatus : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private TMP_InputField input;

    private string currentDayS;
    private string notes = ".";
    
    private float mood = 0;
    private float oldMood = 0;
    void Awake()
    {
        DateTime currentDay = DateTime.Now;
        currentDayS = currentDay.Day + "/" + currentDay.Month + "/" + currentDay.Year;
        mood = slider.value;
        oldMood = mood;
    }

    private void Start()
    {
        input = GameObject.FindObjectOfType<TMP_InputField>();
        Debug.Log(input);
        input.onEndEdit.AddListener(DoSomething);
    }

    private void DoSomething(string arg)
    {
        notes = arg;
        if (notes == "")
            notes = ".";
        Debug.Log(notes);
        UpdateJournal();
    }

    public void UpdateJournal()
    {
        List<string> lines = FileManager.fileManager.GetFile();
        if (!FileManager.fileManager.SameDay())
        {
            Debug.Log("Add new day");
            lines.Add(currentDayS);
            lines.Add(mood.ToString());
            lines.Add(notes);
        }
        else
        {
            Debug.Log("Update Last Day");
            lines[lines.Count - 2] = mood.ToString();
            lines[lines.Count - 1] = notes;
        }

        foreach (string s in lines) {
            //Debug.Log(s);
        }

        FileManager.fileManager.SetFile(lines);
    }

    private void Update()
    {
        mood = slider.value;

        if (Input.GetMouseButtonUp(0))
        {
            if (oldMood != mood)
            {
                UpdateJournal();
                oldMood = mood;
            }

        }
    }
}
