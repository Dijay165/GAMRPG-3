using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectPoolsManager : MonoBehaviour
{
    private static ObjectPoolsManager _instance;
    public static ObjectPoolsManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ObjectPoolsManager>();
            }

            return _instance;

        }
    }
    //[HideInInspector] 
    public List<GenericObjectPool> pools = new List<GenericObjectPool>();
    private void Awake()
    {
        _instance = this;

        foreach (GenericObjectPool pool in transform.GetComponentsInChildren(typeof(GenericObjectPool)))
        {
            pools.Add(pool);
        }
    }

    public static GenericObjectPool GetPool(PoolableObject p_prefab)
    {

        foreach (GenericObjectPool pool in ObjectPoolsManager.instance.pools)
        {
            
            if (pool.prefab == p_prefab)
            {
                
                return pool;
            }
        }
        return null;
    }

    public static GenericObjectPool GetPool(System.Type p_type)
    {

        foreach (GenericObjectPool pool in ObjectPoolsManager.instance.pools)
        {

            if (pool.prefab.GetType() == p_type)
            {

                return pool;
            }
        }
        return null;
    }

}
