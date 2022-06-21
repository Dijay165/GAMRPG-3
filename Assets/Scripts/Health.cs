using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;
using UnityEngine.Events;

public class Death : UnityEvent<Health> { }
public class HealthModify : UnityEvent<bool, float, float> { }
[RequireComponent(typeof(TestStatsHolder))]
public class Health : MonoBehaviour
{
    public Transform playersParent;
    [HideInInspector] public bool invulnerable = false;
     int team;
    private bool isAlive;
    [SerializeField] private float currentHealth;
    [SerializeField] private float minHealth;
    [SerializeField] private float maxHealth;

    public Death OnDeathEvent = new Death();
  //  public Action OnDeath;
    public HealthModify OnHealthModifyEvent = new HealthModify();



    // Start is called before the first frame update
    void Start()
    {
 
        isAlive = true;
        
        playersParent = transform;
        team = (int)GetComponent<TestStatsHolder>().unitFaction;
        maxHealth = GetComponent<TestStatsHolder>().unitStat.healthPoints;
        InitializeValues();
    }

    private void OnEnable()
    {
        
    }
    public void InitializeValues()
    {
        isAlive = true;
        currentHealth = maxHealth;
    }
    public bool CompareTeam(int p_inflictingTeam)
    {
        if (p_inflictingTeam == team)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void ModifyHealth(float p_healthModifer)
    {
        if (isAlive)
        {
            currentHealth += p_healthModifer;

            Mathf.Clamp(currentHealth, minHealth, maxHealth);
            
            //Check is alive
            CheckHealth();

        }
    }
    public void AddHealth(float p_healthModifer)
    {
        if (isAlive)
        {
            currentHealth += p_healthModifer;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            
            //Check is alive
            CheckHealth();
            

        }
    }
    public void SubtractHealth(float p_healthModifer)
    {

        if (isAlive)
        {
            currentHealth -= p_healthModifer;
            if (currentHealth < minHealth)
            {
                currentHealth = minHealth;
            }
            //Check is alive
            CheckHealth();
    
        }

    }

    void CheckHealth()
    {
        if (currentHealth > 0)
        {
            isAlive = true;
            OnHealthModifyEvent.Invoke(isAlive, currentHealth, maxHealth);
        }
        else
        {
            isAlive = false;
            OnDeathEvent.Invoke(this);
            Debug.Log("Invoke Stuff");
        }
    }

   
}
