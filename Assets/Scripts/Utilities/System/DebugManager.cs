using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> debugGO;
    public TimeManager timeManager;
    public GameObject structure;


    private void OnEnable()
    {
        Events.OnTowerDied.AddListener(DestroyStructure);
    }

    private void OnDisable()
    {
        Events.OnTowerDied.RemoveListener(DestroyStructure);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GOActivator();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelGO();
        }
    }

    void GOActivator()
    {
        foreach(GameObject obj in debugGO)
        {
            obj.SetActive(true);
        }
      
    }

    void CancelGO()
    {

        foreach (GameObject obj in debugGO)
        {
            obj.SetActive(false);
        }
       // debugGO.SetActive(false);
    }

    public void IncreaseGameSpeed()
    {
        Time.timeScale *= 2f;
    }

    public void DecreaseGameSpeed()
    {
        Time.timeScale /= 2;
    }

    public void ResetTime()
    {
        Time.timeScale = 1;
    }

    public void DestroyStructure()
    {
        //Click any structure and then if this button is pressed, structure will be destroy. 
        Destroy(structure.gameObject);
        Events.OnResetInfoUI.Invoke();
        Debug.Log("Destroy");
    }
    public void RestoreHealth()
    {
        //Click any unit and then if this button is pressed, the healthpoints of the unit will be filled. 
        Health health = GameManager.instance.lastUnitSelect.GetComponent<Health>();
        health.AddHealth(100f);
    }

    public void RestoreMana()
    {
        Mana mana = PlayerManager.instance.player.GetComponent<Mana>();
        mana.AddMana(100f);
    }

    public void RestoreCooldown()
    {
        GameObject player = PlayerManager.instance.player;

      

        for(int i = 0; i < SkillManager.instance.text.Count; i++)
        {
            player.GetComponent<SkillHolder>().skills[i].canCast = true;
            player.GetComponent<SkillHolder>().skills[i].isCooldown = false;
            player.GetComponent<SkillHolder>().skills[i].isInEffect = false;
            SkillManager.instance.text[i].text = "";
        }

        player.GetComponent<SkillHolder>().StopCountdown();

    }
}
