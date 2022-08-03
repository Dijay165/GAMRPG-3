using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelSkill : MonoBehaviour
{
    // Start is called before the first frame update

    private Health targetHealth;
    public Transform targetTransform;

    public float damage;
    public float speed = 1;
    public AttackType attackType;
    void Start()
    {
        // StartCoroutine(ActivateSkill());
        

    }

    private void OnEnable()
    {
        Skill();
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


    public void Skill()
    {
        Vector3 playerPosition = gameObject.transform.position;
        Vector3 targetPosition = targetHealth.playersParent.transform.position;

        gameObject.transform.position = targetPosition;
        targetHealth.playersParent.transform.position = playerPosition;
    }

    public IEnumerator ActivateSkill()
    {
        Vector3 playerPosition = gameObject.transform.position;
        Vector3 targetPosition = targetHealth.playersParent.transform.position;

        gameObject.transform.position = targetPosition;
        targetHealth.playersParent.transform.position = playerPosition;

        yield return new WaitForSeconds(speed);
        Destroy(gameObject);
    }
}
