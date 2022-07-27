using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDetection : MonoBehaviour
{
    // Start is called before the first frame update

    private Health structureHealth;
    public float attackRange = 700f;
    //public List<Unit> targetUnit;
    public Unit targetUnit;
    private TowerAttack towerAttack;
    public GameObject towerHead;
    float delay = 0f;
    public float turnSpeed;
    int randTarget;

    private void Awake()
    {
        structureHealth = gameObject.GetComponentInParent<Health>();
        towerAttack = gameObject.GetComponentInParent<TowerAttack>();
    }

    private void Update()
    {
        if (!targetUnit)
        {
            if (Physics.SphereCast(transform.position, 5, transform.forward, out RaycastHit hit, 10))
            {
                if (hit.collider.gameObject.TryGetComponent<Unit>(out Unit otherTargetUnit))
                {
                    bool isAttack = CanAttack(targetUnit);
                    if (isAttack)
                    {
                        targetUnit = otherTargetUnit;
                    }

                }


            }
            else
            {

            }
        }
        else if (targetUnit)
        {
            if (Time.time >= delay)
            {
                towerAttack.unit.currentTarget = targetUnit.health;
               
                float distance = Vector3.Distance(transform.position, targetUnit.transform.position);

                if (distance < attackRange)
                {
                    Vector3 targetDirection = targetUnit.transform.position - transform.position;

                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0.0f);

                    towerHead.transform.rotation = Quaternion.LookRotation(newDirection);

                
                    //  towerAttack.Attack();
                    StartCoroutine(towerAttack.Attack());
                    delay = Time.time + 1f / towerAttack.attackSpeed;
                }
            }
                  

            
          
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
