using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDetection : MonoBehaviour
{
    // Start is called before the first frame update

    private TestStatsHolder structure;
    public float attackRange = 700f;
    private TestStatsHolder targetUnit;
    private TowerAttack towerAttack;
    public GameObject towerHead;
    float delay = 0f;
    public float turnSpeed;

    private void Awake()
    {
        structure = gameObject.GetComponentInParent<TestStatsHolder>();
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
            if (other.gameObject.TryGetComponent<TestStatsHolder>(out TestStatsHolder otherTarget))
            {
                Debug.Log("Hascollide" + gameObject.name);
                bool isAttack = CanAttack(otherTarget);
                if (isAttack)
                {
                    targetUnit = otherTarget;
                    towerAttack.unit.currentTarget = otherTarget.attributes.hp;
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
    public bool CanAttack(TestStatsHolder target)
    {
        bool canAttack = false;
        if (structure.unitFaction != target.unitFaction)
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
