using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startAnim : MonoBehaviour
{
    [SerializeField] Animation animation;
    [SerializeField] GameObject layer;
    [SerializeField] Button button;
    void Start()
    {
        button.onClick.AddListener(startAnimation);
    }

    private void startAnimation()
    {
        animation.Play();
    }

    public void disableLayer()
    {
        layer.active = false;
    }
    public void enableLayer()
    {
        layer.active = true;
    }
}
