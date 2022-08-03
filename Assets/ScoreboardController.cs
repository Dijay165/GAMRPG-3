using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardController : MonoBehaviour
{
    [SerializeField] GameObject scoreboardUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (scoreboardUI.activeSelf != true)
            {
                scoreboardUI.SetActive(true);
            }
            else
            {
                scoreboardUI.SetActive(false);
            }
          
        }
    }
}
