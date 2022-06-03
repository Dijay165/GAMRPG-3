using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/*
  It has the same functionality as a normal event. You call the Events script to invoke the declared event variable that you've created. 
  please look at the SelectUnit.cs to see how to connect them. 
 reference: https://www.youtube.com/watch?v=RPhTEJw6KbI&list=PLuiBbLS_hU1uu5bMXVceRpHBxO7fSmeLd&index=48
*/
public class UnitSelectionUI : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI healthPointText;
    public void ChangeInfo(UnitStat unitStat)
    {
        Events.OnUnitSelect.Invoke();
        
        if (unitStat != null)
        {
   
            Debug.Log("Some Stuff");
            attackText.text = unitStat.attack.ToString();
            defenseText.text = unitStat.defense.ToString();
            moveSpeedText.text = unitStat.moveSpeed.ToString();
            healthPointText.text = unitStat.healthPoints.ToString();
        }
    }

}
