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
            FindObjectOfType<Player>().fallBoundary = -99999;
            StartCoroutine(NextWorld());
        }
	}

    IEnumerator NextWorld()
    {
        levelPassed = true;
        worldComplete.gameObject.SetActive(true);
        StartCoroutine(LevelsMusicController.StartNextWorldMusic());
        yield return new WaitForSeconds(2f);
        StartCoroutine(GameMaster.instance.NextLevel());
    }
}
