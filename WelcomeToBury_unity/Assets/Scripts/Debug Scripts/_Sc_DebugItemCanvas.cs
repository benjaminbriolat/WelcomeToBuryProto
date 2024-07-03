using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_DebugItemCanvas : MonoBehaviour
{
    bool isOpen = false;
    public void Open()
    {
        if (isOpen == true)
        {
            isOpen = false;
            transform.GetComponent<CanvasGroup>().alpha = 0;
            transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
            transform.GetComponent<CanvasGroup>().interactable = false;
        }
        else
        {
            isOpen = true;
            transform.GetComponent<CanvasGroup>().alpha = 1;
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.GetComponent<CanvasGroup>().interactable = true;
        }
    }
}
