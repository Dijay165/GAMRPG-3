using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageOverhead : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public Transform position; 

    private void OnEnable()
    {
      //  DamageTextAnimation();
    }


    void DamageText(float damage)
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
