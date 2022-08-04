using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineSetup : MonoBehaviour
{
    // Start is called before the first frame update

    public static CoroutineSetup instance;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
