using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOBACamera : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;
    private void Start()
    {
        
    }
    private void Update()
    {
        Vector3 pos = transform.parent.transform.localPosition;

        //Uncomment if you want to enable this feature, this is not included in the milestone btw
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            Vector3 dir = transform.InverseTransformDirection(Vector3.right);
            pos += (dir * panSpeed) * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorderThickness)
        {
            Vector3 dir = transform.InverseTransformDirection(Vector3.right);
            pos -= (dir * panSpeed) * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            Vector3 dir = transform.InverseTransformDirection(Vector3.forward);
            pos -= (dir * panSpeed) * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorderThickness)
        {
            Vector3 dir = transform.InverseTransformDirection(Vector3.forward);
            pos += (dir * panSpeed) * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, 0, panLimit.x);
        pos.y = 1000;
        pos.z = Mathf.Clamp(pos.z, 0, panLimit.y);
        transform.parent.transform.localPosition = pos;
    }
}
