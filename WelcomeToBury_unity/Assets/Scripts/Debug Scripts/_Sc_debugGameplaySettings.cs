using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Sc_debugGameplaySettings : MonoBehaviour
{
    public static _Sc_debugGameplaySettings instance = null;
    [SerializeField] List<_Sc_pnjActionsParent> pnjActionsParent = null;
    [HideInInspector] public bool isOpen = false;
    public bool gesteDeSoinActive = false;
    [SerializeField] Image coche = null;
    CanvasGroup _canvasGroup = null;
    private void Awake()
    {
        instance = this;
        pnjActionsParent = new List<_Sc_pnjActionsParent>();
        _canvasGroup = transform.GetChild(0).GetChild(0).GetComponent<CanvasGroup>();
    }
    public void AddPnj(_Sc_pnjActionsParent newPnj)
    {
        if(pnjActionsParent.Contains(newPnj) == false)
        {
            pnjActionsParent.Add(newPnj);
        }
    }

    public void open()
    {
        if(isOpen == false)
        {
            isOpen = true;
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }
        else
        {
            isOpen = false;
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }
    }

    public void ActivategesteDesoin()
    {
        if(gesteDeSoinActive == false)
        {
            gesteDeSoinActive = true;
            coche.enabled = true;
        }
        else
        {
            gesteDeSoinActive = false;
            coche.enabled = false;
        }
        for (int i = 0; i < pnjActionsParent.Count; i++)
        {
            pnjActionsParent[i].LockGesteDeSoin(!gesteDeSoinActive);
        }
    }
}
