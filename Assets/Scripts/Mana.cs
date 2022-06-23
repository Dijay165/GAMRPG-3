using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mana : MonoBehaviour
{

    [SerializeField] private float mana;
    [SerializeField] private float minMana;
    [SerializeField] private float maxMana;

    [SerializeField] private Image manaBarFill;
    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent<Image>(out var test))
        {
            //Do something here
            test.fillAmount = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHealth(float p_manaModifier)
    {
        
        mana += p_manaModifier;
        if (mana > maxMana)
        {
            mana = maxMana;
        }

        UpdateManaBar();
        
    }
    public void SubtractHealth(float p_manaModifier)
    {
        
        mana -= p_manaModifier;
        if (mana < minMana)
        {
            mana = minMana;
        }

        UpdateManaBar();
        
    }

    void UpdateManaBar()
    {
        if (manaBarFill != null)
        {
            manaBarFill.fillAmount = mana / maxMana;
        }
    }
}
