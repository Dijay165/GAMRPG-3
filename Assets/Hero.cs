using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    public override void Death(Health objectHealth = null)
    {
        base.Death();
        FindNearbyHeroes(1500);
        //Formula: base gold + (dead hero level * 8) + streak gold
        if (nearbyEnemyHeroes.Count > 0)
        {
            foreach (Hero currentHero in nearbyEnemyHeroes)
            {
                HeroPerformanceData hpd = GameManager.GetHeroData(currentHero);
                if (hpd == GameManager.GetHeroData(health.damager))
                {
                    hpd.gold += (goldReward + (level.level * 8) + GameManager.GetKillStreakGold(hpd.killstreak));

                }
                else
                {
                    //Formula(30 + Victim Net Worth x 0.038) x k / Number of Heroes
                    hpd.gold += (goldReward + Mathf.FloorToInt(1f * 0.038f)) / nearbyEnemyHeroes.Count;
                }
            }
           
        }
         
      



    }
}
