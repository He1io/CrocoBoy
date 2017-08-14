using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    Player player;
    Vector2 direction;

    public float speed = 10f;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        direction = player.transform.position - transform.position;
        direction.Normalize();
        Destroy(this.gameObject,10f);
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += new Vector3(direction.x,direction.y,0) * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(GameMaster.instance.KillPlayer(player));
        }
    }
}
