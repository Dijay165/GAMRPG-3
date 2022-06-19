using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthOverheadUI : MonoBehaviour
{
    [SerializeField] private bool isRevealed = false;
    [SerializeField] private float unrevealTimeOut;
    Coroutine currentTimeOut;
    private Camera cam;
    [SerializeField] private Vector2 positionCorrection = new Vector2(0, 40);
    private RectTransform targetCanvas;
    private RectTransform healthBarTransform;
    [SerializeField] private Image healthFrame;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image delayedBar;
    public Transform objectToFollow;

    float fill;

    public void OnHealthDied(Health p_health)
    {
        if (currentTimeOut != null)
        {
            StopCoroutine(currentTimeOut);
        }

        isRevealed = false;
        healthFrame.gameObject.SetActive(false);
        HealthOverheadUIPool.pool.Release(this);


    }

    public IEnumerator Co_RevealTimeOut()
    {
        yield return new WaitForSeconds(unrevealTimeOut);
        healthFrame.gameObject.SetActive(false);
        isRevealed = false;
    }

    private void Update()
    {
        
        if (isRevealed)
        {
          
            RepositionHealthBar();
    
            

        }
        
    }

    public void SetHealthBarData(Transform p_targetTransform, RectTransform p_healthBarPanel)
    {
        this.targetCanvas = p_healthBarPanel;
        healthBarTransform = GetComponent<RectTransform>();
        objectToFollow = p_targetTransform;
        healthFrame.gameObject.SetActive(false);
        transform.SetParent(p_healthBarPanel, false);

    }
    public void OnHealthChanged(bool p_isAlive, float p_currentHealth, float p_maxHealth)
    {
    
        if (p_isAlive)
        {

            if (!isRevealed)
            {
   
               // RepositionHealthBar();
                healthFrame.gameObject.SetActive(true);
                isRevealed = true;

            }

            fill = p_currentHealth / p_maxHealth;

          
            healthBar.fillAmount = fill;


            if (currentTimeOut != null)
            {
                StopCoroutine(currentTimeOut);
            }
            currentTimeOut = StartCoroutine(Co_RevealTimeOut());
        }


    }
   
    private void Start()
    {
        cam = cam ? cam : Camera.main;

    }
    Vector3 worldToViewportPoint(Vector3 point3D)
    {
        Matrix4x4 P = Camera.main.projectionMatrix;
        Matrix4x4 V = Camera.main.transform.worldToLocalMatrix;
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


        var _result = Camera.main.WorldToViewportPoint(point3D);
        // result == _result

        return _result;
    }

    private void RepositionHealthBar()
    {
        Vector3 newtest = objectToFollow.position;
        var ViewportPositiont = cam.WorldToViewportPoint(objectToFollow.position);
        Vector2 ViewportPosition = worldToViewportPoint(newtest);
        Vector3 p = cam.ViewportToWorldPoint(ViewportPosition);
        //new Vector2(2200,80,2800)
        //Vector2 tre = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(0,100, 0)); // cam.WorldToViewportPoint(new Vector2(2500,2500));
        //Vector2 tree = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(2500,100, 2500)); //cam.WorldToViewportPoint(new Vector2(0, 0));
        //Vector2 treee = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(5000, 100,5000)); //cam.WorldToViewportPoint(new Vector2(5000, 5000));
        //Debug.Log(ViewportPositiont + " - " + ViewportPosition + " - " + p);
        //Debug.Log(tre + " - " + tree + " - " + treee);
    
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * 0.5f)),//
        ((ViewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * 0.5f))); //
        //Debug.Log(WorldObject_ScreenPosition + " - " + targetCanvas.sizeDelta.x);
        //var distance = (cam.transform.position - objectToFollow.position).magnitude;
        //Debug.Log(objectToFollow.ToString() + " - " + ViewportPosition + " - " + WorldObject_ScreenPosition + " - " + distance + " - " + (positionCorrection.y - distance/31f).ToString());
        //WorldObject_ScreenPosition += new Vector2(positionCorrection.x * distance / 22.1248f, positionCorrection.y * distance / 22.1248f);
        //WorldObject_ScreenPosition += new Vector2(positionCorrection.x, positionCorrection.y);


        healthBarTransform.anchoredPosition = WorldObject_ScreenPosition;// - targetCanvas.sizeDelta / 2f;// WorldObject_ScreenPosition;

    }
}
