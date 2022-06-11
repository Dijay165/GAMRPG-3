using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthOverheadUI : PoolableObject
{
    private bool isRevealed;
    [SerializeField] private float unrevealTimeOut;
    Coroutine currentTimeOut;
    private Camera cam;
    [SerializeField] private Vector2 positionCorrection = new Vector2(0, 40);
    private RectTransform targetCanvas;
    private RectTransform healthBarTransform;
    [SerializeField] private Image healthFrame;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image delayedBar;
    private Transform objectToFollow;

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

                RepositionHealthBar();
                healthFrame.gameObject.SetActive(true);
                isRevealed = true;

            }

            fill = p_currentHealth / p_maxHealth;





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


    private void RepositionHealthBar()
    {
        Vector2 ViewportPosition = cam.WorldToViewportPoint(objectToFollow.position);

        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * 0.5f)));

        WorldObject_ScreenPosition += new Vector2(positionCorrection.x, positionCorrection.y);

        healthBarTransform.anchoredPosition = WorldObject_ScreenPosition;

    }
}
