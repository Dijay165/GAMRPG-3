using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum DayState
{
    Morning,
    Night
};
public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI timeText;
    public Image imageSprite;
    public DayState dayState;
    public Sprite[] images;
    [SerializeField]
    private float stateDuration;
    [SerializeField]
    private float timer;

    void Start()
    {
        InvokeRepeating("ChangeDayState", 1, stateDuration);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.Rotate(new Vector3(180 / stateDuration, 0, 0) * Time.deltaTime);
        DisplayTime(timer);
    }

    void ChangeDayState()
    {
        switch (dayState)
        {
            case DayState.Morning:
                dayState = DayState.Night;
                imageSprite.sprite = images[1];
                break;

            case DayState.Night:
                dayState = DayState.Morning;
               
                imageSprite.sprite = images[0];
                break;
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

   
}
