using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using TMPro;
using UnityEngine.UI;

public class JournalUnit : MonoBehaviour
{
    private TMP_Text title;
    private Slider slider;
    private Button button;
    private void Awake()
    {
        title = GetComponentInChildren<TMP_Text>();
        slider = GetComponentInChildren<Slider>();
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(OnButtonPress);
    }

    private void OnButtonPress()
    {
        GoToDay goToDay = gameObject.AddComponent<GoToDay>();
        goToDay.transform.parent = null;
        goToDay.GoTo(title.text);
    }

    public void SetUnit(string _day, float _mood)
    {
        title.text = _day;
        slider.value = _mood;
    }


}
