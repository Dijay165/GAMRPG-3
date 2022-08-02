using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float projectileSpeed;

    [SerializeField] private float damage;
    [SerializeField] private float speed;

    private BoxCollider bc;
    private Health targetHealth;
    private float totalDistance = 0f;

    private Vector3 previousPosition;
    public GameObject armorReduction;

    public float waveTravelDistance;

    private GameObject par;
    void Start()
    {
        previousPosition = transform.position;
        par = transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        //float distanceThisFrame = Vector3.Distance(transform.position, previousPosition);
        if (totalDistance < waveTravelDistance)
        {
            totalDistance +=  Time.deltaTime * projectileSpeed;
            //totalDistance += distanceThisFrame;
          
           // Debug.Log("Distance this frame: " + distanceThisFrame.ToString() + "   Total Distance: " + totalDistance.ToString());
        }
        else
        {
            Destroy(par);
        }



        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);

    }

    public void Initialization(Health p_targetHealth, float p_damage)
    {
        targetHealth = p_targetHealth;
      
        damage = p_damage;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Unit>(out Unit unit))
        {
            // Debug.Log(unit.name);
            if (!unit.CompareTag("Player"))
            {
                GameObject obj = Instantiate(armorReduction);

                StatusEffect status = obj.gameObject.GetComponent<StatusEffect>();

                status.isInEffect = true;
                status.Initialized(1f, unit.transform);
                status.gameObject.transform.SetParent(unit.transform);
                Debug.Log("Collide with something: " + unit.gameObject.name);
            }
        }
    }
}
