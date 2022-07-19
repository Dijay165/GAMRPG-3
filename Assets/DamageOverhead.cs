using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageOverhead : MonoBehaviour
{
    public TextMeshProUGUI damageText;
 //   public Transform position;
    public Transform lookAt;
    public Vector3 offset;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        //  DamageTextAnimation();

       
    }


    private void Update()
    {
        if(lookAt != null)
        {
            Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offset);

            if (transform.position != pos)
                transform.position = pos;
        }
      
    }

    public void DamageText(float damage)
    {   
        damageText.text = damage.ToString();
        DamageTextAnimation();
    }

    void DamageTextAnimation()
    {
        damageText.transform.DOMoveY(transform.position.y + 5f, 1).OnComplete(DisableText);
    }

    public void DisableText()
    {
        gameObject.SetActive(false);
        DamagedOverheadPool.pool.Release(this);
    }


}
