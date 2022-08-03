using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSAura : MonoBehaviour
{
    // Start is called before the first frame update

    public PassiveSkill skill;

    public float auraRadius;
    [SerializeField] List<GameObject> auraCreeped;
    // public Unit unit;

    public List<float> rangeBonus;
    public List<float> damageBonus;
    void Start()
    {
        //auraRadius = skill.castRange;
       // rangeBonus = skill.effectDuration;
      //  damageBonus = skill.damage;
        
        //auraRadius = 1200f;

    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, auraRadius);

        for (int i = 0; i < hitColliders.Length; i++)
        {


            if (hitColliders[i].gameObject.GetComponent<RangedDamager>() != null)
            {
                Debug.Log(hitColliders[i].name);
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
                                if (hitHealth.CompareTeam(gameObject.GetComponent<Unit>().unitFaction))
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
                        if (hitHealth.CompareTeam(gameObject.GetComponent<Unit>().unitFaction))
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
                float distance = Vector3.Distance(gameObject.transform.position, auraCreeped[i].transform.position);
                if (distance < auraRadius)
                {

                    if (auraCreeped[i].GetComponent<DamageModifier>() == null)
                    {
                        DamageModifier damageModifier = auraCreeped[i].AddComponent<DamageModifier>();


                        damageModifier.attackRangeModifier = rangeBonus[skill.skillLevel];
                        damageModifier.attackDamageModifier = damageBonus[skill.skillLevel];
                        Debug.Log(damageModifier.attackDamageModifier);

                        damageModifier.ApplyModification();
                        Debug.Log("MODIFIER APPLIED " + auraCreeped[i].gameObject.name);


                    }
                }
                else
                {
                    if (auraCreeped[i].TryGetComponent(out DamageModifier damageModifier))
                    {
                        damageModifier.attackRangeModifier = rangeBonus[skill.skillLevel];
                        damageModifier.attackDamageModifier = damageBonus[skill.skillLevel];
                        damageModifier.RemoveModification();
                        Destroy(auraCreeped[i].GetComponent<DamageModifier>());
                        Debug.Log("REMOVE MODF APPLIED " + auraCreeped[i].gameObject.name);
                    }
                    auraCreeped.Remove(auraCreeped[i]);
                }



            }
        }
    }
}

