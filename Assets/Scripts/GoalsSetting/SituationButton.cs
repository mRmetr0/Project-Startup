using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SituationButton : MonoBehaviour
{
    [SerializeField]
    private Button b;

    private string _name;
    public string name { get { return _name;} }
    public Button button { get { return b; } }

    private void Start()
    {
        _name = b.GetComponentInChildren<TMP_Text>().text;
        b.onClick.AddListener(OnButtonPress);
    }

    private void OnButtonPress()
    {
        QuestionInput input = FindObjectOfType<QuestionInput>();
        input.ButtonWrite(_name);
    }

}
