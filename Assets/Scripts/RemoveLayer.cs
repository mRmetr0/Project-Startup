using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLayer : MonoBehaviour
{
    [SerializeField] GameObject fadeInLayer;
    public void deleteLayer()
    {
        fadeInLayer.active = false;
    }
}
