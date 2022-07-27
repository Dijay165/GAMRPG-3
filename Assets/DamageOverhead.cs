using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageOverhead : GenericOverheadUI
{
    public TextMeshProUGUI damageText;

    public float startTime;
    public float disappearTime;
    public float currentOffset = 1f;
    public float offsetRate = 1f;
    public Vector3 offset;


    protected override void Deinitialize()
    {


        base.Deinitialize();
        Destroy(gameObject);
        //DamagedOverheadPool.pool.Release(this);
    }

    private void Update()
    {
        if (startTime < disappearTime)
        {
            
            RepositionOverheadUI(new Vector2(0, currentOffset));
            startTime = Time.time;
            currentOffset += offsetRate;
        }
        else
        {
            Deinitialize();
        }
    


    }

    
    public override void Initialize(Transform p_targetTransform, RectTransform p_healthBarPanel)
    {


        objectToFollow = p_targetTransform;
        lastObjectToFollowPosition = p_targetTransform.position;

        base.Initialize(p_targetTransform, p_healthBarPanel);
        startTime = Time.time;
        disappearTime = Time.time + 5f;
        RepositionOverheadUI();
        DamageTextAnimation();
    }
    public void DamageText(float damage)
    {   
        damageText.text = damage.ToString();
      
    }

    void DamageTextAnimation()
    {
        //overheadTransform.DOAnchorPosY(overheadTransform.anchoredPosition.y + 5f, 1,false).OnComplete(DisableText);
        //RepositionOverheadUI(new Vector2(0,0.5f));
    }

    public void DisableText()
    {
       
        //DamagedOverheadPool.pool.Release(this);
    }


}
