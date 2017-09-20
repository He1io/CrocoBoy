using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(RestartGame());
	}
	
	IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(18);
        AudioManager.instance.StopSound("Credits");
        SceneManager.LoadScene(0);

        //Probably I need to make a method to restart all static variable in order to start the game without money and boosts
        PlayerStats.RestartStats();
        Player.canDoubleJump = false;
    }
}
