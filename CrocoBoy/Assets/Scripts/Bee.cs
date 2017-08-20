using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy {

    public Transform target;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	void Update () {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position +=  direction * Speed * Time.deltaTime;

        if (target.GetComponent<Animator>().GetBool("Dead"))
        {
            this.enabled = false;
        }

        if(transform.position.x > target.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
            spriteRenderer.flipX = true;

    }
    override public void DamageEnemy()
    {
        HP--;

        if (animator != null && HP > 0)
        {
            animator.Play("Hitted");

        }

        if (HP == 0)
        {
            GetComponent<EdgeCollider2D>().enabled = false;
            StartCoroutine(GameMaster.instance.KillEnemy(this));
        }
    }

}
