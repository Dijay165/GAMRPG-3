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
    Vector3 direction;
    [SerializeField] private BoxCollider bc;
    IEnumerator runningDecay;
    private void Start()
    {
        if (target.transform == null)
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        if (target != null)
        {
            if (target.transform != null)
            {
                Vector3 sav = target.transform.position;
                if (Vector3.Distance(sav, transform.position) > 50f)
                {


                    direction = sav - transform.position;
                 
                    transform.LookAt(target.transform);






                }
                else
                { //Debug.Log(gameObject.name + " - " + damage + " - " + targetUnit.gameObject.name);
                    targetUnit.SubtractHealth(damage);

                }


            }
        }
        else
        {
            bc.enabled = true;
            if (runningDecay != null)
            {
                StopCoroutine(runningDecay);
                runningDecay = null;
            }
            runningDecay = Co_Decay();
            StartCoroutine(Co_Decay());
         


        }
        transform.position += (direction).normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (runningDecay != null)
        //{
        //    StopCoroutine(runningDecay);
        //    runningDecay = null;
        //}
        Destroy(gameObject);
    }

    public IEnumerator Co_Decay()
    {
        Debug.Log("coroutine start");
        yield return new WaitForSeconds(2f);
        Debug.Log("DESTROY");
        Destroy(gameObject);
    }

    //public IEnumerator SendHoming()
    //{
    //    //Iirc using while in a coroutine is bad practice
    //    if(target.transform != null)
    //    {
    //        Vector3 sav = target.transform.position;
    //        while (Vector3.Distance(sav, transform.position) > 50f)
    //        {
              
    //            if (target != null)
    //            {
    //                Vector3 direction = sav - transform.position;
    //                transform.position += (direction).normalized * speed * Time.deltaTime;
    //                transform.LookAt(target.transform);
    //                yield return null;
    //            }
    //            else
    //            {
    //                Destroy(gameObject);
    //                yield return null;
    //            }
                
    //        }
    //        //Debug.Log(gameObject.name + " - " + damage + " - " + targetUnit.gameObject.name);
    //        targetUnit.SubtractHealth(damage);
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        yield return null;
    //        Destroy(gameObject);
    //    }
 

    //}

    public void Homing()
    {
        if(targetUnit != null)
        {
            //StartCoroutine(SendHoming());
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
