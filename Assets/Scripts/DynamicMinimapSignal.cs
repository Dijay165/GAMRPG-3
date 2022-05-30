using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMinimapSignal : MinimapSignal
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Co_UpdateMapPosition());
    }

    IEnumerator Co_UpdateMapPosition()
    {
        normalized = Divide(
        MinimapManager.instance.map3dParent.InverseTransformPoint(mapObject.transform.position),
        MinimapManager.instance.mapCenter.position - MinimapManager.instance.map3dParent.position
        );
        normalized.y = normalized.z;


        foreach (MinimapIcon selectedGlobalIcon in MinimapManager.instance.miniMapIcons)
        {
            //Player's position
            if (selectedGlobalIcon.id == id)
            {
                mapped = Multiply(normalized, MinimapManager.instance.minimapCenter);
                mapped.z = 0;
                selectedGlobalIcon.minimapPosition = mapped;

            }
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(Co_UpdateMapPosition());
    }
}
