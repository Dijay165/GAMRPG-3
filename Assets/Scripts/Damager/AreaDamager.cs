using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamager : MonoBehaviour
{
    private bool temporaryGizmos = false;
    [SerializeField] private int team;
    [SerializeField] private float damageAmount;
    [SerializeField] private float range = 150f;

    [SerializeField] private Transform weapon;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 detectionSize;

    public Health targetHealth;

    private void Start()
    {
        temporaryGizmos = true;
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

    void DamageTarget()
    {
        targetHealth.SubtractHealth(damageAmount);
    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(
                new Vector2(weapon.position.x + (offset.x * transform.localScale.x / Mathf.Abs(transform.localScale.x)),
                            weapon.position.y + (offset.y * transform.localScale.y / Mathf.Abs(transform.localScale.y))),
                new Vector2(detectionSize.x, detectionSize.y),
                Quaternion.identity);//,
                                     //layer);


        //Check when there is a new collider coming into contact with the box
        foreach (Collider selectedHit in hitColliders)
        {

            //Increase the number of Colliders in the array
            if (selectedHit)
            {
                if (selectedHit.gameObject.TryGetComponent(out Health foundHealthComponent))
                {
                    if (!foundHealthComponent.CompareTeam(team))
                    {
                        foundHealthComponent.SubtractHealth(damageAmount);
                    }
                }
            }
        }


    }


    virtual protected void OnDrawGizmos()
    {
        if (temporaryGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
                new Vector2(transform.position.x + (offset.x * transform.localScale.x / Mathf.Abs(transform.localScale.x)),
                            transform.position.y + (offset.y * transform.localScale.y / Mathf.Abs(transform.localScale.y))),
                new Vector2(detectionSize.x, detectionSize.y));

        }



    }
}
