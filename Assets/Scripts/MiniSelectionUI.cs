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


    private void OnEnable()
    {
        Events.OnUnitSelect.AddListener(ChangeAttributeUI);
    }

    private void OnDisable()
    {
        Events.OnUnitSelect.RemoveListener(ChangeAttributeUI);
    }

    private void Update()
    {
        if(GameManager.instance.lastUnitSelect != null)
        {
            ChangeAttributeUI(GameManager.instance.lastUnitSelect);
        }
        else
        {
            border.gameObject.SetActive(false);
        }
    }

    public void ChangeAttributeUI(Unit unit)
    {
        // Events.OnUnitSelect.Invoke(unitStat);

        GameManager.instance.lastUnitSelect = unit;
 

        if (unit != null)
        {
            
            characterPortrait.sprite = unit.unitStat.portraitImage;
            
            if (unit.TryGetComponent<Health>(out Health health))
            {
                currentHealthText.text = health.GetHealth().ToString();
                maxHealthText.text = health.maxHealth.ToString();
                slider.maxValue = health.maxHealth;
                slider.value = health.currentHealth;
            }
            if (unit.TryGetComponent<Attributes>(out Attributes attributes))
            {
                attackText.text = attributes.attackDamage.ToString();
                defenseText.text = attributes.armor.ToString();
                moveSpeedText.text = attributes.movementSpeed.ToString();
            }
        }
    }

}
