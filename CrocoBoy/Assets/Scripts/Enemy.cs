using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;
    public float Speed;

    private Animator animator;

    private bool alreadyHitted = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    virtual public void DamageEnemy()
    {
        HP--;

        if (animator != null && HP > 0)
        {
            animator.Play("Hitted");
        }
            
        if (HP == 0)
        {
            StartCoroutine(GameMaster.instance.KillEnemy(this));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (animator!=null && animator.GetCurrentAnimatorStateInfo(0).IsName("Hitted"))
        {
            return;
        }

        if (collision.gameObject.tag == "Player" && !alreadyHitted)
        {
            //We cannot hit the enemy if we are dead
            if (collision.transform.GetComponent<Animator>().GetBool("Dead") == true)
            {
                return;
            }

            alreadyHitted = true;

            Transform player = collision.transform;
            //STOMP
            float pointBetweenPlayerAndEnemy = (transform.position.y + collision.transform.position.y) / 2;
            //TODO: Player Position - 0.2 is a bad way to calculate the player's feet position in order to stomp an enemy properly
            if ((player.position.y - 0.2) > pointBetweenPlayerAndEnemy)
            {
                DamageEnemy();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && alreadyHitted)
        {
            alreadyHitted = false;
        }
    }
}

