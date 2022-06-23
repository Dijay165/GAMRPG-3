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

    public float attackSpeed = 1.7f;

    MOBAMovement mOBAMovement;

    Attributes attributes;
    
    void Start()
    {
        team = (int)GetComponent<TestStatsHolder>().unitFaction;
        anim = GetComponent<Animator>();
        attributes = GetComponent<Attributes>();
        mOBAMovement = GetComponent<MOBAMovement>();
        damageAmount = attributes.attackDamage;
        attackSpeed = attributes.attackSpeed;
    }

    private void Update()
    {
        if(targetHealth != null)
        {
            float distance = Vector3.Distance(mOBAMovement.agent.transform.position, targetHealth.playersParent.position);
            if (distance < range)
            {
                Debug.Log("In Distance");


                StartCoroutine(MeleeAttack());
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

    public IEnumerator MeleeAttack()
    {
        Debug.Log("Atack");
        anim.SetTrigger("Basic Attack");
        yield return new WaitForSeconds(attackSpeed / ((100 + attackSpeed) * 0.01f));
    }
}
