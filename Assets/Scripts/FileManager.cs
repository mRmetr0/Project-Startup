using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FileManager : MonoBehaviour
{
    public static FileManager fileManager;
    
    private FileInfo _sourceFile = null;
    private StreamReader _reader = null;
    private StreamWriter _writer = null;
    
    readonly string path = "Assets/Resources/HappyRecords.txt";
    private string currentDayS;

    private void Awake()
    {
        if (fileManager == null)
        {
            fileManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        _sourceFile = new FileInfo(path);
        DateTime currentDay = DateTime.Now;
        currentDayS = currentDay.Day + "/" + currentDay.Month + "/" + currentDay.Year;
    }

    public bool SameDay()
    {
        List<string> lines = GetFile();
        return lines[lines.Count-3] == currentDayS;
    }

    public List<string> GetFile()
    {
        List<string> lines = new List<string>();
        _reader = _sourceFile.OpenText();
        while (!_reader.EndOfStream)
        {
            lines.Add(_reader.ReadLine());
        }
        _reader.Close();
        if (lines.Count<=0)
        {
            StreamWriter writer = new StreamWriter(path, false);
            writer.Write("-1\n-1");
            GetFile();
            writer.Close();
        }
        return lines;
    }

    public void SetFile(string text)
    {
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(text);
        writer.Close();
    }

    public void SetFile(List<string> lines)
    {
        string text = "";
        StreamWriter writer = new StreamWriter(path, false);
        foreach (string line in lines)
        {
            text += line + "\n";
        }
        writer.Write(text);
        writer.Close();
    }

    
    //CAN BE OPTIMIZED: by checking every day with a line instead of every single line;
    public List<string> hasDay(string day)
    {
        List<string> lines = GetFile();
        for (int i = lines.Count-1; i >= 0; i--)
        {
            if (lines[i] == day)
            {
                List<string> dayInfo = new List<string>();
                for (int j = 0; j < 3; j++) {
                    dayInfo.Add(lines [i+j]);
                }

                return dayInfo;
            }
        }

        Debug.Log("No day found.");
        return null;
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
