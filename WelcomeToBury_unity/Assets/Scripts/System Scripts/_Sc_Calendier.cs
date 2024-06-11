using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _Sc_Calendier : MonoBehaviour
{
    public static _Sc_Calendier instance = null;
    [Header("UI")]
    [SerializeField] TextMeshProUGUI dayText = null;
    [SerializeField] TextMeshProUGUI plageText = null;
    int currentDay = 1;
    int currentPlage = 1;
    _Sc_EpidemicManager _sc_epidemicManager = null;
    [Header("Affected Objects")]
    [SerializeField] List<Transform> pnjs = null;
    [SerializeField] List<Transform> crops = null;
    private void Awake()
    {
        instance = this;
        pnjs = new List<Transform>();
        crops = new List<Transform>();
    }

    void Start()
    {
        currentDay = 1;
        currentPlage = 1;
        dayText.text = currentDay.ToString();
        plageText.text = currentPlage.ToString();
        _sc_epidemicManager = _Sc_EpidemicManager.instance;
    }
    private void Update()
    {
        //Debug jours
        if(Input.GetKeyDown(KeyCode.T))
        {
            AdvanceCalendar();
        }
    }

    public void AdvanceCalendar()
    {
        //Set day and span
        if (currentPlage < 3)
        {
            currentPlage += 1;
        }
        else
        {
            currentDay += 1;
            currentPlage = 1;
            if (_sc_epidemicManager != null)
            {
                _sc_epidemicManager.AdvancedDay(currentDay);
            }
        }
        dayText.text = currentDay.ToString();
        plageText.text = currentPlage.ToString();

        //envoyer repousse au ressources de CROPS
        //envoyer progression statut aux PNJS
        //envoyer progression aux ressource de l'inventaire
    }

    public void AddCrop(Transform newCrop)
    {
        if(crops.Contains(newCrop) == false)
        {
            crops.Add(newCrop);
        }
    }

    public void AddPnj(Transform newPnj)
    {
        if (pnjs.Contains(newPnj) == false)
        {
            pnjs.Add(newPnj);
        }
    }

    
}
