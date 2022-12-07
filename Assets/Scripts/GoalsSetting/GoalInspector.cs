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

    private FileInfo _info;

    public FileInfo info
    {
        get { return _info; }
    }

    private List<string> lines;
    private List<GameObject> prefabs;
    
    private int _currentGoal = -1;

    public int currentGoal
    {
        get { return _currentGoal; }
    }

    private void Awake()
    {
        goalInspector = this;
    }

    private void OnDestroy()
    {
        if (_info != null && lines.Count>0)
        {
            FileManager.fileManager.SetFile(_info, lines);
        }
    }

    public void SetScene(FileInfo _info)
    {
        this._info = _info;
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

            if (currentGoal == -1 && slider.value < 100)
            {
                _currentGoal = i;
            }
            else
            {
                prefab.GetComponentInChildren<Button>().interactable = false;
                slider.interactable = false; 
                if (slider.value == 100)
                {
                    slider.GetComponentInChildren<Image>().color = Color.yellow;
                }
                else
                {
                    slider.GetComponentInChildren<Image>().color = Color.gray;
                }

            }
        }

    }
}
