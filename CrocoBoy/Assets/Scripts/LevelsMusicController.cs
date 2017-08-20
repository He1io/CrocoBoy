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

    public static void StartBossMusic()
    {
        //Stop world music and play boss music
        instance.audioManager.StopSound("Soundtrack" + currentWorld);
        instance.audioManager.PlaySound("Boss");
        currentWorld++;
    }

    public static IEnumerator StartShopMusic()
    {
        AudioManager.instance.StopSound("Boss");
        AudioManager.instance.PlaySound("WorldComplete");
        yield return new WaitForSeconds(2f);
        AudioManager.instance.PlaySound("ShopTheme");
    }

    public static void StartNextWorldMusic()
    {
        //Stop shop music and play next world music
        instance.audioManager.StopSound("ShopTheme");
        instance.audioManager.PlaySound("Soundtrack" + currentWorld);
    }

}
