using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class TargetedDamager : MonoBehaviour
{
    private int team;
    [SerializeField] private float damageAmount = 2f;
    [SerializeField] private float range = 150f;

    public Health targetHealth;

    Animator anim;

    float distance;

    MOBAMovement mOBAMovement;
    
    void Start()
    {
        team = GetComponent<Health>().team;
        anim = GetComponent<Animator>();

        mOBAMovement = GetComponent<MOBAMovement>();
    }

    private void Update()
    {
        if(targetHealth != null)
        {
            float distance = Vector3.Distance(mOBAMovement.agent.transform.position, targetHealth.playersParent.position);
            if (distance < range)
            {
                Debug.Log("In Distance");

                anim.SetTrigger("Basic Attack");
        
            }
        }
       
    }
    private void NewTarget(GameObject p_target)
    {

        if (p_target.TryGetComponent(out Health foundHealthComponent))
        {
            if (!foundHealthComponent.CompareTeam(team))
            {
                targetHealth = foundHealthComponent;//.SubtractHealth(damageAmount);
            }
        }

    }

    public void DamageTarget()
    {
        targetHealth.SubtractHealth(damageAmount);
    }

    private void OnDrawGizmos()
    {
        
    }
}
