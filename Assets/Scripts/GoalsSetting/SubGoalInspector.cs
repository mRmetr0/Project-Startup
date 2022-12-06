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
    private List<string> lines;
    
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text title;
    [SerializeField] private Button b;

    private int newValue;
    private int oldValue;

    private int progress;

    private void Awake()
    {
        subGoalInspector = this;
        //slider = FindObjectOfType<Slider>();
        //title = FindObjectOfType<TMP_Text>();
        b.onClick.AddListener(OnBackButtonClick);
    }

    private void OnBackButtonClick()
    {
        FileManager.fileManager.SetFile(info, lines);
        AssetDatabase.Refresh();
        GoToDay g = gameObject.AddComponent<GoToDay>();
        g.GoToGoalInspector(info);
    }

    public void SetScene(FileInfo _info, int _progress)
    {
        info = _info;
        progress = _progress;
        lines = FileManager.fileManager.GetFile(_info);
        title.text = lines[_progress];
        slider.value = int.Parse(lines[_progress + 1]);
        newValue = oldValue = (int)slider.value;
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
