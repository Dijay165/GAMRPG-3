using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusArmorAura : MonoBehaviour
{
    //public AttributeModifier attributeModifier;
    public SphereCollider detector;
    public float auraRadius = 900;
    public int armorAmount = 3; //5 for tier 2 3 4
    // Start is called before the first frame update
    void Start()
    {
        detector.radius = auraRadius;
    }

    // Update is called once per frame
    void Update()
    {
        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, auraRadius);
        ////if enemy is within its detection radius, it sees enemy
        //foreach (var hitCollider in hitColliders)
        //{
            
           
        //    Debug.Log("MODIFIER DETE " + hitCollider.gameObject.transform.root.gameObject.name);
        //    GameObject potentialHero = hitCollider.gameObject.transform.root.gameObject;
        //    if (potentialHero.GetComponent<Attributes>())
        //    {
        //        Debug.Log("hero DETE");
        //        //AttributeModifier newModifier = Instantiate(attributeModifier, potentialHero.transform);
        //        //newModifier.armorAmount = armorAmount;
        //        //newModifier.ApplyModification();
        //        if (potentialHero.GetComponent <AttributeModifier>() == null)
        //        {
        //            AttributeModifier newModifier = potentialHero.AddComponent<AttributeModifier>();
        //            newModifier.armorAmount = armorAmount;
        //            newModifier.ApplyModification();
        //            Debug.Log("MODIFIER APPLIED");
        //        }
                


        //    }


        //}

   
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject potentialHero = other.gameObject.transform.root.gameObject;

        //hero only
        if (potentialHero.GetComponent<TestStatsHolder>() != null)
        {
            Health hitHealth = potentialHero.GetComponent<Health>();
            if (hitHealth != null)
            {
                if (potentialHero.GetComponent<TestStatsHolder>().unitFaction != GetComponent<TestStatsHolder>().unitFaction)
                {
                   
                    //Debug.Log("MODIFIER DETE " + other.gameObject.transform.root.gameObject.name);

                    if (potentialHero.GetComponent<Attributes>())
                    {
                      //  Debug.Log("hero DETE");
                        //AttributeModifier newModifier = Instantiate(attributeModifier, potentialHero.transform);
                        //newModifier.armorAmount = armorAmount;
                        //newModifier.ApplyModification();
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
        
        //Health hitHealth = potentialHero.GetComponent<Health>();
        //if (hitHealth != null)
        //{
        //    if (hitHealth.CompareTeam(gameObject.GetComponent<Health>().team))
        //    {
        //        Debug.Log("MODIFIER DETE " + other.gameObject.transform.root.gameObject.name);

        //        if (potentialHero.GetComponent<Attributes>())
        //        {
        //            Debug.Log("hero DETE");
        //            //AttributeModifier newModifier = Instantiate(attributeModifier, potentialHero.transform);
        //            //newModifier.armorAmount = armorAmount;
        //            //newModifier.ApplyModification();
        //            if (potentialHero.GetComponent<AttributeModifier>() == null)
        //            {
        //                AttributeModifier newModifier = potentialHero.AddComponent<AttributeModifier>();
        //                newModifier.armorAmount = armorAmount;
        //                newModifier.ApplyModification();
        //                Debug.Log("MODIFIER APPLIED");
        //            }



        //        }
        //    }
        //}




    }

    public void OnTriggerExit(Collider other)
    {



        //Debug.Log("MODIFIER DETE " + other.gameObject.transform.root.gameObject.name);
        GameObject potentialHero = other.gameObject.transform.root.gameObject;
        if (potentialHero.GetComponent<Attributes>())
        {
            //Debug.Log("hero DETE");
            //AttributeModifier newModifier = Instantiate(attributeModifier, potentialHero.transform);
            //newModifier.armorAmount = armorAmount;
            //newModifier.ApplyModification();
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
