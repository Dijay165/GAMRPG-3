using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MiniSelectionUI : MonoBehaviour
{
    // Start is called before the first frame updatepublic TextMeshProUGUI attackText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI currentHealthText;
    public Slider slider;
    public Image characterPortrait;
    public GameObject border; 


    public void ChangeAttributeUI(Attributes unitStat)
    {
        Events.OnUnitSelect.Invoke();
        Debug.Log("Touched");
        if (unitStat != null)
        {
            Debug.Log(unitStat.hp.currentHealth);
            Debug.Log(slider.value);
            slider.maxValue = unitStat.hp.maxHealth;
            slider.value = unitStat.hp.currentHealth;

            attackText.text = unitStat.attackDamage.ToString();
            defenseText.text = unitStat.armor.ToString();
            moveSpeedText.text = unitStat.movementSpeed.ToString();
            currentHealthText.text = unitStat.hp.currentHealth.ToString();
            maxHealthText.text = unitStat.hp.maxHealth.ToString();
            characterPortrait.sprite = unitStat.portraitImage;

        }
    }
}
