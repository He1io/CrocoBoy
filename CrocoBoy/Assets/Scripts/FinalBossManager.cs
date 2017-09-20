using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossManager : MonoBehaviour {

    public Transform skullTransform;
    Skull skull;

    //Game Finished
    public Transform worldComplete;
    bool levelPassed = false;

	// I need to manage all this stuff here, because I did it in Skull script but it did not work because the Skull is going to be destroyed
	void Start () {
        skull = skullTransform.GetComponent<Skull>();
	}
	
	void Update () {
       
		if(skull.HP == 0 && !levelPassed)
        {
            levelPassed = true;
            FindObjectOfType<Player>().fallBoundary = -99999;

            //Destroy all enemies and projectiles in order to advance to the next lvl 
            GameMaster.instance.DestroyEnemies();
            GameMaster.instance.DestroyFireballs();
            StartCoroutine(GameMaster.instance.KillEnemy(skull));

            StartCoroutine(Credits());
        }
	}

    IEnumerator Credits()
    {
        worldComplete.gameObject.SetActive(true);
        StartCoroutine(LevelsMusicController.StartCreditsMusic());
        yield return new WaitForSeconds(2f);
        StartCoroutine(GameMaster.instance.NextLevel());
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
