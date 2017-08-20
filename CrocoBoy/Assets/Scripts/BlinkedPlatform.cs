using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkedPlatform : MonoBehaviour {

    public float timeToBlink = 2f;
    private float currentTime;

    //References
    SpriteRenderer[] spriteRenderers;
    BoxCollider2D bc;

	// Use this for initialization
	void Start () {
        currentTime = timeToBlink;

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].enabled = !spriteRenderers[i].enabled;
            }
            bc.enabled = !bc.enabled;

            currentTime = timeToBlink;
        }
	}
}
