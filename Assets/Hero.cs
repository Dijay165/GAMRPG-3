using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creep
{
    public override void Death(Health objectHealth = null)
    {
        //base.Death();
        level.RewardExp();
        DeinitializeValues();


        FindNearbyHeroes(1500);
        //Formula: base gold + (dead hero level * 8) + streak gold
        if (nearbyEnemyHeroes.Count > 0)
        {
            
            for (int i = 0; i < nearbyEnemyHeroes.Count; i++)
            {
                if (nearbyEnemyHeroes[i] is Hero || nearbyEnemyHeroes[i] is Player)
                {
                    HeroPerformanceData hpd = GameManager.GetHeroData(nearbyEnemyHeroes[i]);
                    if (hpd == GameManager.GetHeroData(health.damager))
                    {
                        hpd.gold += (goldReward + (level.currentLevel * 8) + GameManager.GetKillStreakGold(hpd.killstreak));
                        hpd.kills++;
                        hpd.killstreak++;
                        hpd.networth = hpd.gold;
                        Debug.Log(health.damager.gameObject.name);
                        Debug.Log( " HERO DEATH GAINED GOLD " + goldReward);

                    }
                    else if (hpd != null)
                    {
                        //Formula(30 + Victim Net Worth x 0.038) x k / Number of Heroes
                        hpd.gold += (goldReward + Mathf.FloorToInt(1f * 0.038f)) / nearbyEnemyHeroes.Count;
                        hpd.networth = hpd.gold;
                        Debug.Log(gameObject.name);
                        Debug.Log( " SPLIT HERO DEATH GAINED GOLD " + goldReward);
                    }
                    GameManager.OnUpdateHeroUIEvent.Invoke(hpd);
                }
            }
               
            
           
        }
        SpawnManager.instance.RespawnHero(this);

    }


}
