using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VengeanceAura : AbilityBase
{
    public float auraRadius;
    [SerializeField] List<GameObject> auraCreeped;
    [SerializeField] Unit unit;

    [SerializeField] List<float> rangeBonus;
    [SerializeField] List<float> damageBonus;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, auraRadius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            

                if (hitColliders[i].gameObject.GetComponent<RangedDamager>() != null)
                {

                    if (auraCreeped.Count > 0)
                    {
                        for (int ii = 0; ii < auraCreeped.Count;)
                        {
                            if (auraCreeped[ii] == hitColliders[i].gameObject)
                            {
                                break;
                            }
                            ii++;
                            if (ii >= auraCreeped.Count)
                            {
                                if (hitColliders[i].gameObject.TryGetComponent<Health>(out Health hitHealth))
                                {
                                    if (hitHealth.CompareTeam(unit.unitFaction))
                                    {
                                        auraCreeped.Add(hitColliders[i].gameObject);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                        if (hitColliders[i].gameObject.TryGetComponent<Health>(out Health hitHealth))
                        {
                            if (hitHealth.CompareTeam(unit.unitFaction))
                            {
                                auraCreeped.Add(hitColliders[i].gameObject);
                            }
                        }

                    }



                
            }
        }

        for (int i = 0; i < auraCreeped.Count; i++)
        {
            if (auraCreeped[i] != null)
            {
                float distance = Vector3.Distance(transform.position, auraCreeped[i].transform.position);
                if (distance < auraRadius)
                {
                   
                    if (auraCreeped[i].GetComponent<DamageModifier>() == null)
                    {
                        DamageModifier damageModifier = auraCreeped[i].AddComponent<DamageModifier>();

                           
                            damageModifier.attackRangeModifier = rangeBonus[skillLevel];
                            damageModifier.attackDamageModifier = damageBonus[skillLevel];
                           // Debug.Log(damageModifier.attackDamageModifier);

                            damageModifier.ApplyModification();
                            // Debug.Log("MODIFIER APPLIED " + gameObject.name);
                       

                    }
                }
                else
                {
                    if (auraCreeped[i].TryGetComponent(out DamageModifier damageModifier))
                    {
                        damageModifier.attackRangeModifier = rangeBonus[skillLevel];
                        damageModifier.attackDamageModifier = damageBonus[skillLevel];
                        damageModifier.RemoveModification();
                        Destroy(auraCreeped[i].GetComponent<DamageModifier>());
                        // Debug.Log("MODIFIER APPLIED " + gameObject.name);
                    }
                    auraCreeped.Remove(auraCreeped[i]);
                }



            }
        }
    }

}

