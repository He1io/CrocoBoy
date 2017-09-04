using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsMusicController : MonoBehaviour
{
    public static LevelsMusicController instance;

    public static int currentWorld = 1;
    public static bool playerDiedInBossFight = false;

    private AudioManager audioManager;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No Audiomanager found!");
        }
    }


    public static void StartShopMusic()
    {
        instance.audioManager.StopSound("Soundtrack" + currentWorld);
        AudioManager.instance.PlaySound("ShopTheme");
    }

    public static void StartBossMusic()
    {
        //Stop shop music and play boss music
        instance.audioManager.StopSound("ShopTheme");
        instance.audioManager.PlaySound("Boss");
    }

    public static IEnumerator StartNextWorldMusic()
    {
        AudioManager.instance.StopSound("Boss");
        AudioManager.instance.PlaySound("WorldComplete");
        currentWorld++;
        yield return new WaitForSeconds(2f);

        //Stop boss music and play next world music
        instance.audioManager.PlaySound("Soundtrack" + currentWorld);
    }

}
