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
    public int level = 1;
    public int maxLevel;
    public float exp;
    public float maxExp;

    public List<Health> enemies = new List<Health>();

    public float expRadius = 1500;

    public List<int> expPerLevel = new List<int>();
    [SerializeField] private TextMeshProUGUI levelUI;
    [SerializeField] private Image expBarFill;
    public Action OnLevelUp;
    
    public Hitbox LevelTrigger;

    //FORMULA 
    //BountyXP = (100XP + 0.13 × DeadHeroXP) / n + StreakXP
    private void Start()
    {
        if (canLevelUp)
        {
            maxExp = expPerLevel[level-1];
            LevelTrigger.OnTriggerEnteredFunction += TriggerEnteredFunction;
            LevelTrigger.OnTriggerExittedFunction += TriggerExittedFunction;
            maxLevel = expPerLevel.Count + 1;
        }
        
        UpdateLevelUI();
        UpdateExpUIBar();

    }

    public void TriggerEnteredFunction(Collider hit)
    {
        if (hit.gameObject.GetComponent<Health>())
        {

            Health hitHealth = hit.gameObject.GetComponent<Health>();
            //check if it is already in target list, if not in target list, add it

            if (hitHealth.invulnerable == false) //add it if it isnt invulnerable
            {
               
                if (hitHealth.GetComponent<TestStatsHolder>().unitFaction != GetComponent<TestStatsHolder>().unitFaction)
                {
                    if (hit.gameObject.GetComponent<Level>() != null)
                    {
                        if (!hit.gameObject.GetComponent<Level>().isFixedEXPReward)
                        {
                            //Register
                            hitHealth.OnDeathEvent.AddListener(HeroDeath);
                        }
                        else
                        {
                            Debug.Log("Registered");
                            hitHealth.OnDeathEvent.RemoveListener(CreepDeath);
                        }
                    }
                   
               

                    
                }
            }
        }
    }

    public void TriggerExittedFunction(Collider hit)
    {
        if (hit.gameObject.GetComponent<Health>())
        {

            Health hitHealth = hit.gameObject.GetComponent<Health>();
            //check if it is already in target list, if not in target list, add it

            if (hitHealth.invulnerable == false) //add it if it isnt invulnerable
            {

                if (hitHealth.GetComponent<TestStatsHolder>().unitFaction != GetComponent<TestStatsHolder>().unitFaction)
                {

                    if (hit.gameObject.GetComponent<Level>() != null)
                    {
                        if (!hit.gameObject.GetComponent<Level>().isFixedEXPReward)
                        {
                            //Register
                            hitHealth.OnDeathEvent.AddListener(HeroDeath);
                        }
                        else
                        {
                            Debug.Log("Registered");
                            hitHealth.OnDeathEvent.RemoveListener(CreepDeath);
                        }
                    }
                }
            }
        }
    }
    public void HeroDeath(Health enemyHealth)
    {
        //(100XP + 0.13 × DeadHeroXP) / n + StreakXP
        Debug.Log("Unit " + enemyHealth + " " + enemyHealth.gameObject.GetComponent<Level>().exp);
        float expModifier = (100 + 0.13f * enemyHealth.gameObject.GetComponent<Level>().exp);
        AddExp(expModifier);
    }

    public void CreepDeath(Health enemyHealth)
    {
        //(100XP + 0.13 × DeadHeroXP) / n + StreakXP
        //Debug.Log("Unit " + enemyHealth.gameObject.name + " " + enemyHealth.gameObject.GetComponent<Level>().fixedEXPReward);
        float expModifier = (enemyHealth.gameObject.GetComponent<Level>().fixedEXPReward);
        AddExp(expModifier);
    }
    public void AddExp(float p_expModifer)
    {
        
        if (level < maxLevel)
        {
            Debug.Log(level + " ADDED EXP " + p_expModifer);
            exp += p_expModifer;
            //can still level up
            if (exp >= maxExp)
            {
                Debug.Log("Level up");
                level++;
                //Level up
                if (level < maxLevel)
                {
                    SetNextExpRequirement();

                }
                else //if it reached max level
                {
                    exp = maxExp;
                }
                Debug.Log("work 1");
                UpdateLevelUI();
                Debug.Log("work 2");
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

        maxExp = expPerLevel[level - 1];

 
    }

    //void CheckEnemies()
    //{
    //    enemies.Clear();
    //    //Detected enemy list
    
    //    //RaycastHit hit;
    //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, expRadius);
    //    foreach (var hitCollider in hitColliders)
    //    {
    //        //if enemy is within its detection radius, it sees enemy



    //        if (hitCollider.gameObject.GetComponent<Health>())
    //        {
    
    //            Health hitHealth = hitCollider.gameObject.GetComponent<Health>();
    //            //check if it is already in target list, if not in target list, add it

    //            if (hitHealth.invulnerable == false) //add it if it isnt invulnerable
    //            {

    //                if (!hitHealth.CompareTeam(gameObject.GetComponent<Health>().team))
    //                {

    //                    enemies.Add(hitHealth);

    //                    //Register
    //                    hitHealth.OnDeath += EnemyDeath;
    //                    //organize
    //                    if (enemies.Count > 2)
    //                    {
    //                        for (int i = 0; i < enemies.Count; i++)
    //                        {
    //                            float newEnemyDistance = Vector3.Distance(enemies[i].transform.position, transform.position);
    //                            for (int si = 1; si < enemies.Count - 1; si++)
    //                            {
    //                                float currentEnemyIndexDistance = Vector3.Distance(enemies[si].transform.position, transform.position);
    //                                if (newEnemyDistance > currentEnemyIndexDistance) //if the newly detected enemy's distance is further away than the current index of enemy in list, move to the next enemy in the list
    //                                {
    //                                    Health saved = enemies[i];
    //                                    enemies[i] = enemies[si];
    //                                    enemies[si] = saved;
    //                                }
    //                            }
    //                        }
    //                    }

    //                }


    //            }



    //        }
    //    }

    //}
    void UpdateLevelUI()
    {
        if (levelUI != null)
        {
            levelUI.text = level.ToString();
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
