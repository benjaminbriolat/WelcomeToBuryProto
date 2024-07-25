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
    [SerializeField] Image cochehealing = null;
    [SerializeField] Image cochepictos = null;
    [SerializeField] Image cocheFiche = null;
    CanvasGroup _canvasGroup = null;
    public bool healingCostsTime = false;
    _Sc_fichePatientUI _fichePatientUi = null;
    CanvasGroup fichePatientDebugCanvasGroup = null;
    public bool fichePatientActive = true;

    private void Awake()
    {
        instance = this;
        pnjActionsParent = new List<_Sc_pnjActionsParent>();
        _canvasGroup = transform.GetChild(0).GetChild(0).GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        _fichePatientUi = _Sc_fichePatientUI.Instance;
        fichePatientDebugCanvasGroup = _fichePatientUi.transform.GetChild(0).GetComponent<CanvasGroup>();
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

    public void ActivateHealCostsTime()
    {
        healingCostsTime = !healingCostsTime;
        cochehealing.enabled = healingCostsTime;
    }

    public void ActivatePictosRemede()
    {
        _fichePatientUi.displayPictos = !_fichePatientUi.displayPictos;
        _fichePatientUi.SetPictos();
        cochepictos.enabled = _fichePatientUi.displayPictos;
    }

    public void ActivateFichePatient()
    {
        if (fichePatientActive == false)
        {
            fichePatientActive = true;
            cocheFiche.enabled = true;

            fichePatientDebugCanvasGroup.alpha = 1;
        }
        else
        {
            fichePatientActive = false;
            cocheFiche.enabled = false;

            fichePatientDebugCanvasGroup.alpha = 0;
        }
    }
}
