using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SubGoalProgress : MonoBehaviour
{
    private Button b;
    
    // Start is called before the first frame update
    void Start()
    {
        b = GetComponentInChildren<Button>();
        b.onClick.AddListener(InspectSubGoal);
    }

    void InspectSubGoal()
    {
        GoToDay g = gameObject.AddComponent<GoToDay>();
        g.GoToSubGoalInspector(GoalInspector.goalInspector.info, GoalInspector.goalInspector.currentGoal);
    }
}
