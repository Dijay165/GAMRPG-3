using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOBACamera : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;
    private void Update()
    {
        Vector3 pos = transform.position;

        //Uncomment if you want to enable this feature, this is not included in the milestone btw
        if (Input.GetKey("w") ||
            Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") ||
            Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") ||
            Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") ||
            Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, 0, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, 0, panLimit.y);
        transform.position = pos;
    }
}
