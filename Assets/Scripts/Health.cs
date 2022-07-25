using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;
using UnityEngine.Events;
using System;

public class Death : UnityEvent<Health> { }
public class HealthModify : UnityEvent<bool, float, float> { }

public class Health : MonoBehaviour
{
    public Transform playersParent;
    public bool invulnerable = false;
    int team;
    [HideInInspector]public bool isAlive;
    public float currentHealth;
    [SerializeField] private float minHealth;
     public float maxHealth;
    public float healthRegen;

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
        
        ResetValues();

      
    }
    private void OnEnable()
    {
        Events.OnMiniUIUpdate.AddListener(SubtractHealth);
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
            healthRegen = unit.unitStat.healthRegeneration;
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
           // Debug.Log("restore health");
            currentHealth += Mathf.CeilToInt(Mathf.Clamp(p_healthModifer, 0, maxHealth));
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
           
            //   currentHealth -= p_healthModifer;

            DamageOverhead damageOverhead = DamagedOverheadPool.pool.Get();
            damageOverhead.lookAt = gameObject.transform;

            //float mitigations = gameObject.GetComponent<Attributes>().totalArmor / 
            //    (p_healthModifer * (400 * gameObject.GetComponent<Attributes>().level));

            float mitigations = Mathf.CeilToInt(gameObject.GetComponent<Attributes>().totalArmor  / p_healthModifer + (p_healthModifer *
                gameObject.GetComponent<Attributes>().level));



            damageOverhead.DamageText(mitigations);

            currentHealth -= Mathf.Clamp(mitigations, 0, maxHealth);

            ///Debug.Log(mitigations);


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
            OnDeathEvent.Invoke(this);
            isAlive = false;
         //   Debug.Log("Invoke Stuff");
        }
    }
   

    public static Dictionary<WeaponType, Dictionary<ArmorType, Func<float, float>>> damageTypes = new Dictionary<WeaponType, Dictionary<ArmorType, Func<float, float>>>() {
            {WeaponType.Basic, new Dictionary<ArmorType, Func<float, float>> {
                { ArmorType.Basic, baseDamage => { return baseDamage * 1f; }},
                { ArmorType.Fortified, baseDamage => { return baseDamage * 0.70f; }},
                { ArmorType.Hero, baseDamage => { return baseDamage * 0.75f; }}
            }},
            {
                WeaponType.Hero, new Dictionary<ArmorType, Func<float, float>> {
                 { ArmorType.Basic, baseDamage => { return baseDamage * 1f; }},
                { ArmorType.Fortified, baseDamage => { return baseDamage * 0.5f; }},
                { ArmorType.Hero, baseDamage => { return baseDamage * 1f; }}
            }},
            {
              WeaponType.Pierce, new Dictionary<ArmorType, Func<float, float>> {
                 { ArmorType.Basic, baseDamage => { return baseDamage * 1.5f; }},
                { ArmorType.Fortified, baseDamage => { return baseDamage * 0.35f; }},
                { ArmorType.Hero, baseDamage => { return baseDamage * 0.5f; }}
            }},
        {
              WeaponType.Siege, new Dictionary<ArmorType, Func<float, float>> {
                 { ArmorType.Basic, baseDamage => { return baseDamage * 1f; }},
                { ArmorType.Fortified, baseDamage => { return baseDamage * 0.5f; }},
                { ArmorType.Hero, baseDamage => { return baseDamage * 1f; }}
            }},
        };

    public float CalcDamage(float baseDamage, WeaponType damageType, ArmorType armorType)
    {
        Dictionary<ArmorType, Func<float, float>> damageTypeDictionary = damageTypes[damageType];
        if (damageTypeDictionary.ContainsKey(armorType))
        {
        //    Debug.Log("Works??");
            return damageTypeDictionary[armorType](baseDamage);
        }
        else
        {
            //Debug.Log("Not Works??");
            return baseDamage;
        }
    }

    public void HealthRegeneration()
    {
        if (isAlive)
        {
            currentHealth += healthRegen;
        }
    }
}
