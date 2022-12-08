using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SubGoalInspector : MonoBehaviour
{
    public static SubGoalInspector subGoalInspector;

    private FileInfo info;
    private FileInfo subGoalInfo;
    private List<string> lines;
    private List<string> subGoals;

    [SerializeField] private GameObject popUpPrefab;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Slider slider;
    [SerializeField] private Button b;
    
    private string subGoalList = "Assets/Resources/SubGoalsList.txt";
    
    private int oldValue;

    private int progress;

    private void Awake()
    {
        subGoalInspector = this;
        //slider = FindObjectOfType<Slider>();
        //title = FindObjectOfType<TMP_Text>();
        b.onClick.AddListener(OnBackButtonClick);
        subGoalInfo = new FileInfo(subGoalList);
        subGoals = FileManager.fileManager.GetFile(subGoalInfo);
    }
    
    private void OnDestroy()
    {
        if (info != null && lines.Count>0)
        {
            FileManager.fileManager.SetFile(info, lines);
        }
    }

    private void OnBackButtonClick()
    {
        FileManager.fileManager.SetFile(info, lines);
        AssetDatabase.Refresh();
        GoToDay g = gameObject.AddComponent<GoToDay>();
        if (slider.value == 100)
            Instantiate(popUpPrefab);
        g.GoToGoalInspector(info);
    }

    public void SetScene(FileInfo _info, int _progress)
    {
        info = _info;
        progress = _progress;
        lines = FileManager.fileManager.GetFile(_info);
        title.text = lines[_progress];
        slider.value = int.Parse(lines[_progress + 1]);
        oldValue = (int)slider.value;
        for (int i = 0; i < subGoals.Count-1; i++) {
            if (title.text == subGoals[i])
            {
                description.text = subGoals[i+1];
                return;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && (int)slider.value != oldValue)
        {
            lines[progress + 1] = ((int)slider.value).ToString();
            oldValue = (int)slider.value;
            Debug.Log(slider.value);
        }
    }
}
