using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Minimap : MonoBehaviour, IPointerDownHandler
{
    public GameObject iconPrefab;
    public MinimapSignal heroSignal;
    public RectTransform minimapParent;
    public List<MinimapIconUI> miniMapIcons = new List<MinimapIconUI>();
    private void Start()
    {

        minimapParent = minimapParent ? minimapParent : gameObject.GetComponent<RectTransform>();
        if (heroSignal == null)
        {
            Debug.Log("ERROR: NO HERO SIGNAL ASSIGNED TO VARIABLE");
        }
        
        MinimapManager.instance.OnMinimapSignalRegistered += RegisterNewSignal;
        StartCoroutine(Co_GenerateMinimap());
    }

    public void DeregisterGlobalSignal(int p_ID)
    {
        foreach (MinimapIconUI selectedLocalIcon in miniMapIcons)
        {
            if (selectedLocalIcon.id == p_ID)
            {
                selectedLocalIcon.minimapRect.gameObject.SetActive(false);
                miniMapIcons.Remove(selectedLocalIcon);
                break;
            }
        }

    }

    protected virtual void RegisterNewSignal(MinimapIcon selectedMinimapIcon)
    {

        MinimapIconUI newIcon = new MinimapIconUI();

        newIcon.id = selectedMinimapIcon.id;


        miniMapIcons.Add(newIcon);

        GameObject newMinimapObject = Instantiate(iconPrefab, minimapParent);
        newIcon.minimapRect = newMinimapObject.GetComponent<RectTransform>();
        newMinimapObject.GetComponent<Image>().sprite = selectedMinimapIcon.icon;
        newIcon.minimapRect.anchoredPosition = selectedMinimapIcon.minimapPosition;
        newIcon.minimapRect.sizeDelta = selectedMinimapIcon.iconSize;

        MinimapManager.instance.OnDeregistered += DeregisterGlobalSignal;

    }

    public void OnPointerDown(PointerEventData data)
    {
        Vector2 localCursor;
        RectTransform minimapRect = GetComponent<RectTransform>();
        Vector2 dataPosition = data.position;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(minimapRect,
            dataPosition,
            null,
            out localCursor))
        {
            return;
        }

        int xpos = (int)(localCursor.x);
        int ypos = (int)(localCursor.y);

        if (xpos < 0)
        {
            xpos = xpos + (int)minimapRect.rect.width / 2;
        }
        else
        {
            xpos += (int)minimapRect.rect.width / 2;
        }

        if (ypos > 0)
        {
            ypos = ypos + (int)minimapRect.rect.height / 2;

        }
        else
        {
            ypos += (int)minimapRect.rect.height / 2;
        }

        
        float worldPositionX = (xpos /250f ) *5000f;
        float worldPositionY = (ypos / 250f) * 5000f;
        //Debug.Log("Pos: " + xpos + " = " + worldPositionX + "," + ypos + " = " + worldPositionY);
        CameraManager.instance.cam.transform.position = new Vector3(worldPositionX, CameraManager.instance.cam.transform.position.y, worldPositionY);

    }
   
    private IEnumerator Co_GenerateMinimap()
    {

        foreach (MinimapIcon selectedGlobalIcon in MinimapManager.instance.miniMapIcons)
        {

            //Look for local icon
            foreach (MinimapIconUI selectedLocalIcon in miniMapIcons)
            {
                if (selectedGlobalIcon.team == heroSignal.team) //teammate
                {
                    if (selectedGlobalIcon.id == selectedLocalIcon.id)
                    {
                        selectedLocalIcon.minimapRect.anchoredPosition = selectedGlobalIcon.minimapPosition;
                        break;
                    }


                }
                else if (selectedGlobalIcon.team != heroSignal.team) //Not teammate
                {
                    if (selectedGlobalIcon.isSeenByOpposingTeam)
                    {
                        if (selectedGlobalIcon.id == selectedLocalIcon.id)
                        {
                            selectedLocalIcon.minimapRect.anchoredPosition = selectedGlobalIcon.minimapPosition;
                            break;
                        }
                    }

                }
            }


        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(Co_GenerateMinimap());

    }
}
