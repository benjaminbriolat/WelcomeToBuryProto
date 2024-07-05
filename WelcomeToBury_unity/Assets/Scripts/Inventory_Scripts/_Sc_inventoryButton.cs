using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Sc_inventoryButton : MonoBehaviour
{
    public bool inventoryOpen = false;
    [SerializeField] CanvasGroup inventoryCanvasGroup = null;
    [SerializeField] CanvasGroup DebugGiveItemCanvasGroup = null;
    [SerializeField] Scrollbar inventoryScrollbar = null;

    [SerializeField] bool alsoOpenDebug = false;
    _Sc_inventoryManager _sc_inventoryManager = null;
    _Sc_cerveau _sc_cerveau = null;

    private void Start()
    {
        inventoryOpen = false;
        inventoryCanvasGroup.alpha = 0;
        inventoryCanvasGroup.interactable = false;
        inventoryCanvasGroup.blocksRaycasts = false;

        DebugGiveItemCanvasGroup.alpha = 0;
        DebugGiveItemCanvasGroup.interactable = false;
        DebugGiveItemCanvasGroup.blocksRaycasts = false;

        _sc_inventoryManager = _Sc_inventoryManager.instance;
        _sc_cerveau = _Sc_cerveau.instance;
    }
    public void SetCanvas()
    {
        if(inventoryOpen == false) //open inventory
        {
            inventoryOpen = true;
            _sc_inventoryManager.inventoryOpen = true;
            _sc_inventoryManager.ClearAllClicks();
            inventoryCanvasGroup.alpha = 1;
            inventoryCanvasGroup.interactable = true;
            inventoryCanvasGroup.blocksRaycasts = true;

            if(alsoOpenDebug == true)
            {
                DebugGiveItemCanvasGroup.alpha = 1;
                DebugGiveItemCanvasGroup.interactable = true;
                DebugGiveItemCanvasGroup.blocksRaycasts = true;
            }

            if(inventoryScrollbar != null)
            {
                inventoryScrollbar.value = 1;
            }
        }
        else if(inventoryOpen == true) //close inventory
        {
            inventoryOpen = false;
            _sc_inventoryManager.inventoryOpen = false;
            _sc_inventoryManager.ClearAllClicks();
            inventoryCanvasGroup.alpha = 0;
            inventoryCanvasGroup.interactable = false;
            inventoryCanvasGroup.blocksRaycasts = false;

            if (alsoOpenDebug == true)
            {
                DebugGiveItemCanvasGroup.alpha = 0;
                DebugGiveItemCanvasGroup.interactable = false;
                DebugGiveItemCanvasGroup.blocksRaycasts = false;
            }
        }
    }
}
