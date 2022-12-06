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

    public enum Paths
    {
        Journal, 
        GoalSetting
    }
    public Paths paths;

    private string path;
    private string journalPath = "Assets/Resources/HappyRecords.txt";
    private string goalSettingPath = "Assets/Resources/Goals.txt";
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
            fileManager.SetPath(paths);
            Destroy(this);
        }


        SetPath(paths);
        //_sourceFile = new FileInfo(path);
        DateTime currentDay = DateTime.Now;
        currentDayS = currentDay.Day + "/" + currentDay.Month + "/" + currentDay.Year;
    }

    private void SetPath(Paths p)
    {
        switch (p)
        {
            case Paths.Journal:
                path = journalPath;
                break;
            case Paths.GoalSetting:
                path = goalSettingPath;
                break;
        }

        _sourceFile = new FileInfo(path);
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
            writer.Close();
        }
        return lines;
    }

    public List<string> GetFile(FileInfo _info)
    {
        List<string> lines = new List<string>();
        _reader = _info.OpenText();
        while (!_reader.EndOfStream)
        {
            lines.Add(_reader.ReadLine());
        }
        _reader.Close();
        if (lines.Count<=0)
        {
            StreamWriter writer = new StreamWriter(path, false);
            writer.Write("-1\n-1");
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
        for(int i = 0; i < lines.Count-2; i++)
        {
            text += lines[i] + "\n";
        }
        writer.Write(text);
        writer.Close();
    }

    public void SetFile(FileInfo _info, List<string> lines)
    {
        FileStream fs = _info.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
        StreamWriter writer = new StreamWriter(fs);
        fs.SetLength(0);
        string text = "";
        for (int i = 0; i < lines.Count; i++)
        {
            text += lines[i]+(i == lines.Count-1 ? "" : "\n");
        }
        writer.WriteLine(text);
        writer.Close();
        fs.Close();
    }


    //CAN BE OPTIMIZED: by checking every day with a line instead of every single line;
    public List<string> HasDay(string day)
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
