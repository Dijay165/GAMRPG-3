using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ancient : Structures
{
    // Start is called before the first frame update
    public Unit oppositeFaction;

    private void Start()
    {
        oppositeFaction = GetComponent<Unit>();
   //     EnemyFaction();
    }

    public override void Death(Health objectHealth = null)
    {
        // base.Death(objectHealth);
        EnemyFaction();
        Events.OnGameOver.Invoke(oppositeFaction);
    }

    public void EnemyFaction()
    {
        if(Faction.Radiant == unitFaction)
        {
            oppositeFaction.unitFaction = Faction.Dire;
        }
        else
        {
            oppositeFaction.unitFaction = Faction.Radiant;
        }
    }
}
