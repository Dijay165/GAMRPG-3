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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }
}
