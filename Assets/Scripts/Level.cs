using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Level : MonoBehaviour
{
    public bool canLevelUp;
    public bool isFixedEXPReward;
    public float fixedEXPReward;
    public int currentLevel = 1;
    public int maxLevel;
    public float exp;
    public float maxExp;


    public List<Health> enemies = new List<Health>();
 

    public float expRadius = 1500;

    public List<int> expPerLevel = new List<int>();
    [SerializeField] private TextMeshProUGUI levelUI;
    [SerializeField] private Image expBarFill;
    public Action OnLevelUp;
    public int skillPoints;
    public Hitbox LevelTrigger;
    Unit unit;
    int team;
    private void Awake()
    {
        if (TryGetComponent<Unit>(out Unit foundUnit))
        {
            unit = foundUnit;
            team = (int)unit.unitFaction;
    
        }
    }

    public void RewardExp()
    {
        unit.FindNearbyHeroes(expRadius);

        if (unit.nearbyEnemyHeroes.Count > 0)
        {
            float currentExpReward = 0;
            if (unit is Hero)
            {
                currentExpReward = (100 + 0.13f * exp);
            }
            else
            {
                currentExpReward = (fixedEXPReward);
            }

            currentExpReward = currentExpReward / unit.nearbyEnemyHeroes.Count;
            foreach (Hero nearbyHero in unit.nearbyEnemyHeroes)
            {
              
                nearbyHero.level.AddExp(currentExpReward);

            }
        }
     
        //   Debug.Log(gameObject.name+" Unit dea");
    }
    
    //FORMULA 
    //BountyXP = (100XP + 0.13 × DeadHeroXP) / n + StreakXP
    private void Start()
    {
        if (canLevelUp)
        {
            maxExp = expPerLevel[currentLevel-1];
            //LevelTrigger.OnTriggerEnteredFunction += TriggerEnteredFunction;
            //LevelTrigger.OnTriggerExittedFunction += TriggerExittedFunction;
            maxLevel = expPerLevel.Count + 1;
        }
        
        UpdateLevelUI();
        UpdateExpUIBar();

    }

    public void AddExp(float p_expModifer)
    {
        
        if (currentLevel < maxLevel)
        {
            Debug.Log(currentLevel + " ADDED EXP " + p_expModifer);
            exp += p_expModifer;
            //can still level up
            if (exp >= maxExp)
            {
              
                Debug.Log("Level up");
                currentLevel++;
                skillPoints++;
                //Level up
                if (currentLevel < maxLevel)
                {
                    SetNextExpRequirement();
                    
                    
                 
                }
                else //if it reached max level
                {
                    exp = maxExp;
                }
            
                UpdateLevelUI();
           
                //OnLevelUp.Invoke();
            }
            UpdateExpUIBar();
        }
        else
        {
            //Dont do anything if it reached max level
        }
        
        

        
    }
   
    void SetNextExpRequirement()
    {
        float  excessExp = exp - maxExp; //750 - 500 = 250 or 500 - 500 = 0
        exp = 0 + excessExp;

        maxExp = expPerLevel[currentLevel - 1];

 
    }

    void UpdateLevelUI()
    {
        if (levelUI != null)
        {
            levelUI.text = currentLevel.ToString();
        }
    }

    void UpdateExpUIBar()
    {
        if (expBarFill != null)
        {

            expBarFill.fillAmount = exp / maxExp;
        }
    }
}
