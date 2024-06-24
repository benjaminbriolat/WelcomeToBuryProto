using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_inventoryButton : MonoBehaviour
{
    bool inventoryOpen = false;
    [SerializeField] CanvasGroup inventoryCanvasGroup = null;
    [SerializeField] CanvasGroup DebugGiveItemCanvasGroup = null;

    private void Start()
    {
        inventoryOpen = false;
        inventoryCanvasGroup.alpha = 0;
        inventoryCanvasGroup.interactable = false;
        inventoryCanvasGroup.blocksRaycasts = false;

        DebugGiveItemCanvasGroup.alpha = 0;
        DebugGiveItemCanvasGroup.interactable = false;
        DebugGiveItemCanvasGroup.blocksRaycasts = false;
    }
    public void SetCanvas()
    {
        if(inventoryOpen == false) //open inventory
        {
            inventoryOpen = true;

            inventoryCanvasGroup.alpha = 1;
            inventoryCanvasGroup.interactable = true;
            inventoryCanvasGroup.blocksRaycasts = true;

            DebugGiveItemCanvasGroup.alpha = 1;
            DebugGiveItemCanvasGroup.interactable = true;
            DebugGiveItemCanvasGroup.blocksRaycasts = true;
        }
        else if(inventoryOpen == true) //close inventory
        {
            inventoryOpen = false;

            inventoryCanvasGroup.alpha = 0;
            inventoryCanvasGroup.interactable = false;
            inventoryCanvasGroup.blocksRaycasts = false;

            DebugGiveItemCanvasGroup.alpha = 0;
            DebugGiveItemCanvasGroup.interactable = false;
            DebugGiveItemCanvasGroup.blocksRaycasts = false;
        }
    }
}
