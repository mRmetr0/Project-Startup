using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalDisplay : MonoBehaviour
{
    private FileInfo _info = null;
    public FileInfo info
    {
        set { _info = value; }
        get { return _info; }
    }

    [SerializeField]
    private TMP_Text _title;
    public string title
    {
        get { return _title.text; }
        set { _title.text = value; }
    }

    [SerializeField]
    private TMP_Text _progress;
    public string progress
    {
        get { return _progress.text; }
        set { _progress.text = value.ToString(); }
    }

    [SerializeField]
    private Button button;

    [SerializeField] private string goToScene;

    void Start()
    {
        button.onClick.AddListener(OnButtonPress);
        /*GoalManager manager = FindObjectOfType<GoalManager>();
        manager.AddDisplay(this);*/
    }

    public void OnButtonPress()
    {
        if (info != null)
        {
            GoToDay inspector = gameObject.AddComponent<GoToDay>();
            inspector.GoToGoalInspector(_info);
        } else {
            FileManager.fileManager.GoToScene(goToScene);
        }
    }

    public string GetData()
    {
        return title + " & " +progress;
    }
}
