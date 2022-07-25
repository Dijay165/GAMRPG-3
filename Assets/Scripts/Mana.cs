using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mana : MonoBehaviour
{

   // [SerializeField] private float mana;
    public float minMana;
    public float maxMana;
    public float currentMana;
    public float manaRegen;

    //  [SerializeField] private Image manaBarFill;
    // Start is called before the first frame update

    private void Awake()
    {
        Initialization();

    }
    public void Initialization()
    {
        if (TryGetComponent<Unit>(out Unit unit))
        {

            maxMana = (int)unit.unitStat.startingMaxMana;
            manaRegen = unit.unitStat.manaRegeneration;
             currentMana = maxMana;
           // currentMana = 100;
        }
    }


    public void AddMana(float p_manaModifier)
    {
      //  Debug.Log("Add Mana");
        currentMana += Mathf.CeilToInt(Mathf.Clamp(p_manaModifier, 0, maxMana));
       // currentMana += p_manaModifier;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }

       // UpdateManaBar();
        
    }
    public void SubtractMana(float p_manaModifier)
    {
        currentMana -= Mathf.Clamp(p_manaModifier, 0, maxMana);
        //  currentMana -= p_manaModifier;
        if (currentMana < minMana)
        {
            currentMana = minMana;
        }

       // UpdateManaBar();
        
    }

    //void UpdateManaBar()
    //{
    //    if (manaBarFill != null)
    //    {
    //        manaBarFill.fillAmount = currentMana / maxMana;
    //    }
    //}
}
