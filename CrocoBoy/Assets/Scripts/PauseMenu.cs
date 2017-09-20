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
    Button[] buttons;
    int currentButton = 0;
    int previousButton = -1;

    void Start()
    {
        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.LogError("No audiomanager found!");
        }

        buttons = GetComponentsInChildren<Button>();
    }
    
    void Update()
    {

        if (Input.GetAxis("Vertical") != 0)
        {
            if(previousButton != -1)
            {
                buttons[previousButton].animator.SetTrigger("Normal");
            }

            buttons[currentButton].animator.SetTrigger("Highlighted");
            previousButton = currentButton;
            currentButton++;

            if (currentButton == buttons.Length)
            {
                currentButton = 0;
            }
        }

    }
    
    public void ResumeGame()
    {
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