using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDetection : MonoBehaviour
{
    // Start is called before the first frame update

    private Health structureHealth;
    public float attackRange = 700f;
    public List<Unit> targetUnit;
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
        if (targetUnit.Count > 0)
        {
            RemoveNull();
        
            if(targetUnit[0] != null)
            {
                float distance = Vector3.Distance(transform.position, targetUnit[0].transform.position);

                if (distance < attackRange)
                {
                    //    Debug.Log("distance");
                    Vector3 targetDirection = targetUnit[0].transform.position - transform.position;

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
            else
            {
                targetUnit.RemoveAt(0);
                return;
            }
           
        }
           
    }

    private void OnTriggerEnter(Collider other)
    {
        // OnTrigger detection
            if (other.gameObject.TryGetComponent<Unit>(out Unit otherTargetUnit))
            {
              //  Debug.Log("Has Enter");
                bool isAttack = CanAttack(otherTargetUnit);
                if (isAttack)
                {
                    targetUnit.Add(otherTargetUnit);

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


    public void RemoveNull()
    {

        for (int i = 0; i < targetUnit.Count - 1; i++)
            {
                if (targetUnit[i] == null)
                {
                    targetUnit.RemoveAt(i);
                }
            }

    //    randTarget = Random.Range(0, targetUnit.Count - 1);
      //  Debug.Log(gameObject.name + " : Tower " + " randTarg " + randTarget);
    }
}
