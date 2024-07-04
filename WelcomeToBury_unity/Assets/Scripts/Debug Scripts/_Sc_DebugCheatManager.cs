using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_DebugCheatManager : MonoBehaviour
{
    [SerializeField] CanvasGroup movementDebug = null;
    [SerializeField] CanvasGroup camDebug = null;
    [SerializeField] CanvasGroup itemDebug = null;

    [SerializeField] _Sc_debugMovementCanvas _debugMovement = null;
    [SerializeField] _Sc_DebugCamSliders _debugCam = null;
    [SerializeField] _Sc_DebugItemCanvas _debugItems = null;
    [SerializeField] _Sc_debugClearCraftTableCanvas  craftTableClear= null;
    [SerializeField] _Sc_DebugLockAutoFill _lockAutoFill = null;
    bool isOpen = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Set();
        }
    }
    public void Set()
    {
        if(isOpen)
        {
            isOpen = false;

            movementDebug.alpha = 0;
            movementDebug.interactable = false;
            movementDebug.blocksRaycasts = false;

            camDebug.alpha = 0;
            camDebug.interactable = false;
            camDebug.blocksRaycasts = false;

            itemDebug.alpha = 0;
            itemDebug.interactable = false;
            itemDebug.blocksRaycasts = false;
            craftTableClear.Set();

            if(_debugMovement.isOpen)
            {
                _debugMovement.Open();
            }

            if (_debugCam.isOpen)
            {
                _debugCam.open();
            }

            if (_debugItems.isOpen)
            {
                _debugItems.Open();
            }

            _lockAutoFill.Set();
        }
        else
        {
            isOpen = true;

            movementDebug.alpha = 1;
            movementDebug.interactable = true;
            movementDebug.blocksRaycasts = true;

            camDebug.alpha = 1;
            camDebug.interactable = true;
            camDebug.blocksRaycasts = true;

            itemDebug.alpha = 1;
            itemDebug.interactable = true;
            itemDebug.blocksRaycasts = true;
            craftTableClear.Set();
            _lockAutoFill.Set();
        }
    }

}
