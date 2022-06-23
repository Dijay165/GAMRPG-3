using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStatsHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public UnitStat unitStat;
    public Attributes attributes;
    public Faction unitFaction;

    private void Awake()
    {
        attributes = GetComponent<Attributes>();
    }
    public void Intialization()
    {
        
        unitStat.faction = unitFaction;
      
    }
    private void Start()
    {
        Intialization();
    }


}
