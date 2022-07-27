using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Health))]
public class TargetedDamager : MonoBehaviour
{
    private int team;
    [SerializeField] private float damageAmount = 2f;
    [SerializeField] private float range = 150f;

    public Health targetHealth;
  
    private NavMeshAgent agent;
    Animator anim;

    float attackRate;

    public float attackTime; 

    Attributes attributes;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (TryGetComponent<Unit>(out Unit unit))
        {
            team = (int)unit.unitFaction;
   
        }
        anim = GetComponent<Animator>();
        attributes = GetComponent<Attributes>();
 
        attributes.ResetValues();
        damageAmount = attributes.attackDamage;
        //   attackSpeed = attributes.totalAttackSpeed;

        attackRate = (attributes.baseAttackSpeed + attributes.totalAttackSpeed + attributes.agiFlatModifiers) / (100 * attributes.baseAttackSpeed);
        attackTime = 1 / attackRate;

    }

    private void Update()
    {
        if(targetHealth != null)
        {
            float distance = Vector3.Distance(agent.transform.position, targetHealth.playersParent.position);
            if (distance < range)
            {
             //  Debug.Log("In Distance");


                StartCoroutine(MeleeAttack());
            }
        }
    }
    public void NewTarget(GameObject p_target)
    {

        if (p_target.TryGetComponent(out Health foundHealthComponent))
        {
            if (!foundHealthComponent.CompareTeam(team))
            {
                if (!foundHealthComponent.invulnerable)
                {
                    targetHealth = foundHealthComponent;//.SubtractHealth(damageAmount);
                }
               
            }
        }

    }

    public void DamageTarget()
    {
        
        float modifiedDamage = targetHealth.CalcDamage(damageAmount, attributes.weaponType, targetHealth.gameObject.GetComponent<Attributes>().armorType);

        targetHealth.SubtractHealth(modifiedDamage);
    }

    public IEnumerator MeleeAttack()
    {
        //Debug.Log("Atack");
        anim.SetTrigger("Basic Attack");
        Debug.Log(attackTime);
        yield return new WaitForSeconds(attackTime);
    }
}
