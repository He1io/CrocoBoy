using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int amountOfMoney = 10;

    //References
    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audiomanager found");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && this.enabled)
        {
            //Make sure we don't trigger this event twice
           this.enabled = false;
            GameMaster.instance.TakeCoin(this);
        }
    }
}
