using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 
using System.IO;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEditor.VersionControl;
using UnityEngine.SceneManagement;

public class QuestionInput : MonoBehaviour
{
    private DirectoryInfo info;

    [SerializeField] private Button[] nextButtons;
    [SerializeField] private TMP_InputField[] answereFields;
    [SerializeField] private Button addButton;
    [SerializeField] private Button addSubGoalButton;

    private List<string> lines;
    
    [SerializeField] private Slider[] sliders;
    
    private int oldValue;
    private int newValue;

    private string text = null;
    private string path = "Assets/Resources/GoalFiles/";
    private void Awake()
    {
        lines = new List<string>();
        info = new DirectoryInfo(path);
        
        foreach (Button b in nextButtons)
        {
            b.onClick.AddListener(OnNextButtonPress);
        }
        foreach (TMP_InputField field in answereFields)
        {
            field.onEndEdit.AddListener(UpdateTextField);
        }
        addButton.onClick.AddListener(OnAddGoalPress);
        addSubGoalButton.onClick.AddListener(OnBackButtonPress);
        
        newValue = 0;
        oldValue = newValue;
    }

    private void Update()
    {
        foreach (Slider slider in sliders)
        {
            if (slider.isActiveAndEnabled)
            {
                newValue = (int)slider.value;
                if (Input.GetMouseButtonUp(0) && oldValue != newValue)
                {
                    text = slider.value.ToString();
                    oldValue = newValue;
                }
            }
        }
    }
    
    public void UpdateTextField(string arg)
    {
        if (arg != null)
        {
            text = arg;
        }
    }

    public void ButtonWriteSituation(string input)
    {
        lines.Add(input);
        foreach (Slider slider in sliders)
        {
            slider.value = 0;
            newValue  = 0;
        }
        GoalSetter.goalSetter.NextScreen();
    }

    public void ButtonWriteSubGoal(string arg)
    {
        lines.Add(arg +"\n0");
        text = null;
        GoalSetter.goalSetter.NextScreen();
    }

    public void OnNextButtonPress()
    {
        if (text != null)
        {
            lines.Add(text);
            text = null;
            GoalSetter.goalSetter.NextScreen();
        }
    }

    public void OnBackButtonPress()
    {
            text = null;
            GoalSetter.goalSetter.PreviousScreen();
        
    }

    public void OnAddGoalPress()
    {
            CreateGoal();
            SceneManager.LoadScene("GoalMenuScene");
        
    }

    public void CreateGoal()
    {
        if (lines.Count <=0)
        {
            Debug.Log("No info to put in the file");
            return;
        }
        string content = "";
        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line)&& !string.IsNullOrEmpty((line)))
            {
                //Debug.Log(line+"is not null, white space or empty");
                content += line + "\n";
            }
        //Debug.Log(content);
        }

        
        string fileName = "Goal"+(info.GetFiles().Length/2)+".txt";
        File.WriteAllText(path +fileName, content);
        AssetDatabase.Refresh();
    }
}
