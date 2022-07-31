using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Unit : MonoBehaviour
{
    protected bool isInUse;
    public SpriteRenderer icon;
    public Health currentTarget;

    [HideInInspector]public Health health;
    

    [SerializeField] public Faction unitFaction;
    [SerializeField] public UnitStat unitStat;
    public bool isStun;

    public virtual void AssignTeam()
    {
        if (icon != null)
        {
            icon.sprite = unitStat.iconImage;

            if (unitFaction == Faction.Radiant)
            {
                icon.color = new Color32(0, 255, 0, 255);
            }
            else
            {
                icon.color = new Color32(255, 0, 0, 255);
                
            }
        } 
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(Co_Load());
    }

    protected virtual void OnDisable()
    {
        DeinitializeValues();
    }
    protected virtual void Awake()
    {
        health = GetComponent<Health>();
        if (TryGetComponent<Unit>(out Unit unit))
        {
            unitStat = unit.unitStat;
        }
        
    }

    IEnumerator Co_Load()
    {
        yield return new WaitForSeconds(0.1f);
        InitializeValues();
    }
    protected virtual void InitializeValues()
    {
        AssignTeam();
       
       

       
        health.OnDeathEvent.AddListener(Death);
        isInUse = true;
        GetComponent<Attributes>().ResetValues();
        GetComponent<Health>().ResetValues();
    }

    protected virtual void DeinitializeValues()
    {

        health.DeInitialize();
        //health.OnDeathEvent.RemoveAllListeners();

        isInUse = false;
    }

    public virtual void Death(Health objectHealth = null)
    {
        DeinitializeValues();
     //   Debug.Log(gameObject.name+" Unit dea");
    }

  
}
