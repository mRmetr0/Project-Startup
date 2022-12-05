using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GoalInspector : MonoBehaviour
{
    public static GoalInspector goalInspector;

    private Canvas canvas;     
    
    [SerializeField] private GameObject subGoalPrefab;
    [SerializeField] private TMP_Text title;

    private void Awake()
    {
        goalInspector = this;
    }

    public void SetScene(FileInfo info)
    {
        canvas = FindObjectOfType<Canvas>();
        List<string> lines = FileManager.fileManager.GetFile(info);
        title.text = lines[0];
        for (int i = 4; i < lines.Count-1; i += 2)
        {
            GameObject prefab = Instantiate(subGoalPrefab, canvas.transform);
            //prefab.
            prefab.transform.position += new Vector3(0, 500 - (i*125), 0);
            prefab.GetComponentInChildren<TMP_Text>().text = lines[i];
            prefab.GetComponentInChildren<Slider>().value = int.Parse(lines[i + 1]);
            Debug.Log("Slider.value: "+prefab.GetComponentInChildren<Slider>().value);
        }
    }
}
