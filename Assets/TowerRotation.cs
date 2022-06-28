using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotation : MonoBehaviour
{
    // Start is called before the first frame update
    private TowerAttack towerAttack;
    public GameObject target;

    // Update is called once per frame

    private void OnEnable()
    {
       // target = towerAttack.unit.currentTarget.gameObject;
    }
    void Update()
    {

        if(target != null)
        {
            Vector3 targetDirection = target.transform.position - transform.position;

            // The step size is equal to speed times frame time.
         //   float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0.0f);

            // Draw a ray pointing at our target in
          //  Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
       
    }
}
