using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_DebugLockAutoFill : MonoBehaviour
{
    bool isOpen = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Set();
        }
    }
    public void Set()
    {
        if (isOpen)
        {
            isOpen = false;

            transform.GetComponent<CanvasGroup>().alpha = 0;
            transform.GetComponent<CanvasGroup>().interactable = false;
            transform.GetComponent<CanvasGroup>().blocksRaycasts = false;           
        }
        else
        {
            isOpen = true;

            transform.GetComponent<CanvasGroup>().alpha = 1;
            transform.GetComponent<CanvasGroup>().interactable = true;
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
