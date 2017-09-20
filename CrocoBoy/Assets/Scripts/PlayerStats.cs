using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public static float movementSpeed = 6f;
    public static bool speedBoosted = false;

    public static bool invencibility = false;

    public float jumpForce = 100f;

    public static int startingMoney = 0;

    public static int money;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        money = startingMoney;
    }

    public static void RestartStats()
    {
        movementSpeed = 6f;
        speedBoosted = false;
        invencibility = false;
        startingMoney = 0;
    }

}
