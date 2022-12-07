using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TerrainTools;
using UnityEngine.UI;

public class SituationButton : MonoBehaviour
{
    [SerializeField]
    private Button b;

    private string _name;

    public string name
    {
        set { value = _name;}
        get { return _name;}
    }
    public Button button { get { return b; } }

    private void Start()
    {
        b = GetComponent<Button>();
        b.onClick.AddListener(OnButtonPress);
        _name = b.GetComponentInChildren<TMP_Text>().text;
    }

    private void OnButtonPress()
    {
        QuestionInput input = FindObjectOfType<QuestionInput>();
        input.SituationButtonPress(this);
    }

}
