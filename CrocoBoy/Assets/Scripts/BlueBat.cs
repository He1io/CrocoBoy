using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBat : Enemy
{

    public Transform currentPoint;

    public Transform[] points;

    public int pointSelection;

    //Shoot
    public Transform target;
    public Transform fireballPrefab;
    public LayerMask whatToHit;
    public float shootDelay = 4f;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        HP = 1;

        currentPoint = points[pointSelection];

        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        //MOVING
        if (transform == null)
        {
            Destroy(this);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Time.deltaTime * Speed);

        //If we reach our target point 
        if (transform.position == currentPoint.position)
        {
            pointSelection++;

            if (pointSelection == points.Length)
            {
                pointSelection = 0;
            }

            currentPoint = points[pointSelection];
        }

        //SHOOTING
        shootDelay -= Time.deltaTime;

        if (shootDelay <= 0)
        {
            if (spriteRenderer.isVisible)
            {
                AudioManager.instance.PlaySound("BlueBatShooting");
                Shoot();
            }
            
            shootDelay = 3f;
        }

        if (transform.position.x > target.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
            spriteRenderer.flipX = true;
    }

    void Shoot()
    {
        Vector2 direction = target.position - transform.position;
        
        //Get the rotation the fireball should face
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var fireballRot = Quaternion.AngleAxis(angle, Vector3.forward);

        Transform fireball = Instantiate(fireballPrefab,transform.position,fireballRot,transform);
        fireball.parent = null;
    }
}
