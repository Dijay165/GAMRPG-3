using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusArmorAura : MonoBehaviour
{
    //public AttributeModifier attributeModifier;
    [SerializeField] Unit unit;
    //public SphereCollider detector;
    [SerializeField] float auraRadius = 900;
    [SerializeField] int armorAmount = 3; //5 for tier 2 3 4
    [SerializeField] List<GameObject> potentialHero;
    // Start is called before the first frame update

    private void Awake()
    {
        unit = unit ? unit : GetComponent<Unit>();
    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, auraRadius);


        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.layer == LayerMask.NameToLayer("Unit")
            && hitColliders[i].gameObject.CompareTag("Player"))
            {
             
                if (hitColliders[i].gameObject.GetComponent<Unit>() != null)
                {
              
                    if (potentialHero.Count > 0)
                    {
                        for (int ii = 0; ii < potentialHero.Count;)
                        {
                            if (potentialHero[ii] == hitColliders[i].gameObject)
                            {
                                break;
                            }
                            ii++;
                            if (ii >= potentialHero.Count)
                            {
                                if (hitColliders[i].gameObject.TryGetComponent<Health>(out Health hitHealth))
                                {
                                    if (hitHealth.CompareTeam(unit.unitFaction))
                                    {
                                        potentialHero.Add(hitColliders[i].gameObject);
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
                                potentialHero.Add(hitColliders[i].gameObject);
                            }
                        }
                        
                    }
                   


                }
            }
        }

        for (int i = 0; i < potentialHero.Count; i++)
        {
            if (potentialHero[i] != null)
            {
                if (potentialHero[i].TryGetComponent(out AttributeModifier attributeModifier))
                {
                    float distance = Vector3.Distance(transform.position, potentialHero[i].transform.position);

                    if (distance < auraRadius)
                    {

                        if (!attributeModifier.hasBuff)
                        {
                            attributeModifier.armorAmount = armorAmount;
                            attributeModifier.ApplyModification();
                            // Debug.Log("MODIFIER APPLIED " + gameObject.name);
                        }




                    }
                    else
                    {
                        if (attributeModifier.hasBuff)
                        {
                            attributeModifier.armorAmount = armorAmount;
                            attributeModifier.VoidModification();
                            // Debug.Log("MODIFIER APPLIED " + gameObject.name);
                        }
                        potentialHero.Remove(potentialHero[i]);
                    }
                }
               
                
            }
            else
            {
                potentialHero.Remove(potentialHero[i]);
            }


        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, auraRadius);
    }
}
