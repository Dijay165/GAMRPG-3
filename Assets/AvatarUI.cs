using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AvatarUI : MonoBehaviour
{
    public Image avatar;
    public GameObject countdownFrame;
    public TMP_Text deathCooldown;

    public void SetUpUI(Sprite sprite)
    {
        avatar.sprite = sprite;
        countdownFrame.SetActive(false);
        deathCooldown.text = "0";
    }

    public void UpdateUI(int coutndownTimer)
    {
        if (coutndownTimer > 0)
        {
            countdownFrame.SetActive(true);
            deathCooldown.text = coutndownTimer.ToString();
        }
        else
        {
            countdownFrame.SetActive(false);
        }

    }
}
