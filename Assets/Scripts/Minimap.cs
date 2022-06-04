using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Minimap : MonoBehaviour, IPointerDownHandler
{

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
        Debug.Log("Pos: " + xpos + " = " + worldPositionX + "," + ypos + " = " + worldPositionY);
        CameraManager.instance.cam.transform.position = new Vector3(worldPositionX, CameraManager.instance.cam.transform.position.y, worldPositionY);

    }
   
  
}
