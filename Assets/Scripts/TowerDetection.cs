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
    float delay = 0f;

    private void Awake()
    {
        structure = gameObject.GetComponentInParent<TestStatsHolder>();
        towerAttack = gameObject.GetComponentInParent<TowerAttack>();
    }

    private void Update()
    {
        if(targetUnit != null)
        {
             StartCoroutine(towerAttack.Attack());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
      //  Debug.Log(other.name);
        if (targetUnit == null)
        {
            if (other.gameObject.TryGetComponent<TestStatsHolder>(out TestStatsHolder otherTarget))
            {
                bool isAttack = CanAttack(otherTarget);
                if (isAttack)
                {
                    Debug.Log("Yes");
                    targetUnit = otherTarget;
                    towerAttack.unit.currentTarget = otherTarget.attributes.hp;
                   // StartCoroutine(towerAttack.Attack());
                }
                else
                {
                    Debug.Log("No");
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


    public bool CanAttack(TestStatsHolder target)
    {
        bool canAttack = false;
        if (structure.unitFaction != target.unitFaction)
        {
            Debug.Log("CanAttack");
            canAttack = true;
        }
        else
        {
            Debug.Log("CannotAttack");
            canAttack = false;
        }
        return canAttack;
    }

}
