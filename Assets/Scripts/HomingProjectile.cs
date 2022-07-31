using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HomingProjectile : MonoBehaviour
{
   
    [SerializeField] private float damage;
    [SerializeField] private float speed;


    private BoxCollider bc;

    private bool isInUse;
    private Health targetHealth;
    private Transform targetTransform;

    Vector3 direction;
    IEnumerator runningDecay;


    [HideInInspector] public WeaponType weaponType;
    [HideInInspector] public ArmorType armorType;
    [HideInInspector] public AttackType attackType;

    Attributes attributes;

    // public UnityEvent beforeDestroy;
    public bool canInflictStatusEffect;

    public GameObject statusEffect;

    private void Awake()
    {

        bc = bc ? bc : GetComponent<BoxCollider>();
        attributes = GetComponent<Attributes>();
    }
    public void InitializeValues(Health p_targetHealth, float p_damage, float p_speed)
    {
        targetHealth = p_targetHealth;
        targetTransform = p_targetHealth.transform;
        damage = p_damage;
        speed = p_speed;
        isInUse = true;
        bc.enabled = false;
    }

    private void OnDestroy()
    {
        // beforeDestroy?.Invoke();
        if (canInflictStatusEffect)
        {
            //Insantiate status effect
            GameObject obj = Instantiate(statusEffect, targetTransform);
            StatusEffect status = obj.gameObject.GetComponent<StatusEffect>();
            status.isInEffect = true;
            status.Initialized(1f, targetTransform);
   
        }
    }

    public void DeinitializeValues()
    {
        isInUse = false;

        Destroy(gameObject);
        //ProjectilePool.pool.Release(this);
    }

    public void DeathDeregisterEvent()
    {
        targetTransform = null;
     
    }

   
    private void Update()
    {
        if (!isInUse) return;

        if (targetTransform != null)
        {
            if (targetTransform.transform != null)
            {
                Vector3 sav = targetTransform.transform.position;
                if (Vector3.Distance(sav, transform.position) > 50f)
                {
                    direction = sav - transform.position;
                    transform.LookAt(targetTransform.transform);

                }
                else
                {
                    //CalcDamage.
                    float modifiedDamage = targetHealth.CalcDamage(damage, weaponType, targetHealth.gameObject.GetComponent<Attributes>().armorType);
   
                    targetHealth.SubtractHealth(modifiedDamage);
                    DeinitializeValues();

                }


            }
        }
        else
        {
            bc.enabled = true;
            if (runningDecay != null)
            {
                StopCoroutine(runningDecay);
                runningDecay = null;
            }
            runningDecay = Co_Decay();
            StartCoroutine(Co_Decay());
        }
        transform.position += (direction).normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        DeinitializeValues();
      
    }

    public IEnumerator Co_Decay()
    {

        yield return new WaitForSeconds(0.5f);
        DeinitializeValues();
    }

}
