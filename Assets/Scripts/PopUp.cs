using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PopUp : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += this.Appear;
        for (int i = 0; i < transform.childCount-1; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= this.Appear;
    }

    void Appear(Scene scene, LoadSceneMode sceneMode)
    {
        this.transform.parent = FindObjectOfType<Canvas>().transform;
        for (int i = 0; i < transform.childCount-1; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        this.transform.position = this.transform.parent.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }
    }
}
