using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounterUI : MonoBehaviour {

    public Text moneyText;

    void Start()
    {
        moneyText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        moneyText.text = "x " + PlayerStats.money;
    }
}
