using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy {

    public Transform target;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position +=  direction * Speed * Time.deltaTime;
    }
}
