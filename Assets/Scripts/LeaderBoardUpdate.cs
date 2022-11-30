using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class LeaderBoardUpdate : MonoBehaviour
{
    private FileInfo sourceFile = null;
    private StreamReader reader = null;
    private StreamWriter writer = null;

    private string path = "Assets/Scripts/Leaderboard.txt";
    private string text = "";

    void Start()
    {
        sourceFile = new FileInfo(path);

        ReadFile();
            WriteFile();

    }

    void ReadFile()
    {
        reader = sourceFile.OpenText();
        while (text != null)
        {
            text = reader.ReadLine();
            Debug.Log(text);
        }
        reader.Close();
    }

    void WriteFile()
    {
        writer = new StreamWriter(path, false);
        writer.Write("a\nb\nc");
        writer.Close();
    }
}
