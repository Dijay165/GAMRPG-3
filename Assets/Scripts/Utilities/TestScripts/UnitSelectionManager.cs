using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class UnitSelectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI moveSpeed;

    public UnitStat unitStat;

    //Need to create event that whenever a unit got clicked, it updates the text and unit stats of the unit selection 

    void Start()
    {
        if(unitStat != null)
        {
            attackText.text = unitStat.attack.ToString();
            defenseText.text = unitStat.defense.ToString();
            moveSpeed.text = unitStat.moveSpeed.ToString();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
