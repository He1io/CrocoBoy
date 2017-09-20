using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateMonster : Enemy {

    [SerializeField]
    private StatusIndicator status;

    public int maxHealth = 5;

    public Transform lightning;
    float lightningTime = 3f;
    bool lightningActive = false;

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
        //StatusBar
        status.SetHealth(HP, maxHealth);

        //Shoot
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

        //Lightning
        if (lightningActive)
        {
            lightningTime -= Time.deltaTime;
            if (lightningTime <= 0)
            {
                SpriteRenderer[] sprites = lightning.GetComponentsInChildren<SpriteRenderer>();
                for (int i = 0; i < sprites.Length; i++)
                {
                    sprites[i].enabled = false;
                }

                Animator[] animators = lightning.GetComponentsInChildren<Animator>();
                for (int i = 0; i < animators.Length; i++)
                {
                    animators[i].enabled = false;
                }

                lightning.GetComponent<BoxCollider2D>().enabled = false;
                lightningTime = 3f;
                lightningActive = false;
            }
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

        StartCoroutine(ActiveMagicFlame());

        if (animator != null && HP > 0)
        {
            animator.Play("Hitted");
            AudioManager.instance.PlaySound("Boss1Hitted");

        }

        if (HP == 0)
        {
            status.SetHealth(HP, maxHealth);

            //Destroy all enemies and projectiles in order to advance to the next lvl 
            GameMaster.instance.DestroyEnemies();
            GameMaster.instance.DestroyFireballs();
            DestroySpikes();
            StartCoroutine(GameMaster.instance.KillEnemy(this));
        }
    }

    private IEnumerator ActiveMagicFlame()
    {
        yield return new WaitForSeconds(0.5f);

        SpriteRenderer[] sprites = lightning.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].enabled = true;
        }

        Animator[] animators = lightning.GetComponentsInChildren<Animator>();
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].enabled = true;
            animators[i].Play("MagicFlame",0,0f);
            AudioManager.instance.PlaySound("ChocolateMonsterFire");
        }

        lightning.GetComponent<BoxCollider2D>().enabled = true;


        lightningActive = true;
    }

    public void DestroySpikes()
    {
        GameObject[] spikes = GameObject.FindGameObjectsWithTag("Spike");

        foreach (GameObject spike in spikes)
        {
            Destroy(spike);
        }
    }
}
