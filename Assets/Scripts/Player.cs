using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private HealthOverheadUI healthOverheadUI;
    Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }
    private void OnEnable()
    {
      //  Debug.Log("OnStart");
       

        //PoolableObject healthOverheadUIObject = ObjectPoolsManager.GetPool(typeof(HealthOverheadUI)).pool.Get();
        StartCoroutine(Co_Delay());
        

   //     Debug.Log("OnEnd");
    }


    IEnumerator Co_Delay()
    {
        yield return new WaitForSeconds(0.8f);
        HealthOverheadUI newHealthOverheadUI = HealthOverheadUIPool.pool.Get();

        healthOverheadUI = newHealthOverheadUI;
        //healthOverheadUI = healthOverheadUIObject.GetComponent<HealthOverheadUI>();

        healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
        health.OnHealthModifyEvent.AddListener(healthOverheadUI.OnHealthChanged);
        health.OnDeathEvent.AddListener(healthOverheadUI.OnHealthDied);
    }


}
