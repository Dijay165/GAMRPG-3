using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MOBAMovement : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;
    Animator anim;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    float rotateSpeed = 1440f;

    TargetedDamager targetedDamager;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        targetedDamager = GetComponent<TargetedDamager>();
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {

                transform.rotation = Quaternion.Lerp(transform.rotation, hit.transform.rotation, (360f / rotateSpeed));

                agent.SetDestination(hit.point);
                anim.SetTrigger("Turn");

                if(hit.transform.gameObject.TryGetComponent(out Structures structures))
                {
                    targetedDamager.targetHealth = structures.health;
                }
            }
            //  anim.SetTrigger("Attack1");
            //anim.SetTrigger("Attack2");
            //  anim.SetBool("dead", true);
        }

        anim.SetFloat("Velocity X", agent.velocity.x, 0.01f, Time.deltaTime);
        anim.SetFloat("Velocity Z", agent.velocity.z, 0.01f, Time.deltaTime);
    }
}
