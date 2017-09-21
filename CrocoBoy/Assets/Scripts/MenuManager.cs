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

    bool musicPlaying = false;

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
        //If I start the mainMenu music in the Start() method and initialize AudioManager on the Awake(), 
        //it seems like Unity do not have time enough to initialize Audiomanager and it does not work
        if (!musicPlaying)
        {
            audioManager.PlaySound(mainMenuClip);
            musicPlaying = true;
        }

        //Button A
        if (Input.GetButtonDown("ButtonA"))
        {
            StartGame();
        }
        //Button B
        if (Input.GetButtonDown("ButtonB"))
        {
            QuitGame();
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
