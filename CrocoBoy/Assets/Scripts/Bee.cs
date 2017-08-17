using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy {

    public Transform target;

    SpriteRenderer renderer;

    void Start()
    {
        renderer = transform.GetComponent<SpriteRenderer>();
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
            renderer.flipX = false;
        }
        else
            renderer.flipX = true;

    }
}
