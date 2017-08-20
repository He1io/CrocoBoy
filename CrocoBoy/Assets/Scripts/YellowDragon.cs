using UnityEngine;

public class YellowDragon : Enemy
{
    public Transform currentPoint;

    public Transform[] points;

    public int pointSelection;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        currentPoint = points[pointSelection];
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }

            if (pointSelection == points.Length)
            {
                pointSelection = 0;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }

            currentPoint = points[pointSelection];
        }
    }
}
