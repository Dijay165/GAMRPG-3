using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Health : MonoBehaviour
{
    public Transform playersParent;

    [SerializeField] private int team;
    private bool isAlive;
    [SerializeField] private float health;
    [SerializeField] private float minHealth;
    [SerializeField] private float maxHealth;

    public Action OnDeath;


    // Start is called before the first frame update
    void Start()
    {
 
        isAlive = true;
        ModifyHealth(maxHealth);
        playersParent = transform;

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
            health += p_healthModifer;

            Mathf.Clamp(health, minHealth, maxHealth);
            
            //Check is alive
            CheckHealth();

        }
    }
    public void AddHealth(float p_healthModifer)
    {
        if (isAlive)
        {
            health += p_healthModifer;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            
            //Check is alive
            CheckHealth();
            

        }
    }
    public void SubtractHealth(float p_healthModifer)
    {

        if (isAlive)
        {
            health -= p_healthModifer;
            if (health < minHealth)
            {
                health = minHealth;
            }
            //Check is alive
            CheckHealth();
    
        }

    }

    void CheckHealth()
    {
        if (health > 0)
        {
            isAlive = true;
            
        }
        else
        {
            isAlive = false;
            OnDeath?.Invoke();

        }
    }

   
}
