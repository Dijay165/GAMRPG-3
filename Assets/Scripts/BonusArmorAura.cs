using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusArmorAura : MonoBehaviour
{
    //public AttributeModifier attributeModifier;
    public Faction unitFaction;
    public SphereCollider detector;
    public float auraRadius = 900;
    public int armorAmount = 3; //5 for tier 2 3 4
    // Start is called before the first frame update
    void Start()
    {
        detector.radius = auraRadius;
    }


    public void OnTriggerEnter(Collider other)
    {
        GameObject potentialHero = other.gameObject.transform.root.gameObject;

        //hero only
        if (TryGetComponent<Unit>(out Unit unit))
        {
      
            if (potentialHero.TryGetComponent<Health>(out Health hitHealth))
            {
                if (!hitHealth.CompareTeam(unitFaction))
                {
   
                    if (potentialHero.GetComponent<Attributes>())
                    {
 
                        if (potentialHero.GetComponent<AttributeModifier>() == null)
                        {
                            AttributeModifier newModifier = potentialHero.AddComponent<AttributeModifier>();
                            newModifier.armorAmount = armorAmount;
                            newModifier.ApplyModification();
                           // Debug.Log("MODIFIER APPLIED");
                        }



                    }
                }
            }
        }
 
    }

    public void OnTriggerExit(Collider other)
    {



        //Debug.Log("MODIFIER DETE " + other.gameObject.transform.root.gameObject.name);
        GameObject potentialHero = other.gameObject.transform.root.gameObject;
        if (potentialHero.GetComponent<Attributes>())
        {
            if (potentialHero.GetComponent<AttributeModifier>() != null)
            {
                Destroy(potentialHero.GetComponent<AttributeModifier>());
           
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
