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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    public void Initialization()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
