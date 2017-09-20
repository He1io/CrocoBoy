using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : Enemy
{

    [SerializeField]
    private StatusIndicator status;

    public int maxHealth = 6;

    //Shoot
    public Transform target;
    public Transform fireballPrefab;
    public LayerMask whatToHit;
    public float shootDelay = 5f;

    //Movement
    public Transform currentPoint;

    public Transform[] points;

    public int pointSelection;

    void Start()
    {
        HP = maxHealth;

        currentPoint = points[pointSelection];

    }

    void Update()
    {
        status.SetHealth(HP, maxHealth);

        shootDelay -= Time.deltaTime;

        if (shootDelay <= 0)
        {
            AudioManager.instance.PlaySound("ManEatingPlantShoot");
            Shoot();
            shootDelay = 2.5f;
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

    public override void DamageEnemy()
    {
        HP--;

        if (animator != null && HP > 0)
        {
            animator.Play("Hitted");
            AudioManager.instance.PlaySound("Boss1Hitted");

        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Poison")
        {
            DamageEnemy();
        }
       
    }    
}
