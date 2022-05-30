using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class MinimapSignal : MonoBehaviour
{
    public int team;
    public int id;
    public Sprite icon;
    public Vector2 iconSize;
    public GameObject mapObject;

    protected Vector3 normalized, mapped;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        mapObject = gameObject;
        gameObject.GetComponent<Health>().OnDeath += DeregisterSignal;
        StartCoroutine(Co_CheckIfComplete());
    }

    private void OnDisable()
    {
        gameObject.GetComponent<Health>().OnDeath -= DeregisterSignal;
    }

    IEnumerator Co_CheckIfComplete()
    {
        yield return new WaitUntil(() => MinimapManager.instance != null);
        RegisterNewSignal();
    }

    protected virtual void RegisterNewSignal()
    {
        //Assign unique id
        bool isIDUnique = false;
        while (!isIDUnique)
        {
            int newID = UnityEngine.Random.Range(0, 10000);

            if (MinimapManager.instance.miniMapIcons.Count > 0)
            {
                for (int i = 0; i < MinimapManager.instance.miniMapIcons.Count;)
                {
                    MinimapIcon selectedGlobalIcon = MinimapManager.instance.miniMapIcons[i];
                    if (selectedGlobalIcon.id == newID)
                    {
                        break;
                    }
                    i++;
                    if (i == MinimapManager.instance.miniMapIcons.Count)
                    {
                        isIDUnique = true;
                        id = newID;

                    }
                }
            }
            else
            {
                isIDUnique = true;
                id = newID;
            }

        }


        normalized = Divide(
      MinimapManager.instance.map3dParent.InverseTransformPoint(mapObject.transform.position),
      MinimapManager.instance.mapCenter.position - MinimapManager.instance.map3dParent.position
      );
        normalized.y = normalized.z;

        //position
        mapped = Multiply(normalized, MinimapManager.instance.minimapCenter);
        mapped.z = 0;


        MinimapIcon newIcon = new MinimapIcon();
        newIcon.team = team;

        newIcon.id = id;
        newIcon.icon = icon;
        newIcon.iconSize = iconSize;
        newIcon.minimapPosition = mapped;
        newIcon.isSeenByOpposingTeam = false;

        MinimapManager.instance.miniMapIcons.Add(newIcon);
        MinimapManager.instance.OnMinimapSignalRegistered.Invoke(newIcon);

    }

    void DeregisterSignal()
    {

        MinimapManager.instance.OnDeregistered.Invoke(id);
    }

    protected virtual Vector3 Divide(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    protected virtual Vector3 Multiply(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}
