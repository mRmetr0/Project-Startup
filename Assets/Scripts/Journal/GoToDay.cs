using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToDay : MonoBehaviour
{
    private List<string> _day;
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
        
        RevisitDays revisit = FindObjectOfType<RevisitDays>();
        revisit.SetScene(_day[0], float.Parse(_day[1]), _day[2]);
        Destroy(this);
    }

    public void GoTo(string day)
    {
        _day = FileManager.fileManager.hasDay(day);
        if (_day != null)
        {
            FileManager.fileManager.GoToScene("statusScene");  
        }
        else
        {
            Debug.Log("Day could not be found.");
        }
    }

    
}
