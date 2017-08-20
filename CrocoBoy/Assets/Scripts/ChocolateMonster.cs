using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateMonster : Enemy {

    [SerializeField]
    private StatusIndicator status;

    public int maxHealth = 6;

    //Shoot
    public Transform target;
    public Transform firePoint;
    public Transform fireballPrefab;
    public LayerMask whatToHit;
    public float shootDelay = 2f;
    public float initialShootDelay;
    bool boosted = false;

    void Start()
    {
        HP = maxHealth;
        initialShootDelay = shootDelay;
    }

    void Update()
    {
        status.SetHealth(HP, maxHealth);

        if (HP == 1 && !boosted)
        {
            initialShootDelay = initialShootDelay*2/3;
            boosted = true;
        }

        shootDelay -= Time.deltaTime;

        if (shootDelay <= 0)
        {
            AudioManager.instance.PlaySound("ManEatingPlantShoot");
            Shoot();
            shootDelay = initialShootDelay;
        }


    }

    void Shoot()
    {
        Vector2 direction = target.position - transform.position;

        //Get the rotation the fireball should face
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var fireballRot = Quaternion.AngleAxis(angle, Vector3.forward);

        Transform fireball = Instantiate(fireballPrefab, firePoint.position, fireballRot, transform);
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

        if (HP == 0)
        {
            StartCoroutine(GameMaster.instance.KillEnemy(this));
        }
    }
}
