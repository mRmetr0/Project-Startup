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

    private FileInfo info;
    
    private List<string> lines;
    private List<GameObject> prefabs;
    
    private Slider currentSlider;
    private int currentGoal = -1;
    private int newValue = 0;
    private int oldValue = 0;

    private void Awake()
    {
        goalInspector = this;
    }

    private void OnDestroy()
    {
        if (info != null && lines.Count>0)
        {
            FileManager.fileManager.SetFile(info, lines);
        }
    }

    public void SetScene(FileInfo _info)
    {
        info = _info;
        canvas = FindObjectOfType<Canvas>();
        lines = FileManager.fileManager.GetFile(_info);
        prefabs = new List<GameObject>();
        title.text = lines[0];
        for (int i = 4; i < lines.Count-1; i += 2)
        {
            GameObject prefab = Instantiate(subGoalPrefab, canvas.transform);
            prefabs.Add(prefab);
            prefab.transform.position += new Vector3(0, 500 - (i*125), 0);
            prefab.GetComponentInChildren<TMP_Text>().text = lines[i];
            Slider slider = prefab.GetComponentInChildren<Slider>();
            slider.value = int.Parse(lines[i + 1]);
        }

        for (int i = 0; i < prefabs.Count-1; i++)
        {
            GameObject prefab = prefabs[i];
            Slider slider = prefab.GetComponentInChildren<Slider>();
            
            if (currentGoal == -1 && slider.value < 100)
            {
                slider.interactable = true;
                currentGoal = i;
                currentSlider = slider;
                oldValue = newValue = (int)slider.value;
            }
            else
            {
                slider.interactable = false; 
                if (slider.value == 100)
                {
                    slider.GetComponentInChildren<Image>().color = Color.yellow;
                    return;
                }
                slider.GetComponentInChildren<Image>().color = Color.gray;
            }
        }

    }

    private void Update()
    {
        newValue = (int)currentSlider.value;
        if (Input.GetMouseButtonUp(0) && newValue != oldValue)
        {
            lines[currentGoal + 1] = ((int)currentSlider.value).ToString();
            oldValue = newValue;
        }
    }
}
