using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToDay : MonoBehaviour
{
    private enum Type  {
        Day,
        Goal,
        SubGoal,
        Empty
    }

    private Type type = Type.Empty;
    

    private List<string> _day;
    private FileInfo info;
    private int progress;
    void Awake()
    {
        this.transform.parent = null;
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += this.SetScene;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= this.SetScene;
    }

    void SetScene(Scene scene, LoadSceneMode sceneMode)
    {
        switch (type)
        {
            case Type.Day:
                RevisitDays revisit = FindObjectOfType<RevisitDays>();
                revisit.SetScene(_day[0], float.Parse(_day[1]), _day[2]);
                break;
            case Type.Goal:
                GoalInspector.goalInspector.SetScene(info);
                break;
            case Type.SubGoal:
                SubGoalInspector.subGoalInspector.SetScene(info, progress);
                break;

        }
        Destroy(gameObject);

    }

    public void GoToJournalDay(string day)
    {
        _day = FileManager.fileManager.HasDay(day);
        if (_day != null)
        {
            type = Type.Day;   
            FileManager.fileManager.GoToScene("Status");
        }
        else
        {
            Debug.Log("Day could not be found.");
        }
    }

    public void GoToGoalInspector(FileInfo _info)
    {
        info = _info;
        type = Type.Goal;
        FileManager.fileManager.GoToScene("GoalInspectorScene");
    }

    public void GoToSubGoalInspector(FileInfo _info, int _progress)
    {
        progress = _progress;
        info = _info;
        type = Type.SubGoal;
        FileManager.fileManager.GoToScene("SubGoalInspector");

    }

}
