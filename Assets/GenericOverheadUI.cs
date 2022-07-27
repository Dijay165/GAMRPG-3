using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GenericOverheadUI : MonoBehaviour
{
    [SerializeField] protected bool isRevealed = false;
    [SerializeField] protected float unrevealTimeOut;
    protected Coroutine currentTimeOut;
    protected Camera cam;
    [SerializeField] protected Vector2 positionCorrection = new Vector2(0, 40);
    protected RectTransform targetCanvas;
    protected RectTransform overheadTransform;
    [SerializeField] protected bool alwaysFollow;
    protected Transform objectToFollow;
    [SerializeField] protected Vector3 lastObjectToFollowPosition;
    protected virtual void Deinitialize()
    {
        if (currentTimeOut != null)
        {
            StopCoroutine(currentTimeOut);
        }

        isRevealed = false;
      


    }

   

    public virtual void Initialize(Transform p_targetTransform, RectTransform p_healthBarPanel)
    {
        targetCanvas = p_healthBarPanel;
     
        
        objectToFollow = p_targetTransform;
        lastObjectToFollowPosition = p_targetTransform.position;
        
       
        //overheadFrame.gameObject.SetActive(false);
        
        transform.SetParent(p_healthBarPanel, false);
        

    }
   
    protected virtual void Awake()
    {
        cam = cam ? cam : Camera.main;
        overheadTransform = GetComponent<RectTransform>();

    }
    protected Vector3 WorldToViewportPoint(Vector3 point3D)
    {
        Matrix4x4 P = cam.projectionMatrix;
        Matrix4x4 V = cam.transform.worldToLocalMatrix;
        Matrix4x4 VP = P * V;

        Vector4 point4 = new Vector4(point3D.x, point3D.y, point3D.z, 1.0f);  // turn into (x,y,z,1)
        Vector4 result4 = VP * point4;  // multiply 4 components

        Vector3 result = result4;  // store 3 components of the resulting 4 components

        // normalize by "-w"
        result /= -result4.w;

        // clip space => view space
        result.x = result.x / 2 + 0.5f;
        result.y = result.y / 2 + 0.5f;

        // "The z position is in world units from the camera."
        result.z = -result4.w;


        var _result = cam.WorldToViewportPoint(point3D);
        // result == _result

        return _result;
    }

    protected void RepositionOverheadUI(Vector2 offset = default(Vector2))
    {

        Vector3 newtest = Vector3.zero;
       
        if (!alwaysFollow)
        {
           
            newtest = lastObjectToFollowPosition;
            

        }
        else
        {
            if (objectToFollow != null)
            {
                newtest = objectToFollow.position;
                lastObjectToFollowPosition = newtest;
            }
            else
            {
                newtest = lastObjectToFollowPosition;
            }
        }    


        Vector2 ViewportPosition = WorldToViewportPoint(newtest);
 

        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * 0.5f)),//
        ((ViewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * 0.5f))); //


        overheadTransform.anchoredPosition = WorldObject_ScreenPosition + offset;// - targetCanvas.sizeDelta / 2f;// WorldObject_ScreenPosition;
           
        

    }
}
