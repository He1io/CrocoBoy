using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneManager : MonoBehaviour {

    public Transform worldComplete;
    bool levelPassed = false;

	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !levelPassed)
        {
            StartCoroutine(NextWorld());
        }
	}

    IEnumerator NextWorld()
    {
        levelPassed = true;
        worldComplete.gameObject.SetActive(true);
        LevelsMusicController.StartShopMusic();
        yield return new WaitForSeconds(2f);
        StartCoroutine(GameMaster.instance.NextLevel());

    }
}
