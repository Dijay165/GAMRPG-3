using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public Health targetUnit;
    public Transform target;
    public int damage;
    public float speed;
    public float rotateSpeed = 200f;


    private void Start()
    {
     
    }


    private void Update()
    {
        if (target.transform != null)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator SendHoming()
    {
        //Iirc using while in a coroutine is bad practice
        if(target.transform != null)
        {
            while (Vector3.Distance(target.transform.position, transform.position) > 0.3f)
            {
                Vector3 direction = target.position - transform.position;
                transform.position += (direction).normalized * speed * Time.deltaTime;
                transform.LookAt(target.transform);
                yield return null;
            }
            Debug.Log(gameObject.name + " - " + damage + " - " + targetUnit.gameObject.name);
            targetUnit.SubtractHealth(damage);
            Destroy(gameObject);
        }
        else
        {
            yield return null;
            Destroy(gameObject);
        }
 

    }

    public void Homing()
    {
        if(targetUnit != null)
        {
            StartCoroutine(SendHoming());
        }
        else
        {
            Destroy(gameObject);
        }
    

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<Unit>() == targetUnit)
    //    {
    //        targetUnit.SubtractHealth(damage);
    //    }
    //}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
