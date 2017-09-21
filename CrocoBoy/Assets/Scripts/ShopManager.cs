using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {

    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    public Transform panel;
    public Transform speedBootsUI;
    public Transform doubleJumpUI;
    public Transform shieldUI;

    AudioManager audioManager;

    bool speedBoots = false;
    int speedBootsPrice = 250;
    bool doubleJump = false;
    int doubleJumpPrice = 300;
    bool shield = false;
    int shieldPrice = 850;

    //Boost
    public float speedBoost = 2f;

    void Start()
    {
        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.LogError("No audiomanager found!");
        }

        if (Player.canDoubleJump == true)
        {
            Destroy(doubleJumpUI.gameObject);
        }

        if (PlayerStats.speedBoosted == true)
        {
            Destroy(speedBootsUI.gameObject);
        }

        if (PlayerStats.invencibility == true)
        {
            Destroy(shieldUI.gameObject);
        }
    }

    void Update()
    {
        //Button A
        if (Input.GetButtonDown("ButtonA"))
        {
            NextLevel();
        }
        //Button B
        if (Input.GetButtonDown("ButtonB"))
        {
            QuitGame();
        }
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

    public void ActivePanel()
    {
        panel.gameObject.SetActive(true);
        
    }

    public void DisablePanel()
    {
        panel.gameObject.SetActive(false);
        speedBoots = false;
        doubleJump = false;
        shield = false;
    }

    public void NextLevel()
    {
        StartCoroutine(GameMaster.instance.NextLevel());
        LevelsMusicController.StartBossMusic();
    }

    public void BuySpeedBoots()
    {
        if (EnoughMoney(speedBootsPrice))
        {
            ActivePanel();
            speedBoots = true;
        }
    }
    public void BuyDoubleJump()
    {
        if (EnoughMoney(speedBootsPrice))
        {
            ActivePanel();
            doubleJump = true;
        }
    }

    public void BuyShield()
    {
        if (EnoughMoney(speedBootsPrice))
        {
            ActivePanel();
            shield = true;
        }
    }

    bool EnoughMoney(int price)
    {
        if (PlayerStats.money >= price)
        {
            return true;
        }
        else
        {
            AudioManager.instance.PlaySound("Wrong");
            return false;
        }

    }

    public void ConfirmPurchase()
    {
        if (speedBoots && EnoughMoney(speedBootsPrice))
        {
            AudioManager.instance.PlaySound("Purchase");

            PlayerStats.money -= speedBootsPrice;
            DisablePanel();
            Destroy(speedBootsUI.gameObject);

            PlayerStats.movementSpeed += speedBoost;
            PlayerStats.speedBoosted = true;
        }

        if (doubleJump && EnoughMoney(doubleJumpPrice))
        {
            AudioManager.instance.PlaySound("Purchase");

            PlayerStats.money -= doubleJumpPrice;
            DisablePanel();
            Destroy(doubleJumpUI.gameObject);

            GameMaster.instance.ActivePlayerDoubleJump();
        }

        if (shield && EnoughMoney(shieldPrice))
        {
            AudioManager.instance.PlaySound("Purchase");
            PlayerStats.money -= shieldPrice;
            DisablePanel();
            Destroy(shieldUI.gameObject);

            PlayerStats.invencibility = true;
        }
    }
}
