using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Hitbox : MonoBehaviour
{
    public Action<Collider> OnTriggerEnteredFunction;
    public Action<Collider> OnTriggerExittedFunction;
    private void OnTriggerEnter(Collider other)
    {

        OnTriggerEnteredFunction.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {

        OnTriggerExittedFunction.Invoke(other);
    }
    
}
