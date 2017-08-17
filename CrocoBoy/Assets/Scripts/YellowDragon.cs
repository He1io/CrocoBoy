using UnityEngine;

public class YellowDragon : Enemy
{
    public Transform currentPoint;

    public Transform[] points;

    public int pointSelection;

    SpriteRenderer renderer;

    void Start()
    {
        currentPoint = points[pointSelection];
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Time.deltaTime * Speed);

        if (transform.position == currentPoint.position)
        {
            pointSelection++;

            //If the enemy gets to half of his path, flip the graphics 
            if (pointSelection == 2)
            {
                renderer.flipX = !renderer.flipX;
            }

            if (pointSelection == points.Length)
            {
                pointSelection = 0;
                renderer.flipX = !renderer.flipX;
            }

            currentPoint = points[pointSelection];
        }
    }
}
