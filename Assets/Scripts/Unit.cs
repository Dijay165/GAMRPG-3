using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Unit : MonoBehaviour
{
    protected bool isInUse;
    public SpriteRenderer icon;
    public Health currentTarget;
    public Level level;
    public int goldReward;
    public List<Unit> nearbyEnemyHeroes = new List<Unit>();
    


    [HideInInspector]public Health health;


    [SerializeField] public Faction unitFaction;
    [SerializeField] public UnitStat unitStat;
    [SerializeField] public Attributes attribute;
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
        if (TryGetComponent<Attributes>(out Attributes foundAttribute))
        {
            attribute = foundAttribute;
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
    public void FindNearbyHeroes(float p_radius)
    {
        nearbyEnemyHeroes.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, p_radius);
        foreach (var hitCollider in hitColliders)
        {
            //if enemy is within its detection radius, it sees enemy
            if (hitCollider.gameObject != gameObject)
            {

                if (hitCollider.gameObject.TryGetComponent(out Hero unit))
                {
                    if (unit.unitFaction != unitFaction)
                    {
                        nearbyEnemyHeroes.Add(unit);
                    }
                  
                }




            }
        }
    }

    public virtual void Death(Health objectHealth = null)
    {
        DeinitializeValues();
        //level.RewardExp(); JERRY GO BACK TO THIS
     
      
  


    }

}
