using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour
{
    [SerializeField]
    private List<GoalDisplay> displays;
    private DirectoryInfo info;

    private void Awake()
    { 
        info = new DirectoryInfo("Assets/Resources/GoalFiles/");
        //displays = new List<GoalDisplay>();
        //displays.Reverse();
    }

    private void Start()
    {
        FileInfo[] files = info.GetFiles();
        for (int i = 0; i < files.Length/2; i++) {
            AssignDisplay(files[i*2], displays[i]);
        }

        foreach (GoalDisplay display in displays)
        {
            if (display.info == null)
            {
                Destroy(display.gameObject);
            }
        }
    }

    private void AssignDisplay(FileInfo file, GoalDisplay display)
    {
        List<string> lines = new List<string>();
        StreamReader reader = file.OpenText();
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            lines.Add(line);
        }
        reader.Close();
        display.title = lines[0];
        float currentProgress = 0;
        float maxProgress = 0;
        for (int i = lines.Count-1; i >= 4; i-=2)
        {
            currentProgress += float.Parse(lines[i]);
            maxProgress += 100;
        }
        display.progress = "Progress: "+(int)((currentProgress/maxProgress)*100) + "%";
        display.info = file;
        Destroy(gameObject);
    }

    public void AddDisplay(GoalDisplay display)
    {
        displays.Add(display);
    }
}
