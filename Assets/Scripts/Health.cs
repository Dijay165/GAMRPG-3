using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;
using UnityEngine.Events;

public class Death : UnityEvent<Health> { }
public class HealthModify : UnityEvent<bool, float, float> { }

public class Health : MonoBehaviour
{
    public Transform playersParent;
    public bool invulnerable = false;
    int team;
    private bool isAlive;
    public float currentHealth;
    [SerializeField] private float minHealth;
     public float maxHealth;

    public Death OnDeathEvent = new Death();
    public HealthModify OnHealthModifyEvent = new HealthModify();

    public float GetHealth()
    {
        return currentHealth;
    }

    // Start is called before the first frame update
    void Awake()
    {

        playersParent = transform;
        Events.OnMiniUIUpdate.AddListener(SubtractHealth);
        ResetValues();
    }

    private void OnDisable()
    {
        Events.OnMiniUIUpdate.RemoveListener(SubtractHealth);
    }


    public void ResetValues()
    {
        if (TryGetComponent<Unit>(out Unit unit))
        {
            team = (int)unit.unitFaction;
            maxHealth = (int)unit.unitStat.startingMaxHP;
        }
        InitializeValues();
    }
    public void InitializeValues()
    {

        isAlive = true;
        currentHealth = maxHealth;

    }
    public virtual bool CompareTeam(int p_inflictingTeam)
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

    public virtual bool CompareTeam(Faction p_inflictingTeam)
    {
        if (p_inflictingTeam == (Faction) team)
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
            currentHealth += Mathf.Clamp(p_healthModifer, 0, maxHealth);
            // currentHealth += p_healthModifer;
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
        Events.OnPlayerSelect.Invoke();
      
        if (isAlive)
        {
            ///  Events.OnMiniUIUpdate.Invoke(0f);
            currentHealth -= Mathf.Clamp(p_healthModifer, 0, maxHealth);
         //   currentHealth -= p_healthModifer;
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
           // Debug.Log("Invoke Stuff");
        }
    }

    public float DamageReductionChecker(float attacker, float defender)
    {
       float totalDamage = attacker * defender;

        return totalDamage;
    }
}
