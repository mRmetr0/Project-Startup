using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string goToScene;
    private void Start()
    {
        button.onClick.AddListener(OnButtonPress);
    }

    public void OnButtonPress()
    {
        SceneManager.LoadScene(goToScene);
    }
}
