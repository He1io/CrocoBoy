using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.LogError("No audiomanager found!");
        }
    }
    
    void Update()
    {
        //Button A
        if (Input.GetButtonDown("ButtonA"))
        {
            ResumeGame();
        }
        //Button B
        if (Input.GetButtonDown("ButtonB"))
        {
            QuitGame();
        }
    }
    
    public void ResumeGame()
    {
        GameObject.FindObjectOfType<Player>().enabled = true;
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        audioManager.PlaySound(pressButtonSound);
    }

    public void QuitGame()
    {
        audioManager.PlaySound(pressButtonSound);

        Debug.Log("WE QUIT THE GAME!");
        Application.Quit();
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }
}