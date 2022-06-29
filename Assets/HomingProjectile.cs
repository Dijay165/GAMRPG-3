using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HomingProjectile : MonoBehaviour
{
   
    [SerializeField] private int damage;
    [SerializeField] private float speed;


    [SerializeField] private BoxCollider bc;

    private bool isInUse;
    private Health targetHealth;
    private Transform targetTransform;

    Vector3 direction;
    IEnumerator runningDecay;

    private void Awake()
    {
        bc = bc ? bc : GetComponent<BoxCollider>();
    }
    public void InitializeValues(Health p_targetHealth, int p_damage, float p_speed)
    {
        targetHealth = p_targetHealth;
        targetTransform = p_targetHealth.transform;
        damage = p_damage;
        speed = p_speed;
        isInUse = true;
        bc.enabled = false;
    }

    public void DeinitializeValues()
    {
        isInUse = false;
        ProjectilePool.pool.Release(this);
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
                    targetHealth.SubtractHealth(damage);
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
