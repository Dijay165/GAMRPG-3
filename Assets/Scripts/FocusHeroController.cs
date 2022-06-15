using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusHeroController : MonoBehaviour
{
    [SerializeField] private KeyCode assignedKey = KeyCode.Alpha1;
    [SerializeField] private int pressedAmount;
    private int currentPressedAmount;
    [SerializeField] private float decayTime;
    private Coroutine runningCoroutine;
    [SerializeField]
    private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(assignedKey))
        {
            if (runningCoroutine != null)
            {
                StopCoroutine(runningCoroutine);
                
            }
            currentPressedAmount++;

            if (currentPressedAmount >= pressedAmount)
            {
                currentPressedAmount = 0;
                Events.OnPlayerSelect.Invoke();
                CameraManager.instance.cam.transform.parent.transform.position = new Vector3(PlayerManager.instance.player.transform.position.x + offset.x, offset.y, PlayerManager.instance.player.transform.position.z + offset.z);
            }
            else
            {
                runningCoroutine = StartCoroutine(Co_Decay());
            }


        }
    }

    IEnumerator Co_Decay()
    {
        yield return new WaitForSeconds(decayTime);
        currentPressedAmount = 0;
    }
}
