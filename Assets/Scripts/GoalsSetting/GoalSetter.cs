using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalSetter : MonoBehaviour
{
    public static GoalSetter goalSetter;
    
    private Canvas canvas;
    [SerializeField] private List<GameObject> screens;
    private int progress = 0;
    private void Start()
    {
        for (int i = screens.Count-1; i >=0; i--)
        {
            GameObject screen = screens[i];
            screen.SetActive(i == 0);
        }
        goalSetter = this;
        canvas = GetComponent<Canvas>();
    }

    public void NextScreen()
    {
        if (progress+2 <= screens.Count)
        {
            screens[progress].SetActive(false);
            screens[progress + 1].SetActive(true);
            progress++;
        }
    }

    public void PreviousScreen()
    {
        if (progress > 0)
        {
            screens[progress].SetActive(false);
            screens[progress - 1].SetActive(true);
            progress--;
        }
    }
}
