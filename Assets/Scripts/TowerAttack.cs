using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : RangedDamager
{
    // Start is called before the first frame update
    public float attackSpeed;
    [HideInInspector]
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public IEnumerator Attack()
    {
        Debug.Log("Throw Project");

        yield return new WaitForSeconds(attackSpeed);
        DamageTarget();
    }


}
