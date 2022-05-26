using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject debugGO;
    public TimeManager timeManager;
    void Start()
    {
        
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
        debugGO.SetActive(true);
    }

    void CancelGO()
    {
        debugGO.SetActive(false);
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
}
