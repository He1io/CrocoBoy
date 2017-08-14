using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    [SerializeField]
    string firstWorldClip = "Soundtrack1";

    [SerializeField]
    string mainMenuClip = "MusicMenu";

    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.LogError("No audiomanager found!");
        }
     }

	public void StartGame()
    {
        audioManager.PlaySound(pressButtonSound);

        audioManager.StopSound(mainMenuClip);
        audioManager.PlaySound(firstWorldClip);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
