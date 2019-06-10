using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

[RequireComponent(typeof(ITouchable))]
public class TouchController:MonoBehaviour
{
    private ITouchable touchme;

    private void Awake()
    {
        touchme = GetComponent<ITouchable>();
    }

    private void Update()
    {
        if (touchme == null)
            return;

        Vector2 touch = Vector2.one;
        bool touched = false;
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                touched = true;
                touch = Input.touches[0].position;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            touched = true;
            touch = Input.mousePosition;
        }
       
        if(touched)
        {
            var touchPoint = Camera.main.ScreenToWorldPoint(touch);
            if (touchme.Collider.OverlapPoint(touchPoint))
                touchme.Touched(touch);
        }
    }
}

