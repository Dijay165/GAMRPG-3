using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public Health targetUnit;
    public Transform target;
    public int damage;
    public float speed;

    private void Update()
    {
        transform.position = target.position * speed;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Unit>() == targetUnit)
        {
            targetUnit.SubtractHealth(damage);
        }
    }
   
}
