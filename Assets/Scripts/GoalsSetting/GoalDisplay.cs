using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
    private TMP_Text _progressText;
    public string progress
    {
        get { return _progressText.text; }
        set { _progressText.text = value.ToString(); }
    }

    [SerializeField]
    private Button button;

    [SerializeField] private Image[] images;

    [SerializeField] private Image potImage;
    
    
    [SerializeField] private string goToScene;

    void Awake()
    {
        button.GetComponent<Button>();
        button.onClick.AddListener(OnButtonPress);
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

    public void setDisplay(int percent, FileInfo _info)
    {
        info = _info;
        List<string> lines = FileManager.fileManager.GetFile(info);
        _title.text = lines[0];
        images[0].gameObject.SetActive(false);
        potImage.gameObject.SetActive(true);
        if (percent < 33)
        {
            images[1].gameObject.SetActive(true);
        } 
        else if (percent == 100)
        {
            images[3].gameObject.SetActive(true);
        }
        else
        {
            images[2].gameObject.SetActive(true);
        }
    }
}
