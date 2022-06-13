using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceStopController : MonoBehaviour
{
    [SerializeField] private KeyCode assignedKey = KeyCode.S;
     private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(assignedKey))
        {
            //stops state
            //anim.Play(); 
            anim.SetTrigger("Turn");
        }
    }
}
