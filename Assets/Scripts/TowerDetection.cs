using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDetection : MonoBehaviour
{
    // Start is called before the first frame update

    private Health structureHealth;
    public float attackRange = 700f;
    private Unit targetUnit;
    private TowerAttack towerAttack;
    public GameObject towerHead;
    float delay = 0f;
    public float turnSpeed;

    private void Awake()
    {
        structureHealth = gameObject.GetComponentInParent<Health>();
        towerAttack = gameObject.GetComponentInParent<TowerAttack>();
    }

    private void Update()
    {
        if (targetUnit != null)
        {

            float distance = Vector3.Distance(transform.position, targetUnit.transform.position);
          //  Debug.Log(distance);
            if (distance < attackRange)
            {
            //    Debug.Log("distance");
                Vector3 targetDirection = targetUnit.transform.position - transform.position;

                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0.0f);

                towerHead.transform.rotation = Quaternion.LookRotation(newDirection);

                if (Time.time >= delay)
                {
                    //  towerAttack.Attack();
                    StartCoroutine(towerAttack.Attack());
                    delay = Time.time + 1f / towerAttack.attackSpeed;
                }
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        // OnTrigger detection

      
        if (targetUnit == null)
        {
            if (other.gameObject.TryGetComponent<Unit>(out Unit otherTargetUnit))
            {
                Debug.Log("Hascollide" + gameObject.name);
                bool isAttack = CanAttack(otherTargetUnit);
                if (isAttack)
                {
                    targetUnit = otherTargetUnit;
                    towerAttack.unit.currentTarget = otherTargetUnit.health;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        
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
