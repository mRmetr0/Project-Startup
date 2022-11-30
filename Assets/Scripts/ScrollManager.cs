using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollManager : MonoBehaviour
{
    private ScrollView _scrollView;
    private bool dragging = false;
    private float mouseYOnDragStart = 0;
    
    void Start()
    {
        _scrollView = new ScrollView();
    _scrollView.RegisterCallback<MouseDownEvent>(OnMouseDown, TrickleDown.TrickleDown);  
    _scrollView.RegisterCallback<MouseUpEvent>(OnMouseUp, TrickleDown.TrickleDown);  
    _scrollView.RegisterCallback<MouseMoveEvent>(OnMouseMove, TrickleDown.TrickleDown);    
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {
        if (dragging)
            _scrollView.scrollOffset = new Vector2(0, mouseYOnDragStart-evt.localMousePosition.y);
    }
    private void OnMouseUp(MouseUpEvent evt)
    {
        dragging = false;
    }
    private void OnMouseDown(MouseDownEvent evt)
    {
        dragging = true;
        mouseYOnDragStart = evt.localMousePosition.y;
    }

}
