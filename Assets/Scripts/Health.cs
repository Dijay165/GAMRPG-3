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
    public Unit damager;
    public Level level;
    public Attributes attributes;

    public Death OnDeathEvent = new Death();
    public HealthModify OnHealthModifyEvent = new HealthModify();
    protected HealthOverheadUI healthOverheadUI;
    public float GetHealth()
    {
        return currentHealth;
    }

    // Start is called before the first frame update
    void Awake()
    {

        playersParent = transform;
        level = GetComponent<Level>();
        attributes = GetComponent<Attributes>();    
        ResetValues();

      
    }
    private void OnEnable()
    {
        //Events.OnMiniUIUpdate.AddListener(SubtractHealth);
    }


    private void OnDisable()
    {
        //Events.OnMiniUIUpdate.RemoveListener(SubtractHealth);
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
    public void DeInitialize()
    {
      



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

    public void RegisterOverheadHealthUI()
    {
        if (healthOverheadUI == null)
        {
            healthOverheadUI = HealthOverheadUIPool.pool.Get();
            healthOverheadUI.health = this;
            healthOverheadUI.Initialize(transform, UIManager.instance.overheadUI);
            OnHealthModifyEvent.AddListener(healthOverheadUI.OnHealthChanged);
            OnDeathEvent.AddListener(healthOverheadUI.OnHealthDied);
    

        }
    }
    public void DeregisterOverheadHealthUI()
    {
        if (healthOverheadUI != null)
        {
       
            OnHealthModifyEvent.RemoveListener(healthOverheadUI.OnHealthChanged);
            OnDeathEvent.RemoveListener(healthOverheadUI.OnHealthDied);
            healthOverheadUI.health = null;
            healthOverheadUI = null;

        }
    }
    public void SubtractHealth(Unit p_damager,float p_healthModifer)
    {
        Events.OnPlayerSelect.Invoke();
        damager = p_damager;
        RegisterOverheadHealthUI();

        if (isAlive)
        {
     
            DamageOverhead damageOverhead = DamagedOverheadPool.pool.Get();

            //This is for physical armor 
            //float mitigations = Mathf.CeilToInt(gameObject.GetComponent<Attributes>().totalArmor  / p_healthModifer + (p_healthModifer *
            //    gameObject.GetComponent<Attributes>().level)); 

            damageOverhead.DamageText(p_healthModifer);
            damageOverhead.Initialize(transform, UIManager.instance.overheadUI);
            currentHealth -= Mathf.Clamp(p_healthModifer, 0, maxHealth);

            if (currentHealth < minHealth)
            {
                currentHealth = minHealth;
            }
     
            CheckHealth();
    
        }

    }

    public float MagicResistance(float damage)
    {
        //  float mitigations = 0;


        float totalResistanceMod = 1 * (damage - attributes.totalMagicResistance) * (damage - attributes.totalMagicResistance);
        float reductionMode = 1 * (damage + attributes.magicAttack) * (damage + attributes.magicAttack);
        float finalResistance = 1 - (totalResistanceMod * reductionMode);
        float damageModFromMR = damage - finalResistance;

        ///Debug.Log("Magic Resistance");
       return damageModFromMR;
    }

    public float findDamage(AttackType attacker, float damage)
    {
        float mitigations = 0;
        Debug.Log(gameObject.name);
        Debug.Log(attributes.totalArmor);
        Debug.Log(level.currentLevel);
        switch (attacker)
        {
            case AttackType.Physical:
                mitigations = Mathf.CeilToInt(attributes.totalArmor / damage + (damage *
              level.currentLevel));
                break;
            case AttackType.Magical:

                mitigations = Mathf.CeilToInt(attributes.magicResistance / damage + (damage *
               level.currentLevel));


                break;
        }
        return mitigations;

    }

    void CheckHealth()
    {
        OnHealthModifyEvent.Invoke(isAlive, currentHealth, maxHealth);
        if (currentHealth > 0)
        {
            isAlive = true;
           

        }
        else
        {

            OnDeathEvent.Invoke(this);
            isAlive = false;
           // Debug.Log("Invoke Stuff");
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

    public float CalculateDamage(float baseDamage, WeaponType damageType, ArmorType armorType)
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

    public void Invul(bool b_bool)
    {
        this.invulnerable = b_bool;
    }
}
