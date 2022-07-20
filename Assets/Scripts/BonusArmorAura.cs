using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusArmorAura : MonoBehaviour
{
    //public AttributeModifier attributeModifier;
    public Faction unitFaction;
    //public SphereCollider detector;
    public float auraRadius = 900;
    public int armorAmount = 3; //5 for tier 2 3 4
    public List<GameObject> potentialHero;
    // Start is called before the first frame update



    private void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.GetComponent<Unit>() != null)
        {
            potentialHero.Add(other.gameObject);
           // potentialHero = other.gameObject.transform.root.gameObject;
            //  Debug.Log("Am unit");
            //////hero only


            foreach(GameObject obj in potentialHero)
            {

                if(obj.TryGetComponent<Health>(out Health hitHealth))
                {
                    if (obj.GetComponent<AttributeModifier>() != null)
                    {

                        if (hitHealth.CompareTeam(unitFaction))
                        {
                            AttributeModifier newModifier = obj.GetComponent<AttributeModifier>();

                            if (!newModifier.hasBuff)
                            {
                                newModifier.armorAmount = armorAmount;
                                newModifier.ApplyModification();
                               // Debug.Log("MODIFIER APPLIED " + gameObject.name);
                            }
                        }
                       

                    }
                }
                 
            }

            //if (potentialHero.TryGetComponent<Unit>(out Unit unit))
            //{
            //    Debug.Log("Unit");
            //    if (potentialHero.TryGetComponent<Health>(out Health hitHealth))
            //    {
            //        Debug.Log("Hithealth");
            //        if (hitHealth.CompareTeam(unitFaction))
            //        {
            //            Debug.Log("Unitfaction");


            //            if (potentialHero.GetComponent<AttributeModifier>() != null)
            //                {
            //                    AttributeModifier newModifier = potentialHero.GetComponent<AttributeModifier>();
            //                if (!newModifier.hasBuff)
            //                {
            //                    newModifier.armorAmount = armorAmount;
            //                    newModifier.ApplyModification();
            //                    Debug.Log("MODIFIER APPLIED");
            //                }
                               
            //                }
            //        }
            //    }
            //}
        }

    }

    public void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<Unit>() != null)
        {
            potentialHero.Remove(other.gameObject);

            if (other.TryGetComponent<Health>(out Health hitHealth))
            {
                if (other.GetComponent<AttributeModifier>() != null)
                {

                    if (hitHealth.CompareTeam(unitFaction))
                    {
                        AttributeModifier newModifier = other.GetComponent<AttributeModifier>();

                        newModifier.armorAmount = armorAmount;
                        newModifier.VoidModification();
                    }
                }
            }
        }

            //Debug.Log("MODIFIER DETE " + other.gameObject.transform.root.gameObject.name);
            //GameObject potentialHero = other.gameObject.transform.root.gameObject;
            //if (potentialHero.GetComponent<Attributes>())
            //{
            //  //  Attributes attributes = potentialHero.GetComponent<Attributes>();
            //    if (potentialHero.GetComponent<AttributeModifier>() != null)
            //    {
            //        AttributeModifier newModifier = potentialHero.GetComponent<AttributeModifier>();

            //        newModifier.VoidModification();

            //    }
            //}

        }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    // //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
    //      Gizmos.DrawWireSphere(transform.position, auraRadius);
    //}
}
