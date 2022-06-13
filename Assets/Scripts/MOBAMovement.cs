using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MOBAMovement : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    [SerializeField]
    float rotateSpeed;

    float rotateVelocity;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
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
                agent.SetDestination(hit.point);

                //Quaternion rotationLookAt = Quaternion.LookRotation(hit.point - transform.position);
                //float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationLookAt.eulerAngles.y,
                //    ref rotateVelocity, rotateSpeed * (Time.deltaTime * 5));

                //transform.eulerAngles = new Vector3(0, rotationY, 0);

                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, hit.point, rotateSpeed * Time.deltaTime, 0.0f));
            }
          //  anim.SetTrigger("Attack1");
            //anim.SetTrigger("Attack2");
          //  anim.SetBool("dead", true);
        }

        anim.SetFloat("Velocity X", agent.velocity.x, 0.01f, Time.deltaTime);
        anim.SetFloat("Velocity Z", agent.velocity.z, 0.01f, Time.deltaTime);
    }
}
