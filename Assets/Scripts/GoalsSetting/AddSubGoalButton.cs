using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddSubGoalButton : MonoBehaviour
{
    private Button b;
    private TMP_Text title;

    private string _name;

    public string name
    {
        get { return _name;}
        set { value = _name; }
    }
    public Button button { get { return b; } }

    private void Awake()
    {
        b = GetComponent<Button>();
        b.onClick.AddListener(OnButtonPress);
        title = GetComponentInChildren<TMP_Text>();
        _name = title.text;
    }

    private void OnEnable()
    {
        Debug.Log("before"+title.text);
        title.text = _name;
        Debug.Log("after"+title.text);
    }

    private void OnButtonPress()
    {
        QuestionInput input = FindObjectOfType<QuestionInput>();
        input.ButtonWriteSubGoal(_name);
    }
}
