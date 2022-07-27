using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthOverheadUI : GenericOverheadUI
{

    public Health health;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image delayedBar;
  

    float fill;
    protected override void Deinitialize()
    {

       
        base.Deinitialize();
        health.DeregisterOverheadHealthUI();
        Destroy(gameObject);
        //HealthOverheadUIPool.pool.Release(this);
        //overheadFrame.gameObject.SetActive(false);
    }

    public IEnumerator Co_RevealTimeOut()
    {
        yield return new WaitForSeconds(unrevealTimeOut);
        //HealthOverheadUIPool.pool.Release(this);
        Destroy(gameObject);
        isRevealed = false;
    }
    public void OnHealthDied(Health p_health)
    {
        Deinitialize();
        
    }
    public override void Initialize(Transform p_targetTransform, RectTransform p_healthBarPanel)
    {

        
        base.Initialize(p_targetTransform, p_healthBarPanel);

    }

    private void Update()
    {
        
        if (isRevealed)
        {
      
            if (objectToFollow != null)
                RepositionOverheadUI();
            else
                Destroy(gameObject);
            //HealthOverheadUIPool.pool.Release(this);



        }
        
    }
    public void OnHealthChanged(bool p_isAlive, float p_currentHealth, float p_maxHealth)
    {
    
        if (p_isAlive)
        {

            if (!isRevealed)
            {
   
               // RepositionHealthBar();
                //overheadFrame.gameObject.SetActive(true);
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
   
}
