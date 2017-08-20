using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalFly : Enemy
{
    public Transform currentPoint;

    public Transform[] points;

    public int pointSelection;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        HP = 1;

        currentPoint = points[pointSelection];

        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (transform == null)
        {
            Destroy(this);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Time.deltaTime * Speed);

        //If we reach our target point 
        if (transform.position == currentPoint.position)
        {
            Flip();
            pointSelection++;

            if (pointSelection == points.Length)
            {
                pointSelection = 0;
            }

            currentPoint = points[pointSelection];
        }
    }

    private void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
