using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    public int amountOfCoins = 5;

    //References
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && this.enabled)
        {
            animator.enabled = true;
            //Make sure we don't trigger this event twice
            this.enabled = false;
            StartCoroutine(GameMaster.instance.OpenChest(this));
        }
    }
}
