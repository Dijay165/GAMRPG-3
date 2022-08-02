using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelSkill : MonoBehaviour
{
    // Start is called before the first frame update

    private Health targetHealth;
    private Transform targetTransform;

    public float damage;
    public float speed;
    public AttackType attackType;
    void Start()
    {
        StartCoroutine(ActivateSkill());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializedValues(Health p_targetHealth, float p_damage, float p_speed, AttackType p_attackType)
    {
        targetHealth = p_targetHealth;
        targetTransform = p_targetHealth.transform;
        damage = p_damage;
        speed = p_speed;
        attackType = p_attackType;
    }


    public IEnumerator ActivateSkill()
    {

        yield return new WaitForSeconds(speed);
    }
}
