using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SubGoalProgress : MonoBehaviour
{
    private Button b;
    
    // Start is called before the first frame update
    void Start()
    {
        b = GetComponentInChildren<Button>();
        b.onClick.AddListener(InspectSubGoal);
        SceneManager.sceneLoaded += DestroySelf;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= DestroySelf;
    }

    void DestroySelf(Scene scene, LoadSceneMode sceneMode)
    {
        Destroy(gameObject);
    }

    void InspectSubGoal()
    {
        GoToDay g = gameObject.AddComponent<GoToDay>();
        g.GoToSubGoalInspector(GoalInspector.goalInspector.info, GoalInspector.goalInspector.currentGoal);
    }
}
