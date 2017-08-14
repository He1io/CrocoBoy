using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour
{

    [SerializeField]
    private RectTransform healthBarRect;


    void Start()
    {
        if (healthBarRect == null)
        {
            Debug.LogError("STATUS INDICATOR: No health bar object referrenced!");
        }
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        float value = (float)currentHealth / maxHealth;

        healthBarRect.localScale = new Vector3(value, healthBarRect.localScale.y, healthBarRect.localScale.z);
    }
}
