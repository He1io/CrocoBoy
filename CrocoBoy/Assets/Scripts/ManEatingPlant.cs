using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManEatingPlant : Enemy
{

    [SerializeField]
    private StatusIndicator status;

    public int maxHealth = 6;

    //Shoot
    public Transform target;
    public Transform fireballPrefab;
    public LayerMask whatToHit;
    public float shootDelay = 5f;

    void Start()
    {
        HP = maxHealth;
    }

    void Update()
    {
        status.SetHealth(HP, maxHealth);

        shootDelay -= Time.deltaTime;

        if (shootDelay <= 0)
        {
            AudioManager.instance.PlaySound("ManEatingPlantShoot");
            Shoot();
            shootDelay = 5f;
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
}
