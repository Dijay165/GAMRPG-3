using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class GenericObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private bool isAlwaysInContainer;
    //[SerializeField]
    //private bool isInactiveInContainer;
    public static GenericObjectPool<T> instance;
    [SerializeField] public Transform container;
    public T prefab;
    public static ObjectPool<T> pool;
    [SerializeField]
    private bool isCollectionCheck;
    [SerializeField]
    private int defaultMaxAmount;
    [SerializeField]
    private int flexibleMaxAmount;
    private void Awake()
    {
        instance = this;
        pool = new ObjectPool<T>(
            CreateObject,
            objectPooled => { objectPooled.gameObject.SetActive(true); },
            ReleaseObject,
            DestroyObject,
            isCollectionCheck
            ,
            defaultMaxAmount
            ,
            flexibleMaxAmount
            )  ;

     //   Debug.Log("Loaded" + gameObject.name);
    }
    public static void Clear()
    {
        for (int i =0; i< instance.container.childCount; i++)
        {
            if (!instance.container.GetChild(i).gameObject.activeSelf)
            {
                Destroy(instance.container.GetChild(i).gameObject);
            }
        }
    }
    T CreateObject()
    {
        var newObject = Instantiate(prefab);
        newObject.gameObject.name = prefab.name + pool.CountActive.ToString(); //Temporary, for tracking purposes
        if (isAlwaysInContainer)
        {
            if (container != null)
            {

                newObject.transform.SetParent(container);
            }
        }

        var newGenericObject = newObject.gameObject.GetComponent<T>();
        return newGenericObject;
        //if (newGenericObject is PoolableObject)
        //{
        //    PoolableObject newPoolableObject = newGenericObject as PoolableObject;
        //    newPoolableObject.SetPool(this);
        //    return newPoolableObject;
        //}
        //else
        //{
        //    Debug.Log("ERROR - NON POOLABLE ");
        //    return null;
        //}


    }

    void GetObject(T p_desiredObject)
    {

        //if (!isInactiveInContainer)
        //{

            p_desiredObject.gameObject.SetActive(true);
        //}
    }

    void ReleaseObject(T p_desiredObject)
    {
        p_desiredObject.transform.position = container.position;

        //if (!isInactiveInContainer)
        //{
            p_desiredObject.gameObject.SetActive(false);
        //}


    }

    void DestroyObject(T p_desiredObject)
    {
        Destroy(p_desiredObject.gameObject);
    }

}
