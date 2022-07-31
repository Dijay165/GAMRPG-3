using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TowerAttack))]
public class Tower : Structures
{

    private Health structureHealth;
    public float attackRange = 700f;
    //public List<Unit> targetUnit;
    public Unit targetUnit;
    private TowerAttack towerAttack;
    public GameObject towerHead;
    float delay = 0f;
    public float turnSpeed;
    int randTarget;

    protected override void Awake()
    {
        base.Awake();
        structureHealth = gameObject.GetComponentInParent<Health>();
        towerAttack = gameObject.GetComponentInParent<TowerAttack>();
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void Update()
    {
        if (!this.isStun)
        {
            if (!targetUnit)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);


                for (int i = 0; i < hitColliders.Length; i++)
                {
                    // Debug.Log(gameObject.name + " - NAME" + hitColliders[i].gameObject.name + hitColliders[i].gameObject.layer.ToString());
                    if (hitColliders[i].gameObject.layer == LayerMask.NameToLayer("Unit"))
                    {
                        if (hitColliders[i].gameObject.TryGetComponent<Unit>(out Unit otherTargetUnit))
                        {
                            bool isAttack = CanAttack(otherTargetUnit);
                            //Debug.Log(gameObject.name + " - " + isAttack);
                            if (isAttack)
                            {
                                targetUnit = otherTargetUnit;
                                towerAttack.unit.currentTarget = targetUnit.health;
                                break;
                            }

                        }
                    }

                }





            }
            else if (targetUnit)
            {
                if (Time.time >= delay)
                {
                    if (towerAttack.unit.currentTarget.isAlive)
                    {
                        float distance = Vector3.Distance(transform.position, targetUnit.transform.position);

                        if (distance < attackRange)
                        {
                            Vector3 targetDirection = targetUnit.transform.position - transform.position;

                            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0.0f);
                            //Debug.Log(newDirection);
                            //Debug.Log(gameObject.name);
                            //Debug.Log(towerHead);
                            towerHead.transform.rotation = Quaternion.LookRotation(newDirection);


                            //  towerAttack.Attack();
                            StartCoroutine(towerAttack.Attack());
                            delay = Time.time + 1f / towerAttack.attackSpeed;
                        }
                    }
                    else
                    {
                        targetUnit = null;
                        towerAttack.unit.currentTarget = null;
                    }


                }




            }




        }
        else
        {
            Debug.Log("Stun");
        }

    }

    public bool CanAttack(Unit targetUnit)
    {
        bool canAttack = false;
        if (!structureHealth.CompareTeam(targetUnit.unitFaction))
        {
            // Debug.Log("CanAttack");
            canAttack = true;
        }
        else
        {
            //  Debug.Log("CannotAttack");
            canAttack = false;
        }
        return canAttack;
    }

}
