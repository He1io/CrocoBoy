using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalFly : Enemy
{
    public Transform currentPoint;

    public Transform[] points;

    public int pointSelection;

    void Start()
    {
        HP = 1;

        currentPoint = points[pointSelection];
    }

    void Update()
    {
        if (transform == null)
        {
            Destroy(this);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Time.deltaTime * Speed);

            if (transform.position == currentPoint.position)
            {
                pointSelection++;

                if (pointSelection == points.Length)
                {
                    pointSelection = 0;
                }

                currentPoint = points[pointSelection];
            }
    }
}