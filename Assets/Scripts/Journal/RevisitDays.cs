using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class RevisitDays : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text notes;

    public void SetScene(string _day, float _meter, string _notes)
    {
        Debug.Log("day: "+_day+" mood: "+_meter+"Notes: "+_notes);
        title.text = _day;
        slider.value = _meter;
        notes.text = _notes;
    }
}
