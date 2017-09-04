using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public static GameMaster instance;

    public float chestSoundsInterval = 0.3f;
    //References
    private AudioManager audioManager;
    private GameObject pauseMenu;

    void Awake()
    {
        //Find the PauseMenu and inactive it
        //TODO: I don't like it too much because I have to see the PAUSEMENU all time in the inspector
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No Audiomanager found!");
        }
    }

    void Update()
    {
        //When we pause, stop the game and toggle the active state of the PauseMenu
        if (Input.GetButtonDown("Pause"))
        {
            if (Time.timeScale == 1)
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                Time.timeScale = 1;
            }
        }
    }

    //KILL
    public IEnumerator KillPlayer(Player player)
    {
        Animator animator = player.GetComponent<Animator>();

        if (PlayerStats.startingMoney >= 5)
        {
            PlayerStats.startingMoney -= 5;
        }

        PlayerStats.money = PlayerStats.startingMoney;
        animator.SetBool("Dead", true);
        AudioManager.instance.PlaySound("Dead");
        yield return new WaitForSeconds(0.9f);

        Destroy(player.transform.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator KillEnemy(Enemy enemy)
    {
            audioManager.PlaySound("Explosion");
            enemy.transform.GetComponent<Animator>().Play("Explosion");
            yield return new WaitForSeconds(0.25f);
            Destroy(enemy.gameObject);
    }

    //MONEY
    public void TakeCoin(Coin coin)
    {
        Destroy(coin.gameObject);
        PlayerStats.money += coin.amountOfMoney;
        audioManager.PlaySound("Coin");
    }



    public void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies){
            Destroy(enemy);
        }
    }

    public IEnumerator OpenChest(Chest chest)
    {
        DestroyEnemies();
        //Play coin sounds and increment amount of money
        for (int i = 0; i < chest.amountOfCoins; i++)
        {
            audioManager.PlaySound("Coin");
            PlayerStats.money += 10;
            yield return new WaitForSeconds(chestSoundsInterval);
        }

        StartCoroutine(NextLevel());

    }

    public IEnumerator OpenChestPreBoss(ChestPreBoss chest)
    {
        DestroyEnemies();
        //Play coin sounds and increment amount of money
        for (int i = 0; i < chest.amountOfCoins; i++)
        {
            audioManager.PlaySound("Coin");
            PlayerStats.money += 10;
            yield return new WaitForSeconds(chestSoundsInterval);
        }

        StartCoroutine(NextLevel());
        LevelsMusicController.StartShopMusic();
    }

    //NEXT LEVEL
    public IEnumerator NextLevel()
    {
        float fadeTime = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);

        //Pass to the next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //Pass the money to the next level
        PlayerStats.startingMoney = PlayerStats.money;
    }

    //BOOST
    public void ActivePlayerDoubleJump()
    {
        Player.canDoubleJump = true;
    }
}
