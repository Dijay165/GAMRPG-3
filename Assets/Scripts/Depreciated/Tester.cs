using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.overheadUI = gameObject.GetComponent<RectTransform>();
    }

    
}
