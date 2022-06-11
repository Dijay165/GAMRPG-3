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

    void Start()
    {
        team = GetComponent<Health>().team;
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
}
