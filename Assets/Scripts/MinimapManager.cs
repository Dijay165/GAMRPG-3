using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MinimapIconUI
{
    public int id;
    public RectTransform minimapRect;

}

public class MinimapIcon
{
    public int id;
    public int team;
    public Sprite icon;
    public Vector2 iconSize;
    public Vector3 minimapPosition;
    public bool isSeenByOpposingTeam;

}

   

public class MinimapManager : MonoBehaviour
{
    public static MinimapManager instance;
    public List<MinimapIcon> miniMapIcons = new List<MinimapIcon>();
    public Vector3 minimapCenter;
    public Transform map3dParent;
    public Transform mapCenter;

    public Action<MinimapIcon> OnMinimapSignalRegistered;
    public Action<int> OnDeregistered;
    private void Awake()
    {
        instance = instance ? instance : this;
    }
    // Start is called before the first frame update
    void Start()
    {
        OnDeregistered += DeregisterSignal;
    }


    public void DeregisterSignal(int p_ID)
    {

      
        foreach (MinimapIcon selectedGlobalIcon in miniMapIcons)
        {
            if (selectedGlobalIcon.id == p_ID)
            {
                miniMapIcons.Remove(selectedGlobalIcon);
                break;
            }
        }

    }
}
