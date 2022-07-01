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
    float rotateSpeed = 1440f;

    TargetedDamager targetedDamager;

    Faction unitFaction;

    //  public Vector3 lastSavedLocation;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        targetedDamager = GetComponent<TargetedDamager>();
        if (TryGetComponent<Unit>(out Unit unit))
        {
            unitFaction = unit.unitFaction;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            HeroMove();
        }
          
       
    }

    public void HeroMove()
    {
       
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {

                transform.rotation = Quaternion.Lerp(transform.rotation, hit.transform.rotation, (360f / rotateSpeed));

                agent.SetDestination(hit.point);
                anim.SetTrigger("Turn");
                
                //Tempt for structure only, make this dynamic 
                
                if (hit.transform.gameObject.TryGetComponent(out Health health))
                {
                    if (!health.CompareTeam(unitFaction))
                    {
                        targetedDamager.targetHealth = health;
                        Debug.Log("Can Target");
                    }
                    else
                    {
                     //   Debug.Log("Cannot Target");
                    }
                 
                }
            
        }

        anim.SetFloat("Velocity X", agent.velocity.x, 0.01f, Time.deltaTime);
        anim.SetFloat("Velocity Z", agent.velocity.z, 0.01f, Time.deltaTime);
    }
}
