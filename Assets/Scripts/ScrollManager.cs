using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollManager : MonoBehaviour
{
    private ScrollView scrollView;
    private bool dragging = false;
    private float mouseYonDragStart = 0;

    public void Start()
    {      
        scrollView.RegisterCallback<MouseDownEvent>(OnMouseDown, TrickleDown.TrickleDown);
        scrollView.RegisterCallback<MouseUpEvent>(OnMouseUp, TrickleDown.TrickleDown);
        scrollView.RegisterCallback<MouseMoveEvent>(OnMouseMove, TrickleDown.TrickleDown);
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {
        if (dragging)
        {
            scrollView.scrollOffset = new Vector2(0, mouseYonDragStart - evt.localMousePosition.y);
        }
    }

    private void OnMouseUp(MouseUpEvent evt)
    {
        dragging = false;

    }

    private void OnMouseDown(MouseDownEvent evt)
    {
        dragging = true;
        mouseYonDragStart = evt.localMousePosition.y;
    }

}
