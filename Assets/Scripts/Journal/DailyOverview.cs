using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class DailyOverview : MonoBehaviour
{
    [SerializeField]
    private int offset;
    [SerializeField]
    private JournalUnit unitPrefab;
    [SerializeField]
    private Canvas canvas;
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        SetupUnits();
        SetUpWeekOverview();
    }

    void SetupUnits()
    {
        List<string> lines = FileManager.fileManager.GetFile();
        lines.Reverse();
        for (int i = 0; i < lines.Count/3; i++)
        {
            JournalUnit unit = Instantiate(unitPrefab, canvas.transform);
            unit.SetUnit(lines[2+3*i], float.Parse(lines[1+i*3]));
            unit.transform.position += new Vector3(0, -i*offset, 0);
        }
    }

    void SetUpWeekOverview()
    {
        List<string> lines = FileManager.fileManager.GetFile();
        List<float> values = new List<float>();
        for (int i = 1; i < (21 > lines.Count/3 ? 21 : lines.Count/3);i+=3)
        {
            values.Add(float.Parse(lines[i]));
            //Debug.Log(lines[i]);
        }
    }
}
