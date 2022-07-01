using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private float spawnTime = 75;

    [SerializeField] private int waveCounter = 0;
    [SerializeField] private float waveTime = 30;
    [SerializeField] private float waveTimer = 0;

    [SerializeField] private int amountOfMeleeCreep = 3;
    [SerializeField] private int amountOfRangeCreep = 1;
    [SerializeField] private int amountOfSiegeCreep = 1;
    [SerializeField] private int siegeCreepWaveAmount = 10; //Every 10 waves
    [SerializeField] private int doubleSiegeCreepTime = 2100; // After 35 minutes (2100 seconds)

    // Start is called before the first frame update
    void Start()
    {
        waveTimer = waveTime;
        StartCoroutine(Co_SpawnWave());
    }

 
    IEnumerator Co_SpawnWave()
    {
     
        

        for (int teamIndex = 0; teamIndex < GameManager.instance.teams.Count; teamIndex++)//For each team
        {
            for (int laneIndex = 0; laneIndex < GameManager.instance.teams[teamIndex].lanes.Count; laneIndex++)// For each team's lanes
            {
                int otherTeam = teamIndex == 0 ? 1 : 0; //If p_team variable is equals to 0, make value 1, else (if its 1) make value 0


                //Melee Creep
                if (GameManager.instance.teams[otherTeam].lanes[laneIndex].meleeBarracksIsAlive) //If enemy team's barracks is dead spawn mega Creep
                {
                    SpawnCreep<MeleeCreep>(teamIndex, laneIndex, amountOfMeleeCreep);
                }
                else//If enemy team's barracks is alive spawn normal Creep
                {
                    SpawnCreep<MegaMeleeCreep>(teamIndex, laneIndex, amountOfMeleeCreep); //Mega melee creep make
                }

                //Range Creep
                if (GameManager.instance.teams[otherTeam].lanes[laneIndex].rangeBarracksIsAlive) //If enemy team's barracks is dead spawn mega Creep
                {
                    SpawnCreep<RangeCreep>(teamIndex, laneIndex, amountOfRangeCreep);
                }
                else//If enemy team's barracks is alive spawn normal Creep
                {
                    SpawnCreep<MegaRangeCreep>(teamIndex, laneIndex, amountOfRangeCreep);
                }

                //Siege Creep
                if (waveCounter % siegeCreepWaveAmount == 0)
                {
                    amountOfSiegeCreep = 1;
                    if (GameManager.instance.gameTime >= doubleSiegeCreepTime)
                    {
                        amountOfSiegeCreep = 2;
                    }

                    if (GameManager.instance.teams[otherTeam].lanes[laneIndex].rangeBarracksIsAlive) //If enemy team's barracks is dead spawn mega Creep
                    {
                        SpawnCreep<SiegeCreep>(teamIndex, laneIndex, amountOfSiegeCreep);
                    }
                    else//If enemy team's barracks is alive spawn normal Creep
                    {
                        SpawnCreep<MegaSiegeCreep>(teamIndex, laneIndex, amountOfSiegeCreep);
                    }
                }


            }

        }
       
        waveCounter++;
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(Co_SpawnWave());
       
    }
 
    public static void UpdateBarracksState(Barracks p_barracks)
    {
        for (int teamIndex = 0; teamIndex < GameManager.instance.teams.Count; teamIndex++)//For each team
        {
            for (int laneIndex = 0; laneIndex < GameManager.instance.teams[teamIndex].lanes.Count; laneIndex++)// For each team's lanes
            {
                for (int buildingIndex = 0; buildingIndex < GameManager.instance.teams[teamIndex].lanes[laneIndex].buildings.Count; buildingIndex++)// For each team's lanes
                {
                    Debug.Log(GameManager.instance.teams[teamIndex].lanes[laneIndex].buildings[buildingIndex].name+" bnames IT");
                    if (p_barracks.gameObject == GameManager.instance.teams[teamIndex].lanes[laneIndex].buildings[buildingIndex].gameObject)
                    {
                        Debug.Log("GOT IT");
                        GameManager.instance.teams[teamIndex].lanes[laneIndex].meleeBarracksIsAlive = false;
                        GameManager.instance.teams[teamIndex].lanes[laneIndex].rangeBarracksIsAlive = false;
                        p_barracks.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    void SpawnCreep<T>(int p_team, int p_lane, int p_amount = 1) where T: MonoBehaviour
    {
        
            StartCoroutine(Co_SpawnCreep<T>( p_team,  p_lane,  p_amount));
           
    }

    IEnumerator Co_SpawnCreep<T>(int p_team, int p_lane, int p_amount = 1) where T : MonoBehaviour
    {
        for (int i = 0; i < p_amount; i++)
        {
            
            T genericObject = GenericObjectPool<T>.pool.Get();

            if (genericObject is Creep)
            {
                Creep newCreep = genericObject as Creep;
                newCreep.waypoints = GameManager.MakePath(p_team, p_lane);
               
                newCreep.transform.position = (GameManager.instance.teams[p_team].lanes[p_lane].creepSpawnPoint.transform.position);
                newCreep.unitFaction = (Faction)p_team;
                newCreep.GetComponent<Health>().ResetValues();
                Animator anim = newCreep.GetComponent<Animator>();
                NavMeshAgent nav = newCreep.GetComponent<NavMeshAgent>();
                nav.Warp(new Vector3(newCreep.transform.position.x, 0f, newCreep.transform.position.z));// newCreep.transform.position);
                nav.enabled = false;
                nav.enabled = true;
                anim.SetInteger("pathCount", 2);
              

            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}


