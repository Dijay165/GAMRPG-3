using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TopHUDUI : MonoBehaviour
{
    public RectTransform leftTeam;
    public RectTransform rightTeam;
    public GameObject prefab;
    private void Start()
    {
        for (int i = 0; i < GameManager.instance.teams[0].heroPerformanceData.Count; i++)
        {
            GameObject instance = Instantiate(prefab);
            instance.GetComponent<RectTransform>().SetParent(leftTeam);
        }

        for (int i = 0; i < GameManager.instance.teams[1].heroPerformanceData.Count; i++)
        {
            GameObject instance = Instantiate(prefab);
            instance.GetComponent<RectTransform>().SetParent(rightTeam);
        }
    }
}
