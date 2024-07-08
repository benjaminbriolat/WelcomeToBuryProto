using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _Sc_Calendrier : MonoBehaviour
{
    public static _Sc_Calendrier instance = null;
    [Header("UI")]
    [SerializeField] TextMeshProUGUI dayText = null;
    [SerializeField] TextMeshProUGUI plageText = null;
    int currentDay = 1;
    public int currentPlage = 1;
    _Sc_EpidemicManager _sc_epidemicManager = null;
    [Header("Affected Objects")]
    [SerializeField] List<Transform> pnjs = null;
    [SerializeField] List<Transform> crops = null;
    _Sc_ressourcesPremeption _ressourcesPremenption = null;
    [SerializeField] List<string> spanNames = null;
    [SerializeField] List<Color> lightValues = null;
    [SerializeField] Light _light = null;
    public bool debugNofade = false;
    [SerializeField] bool debugAffectLight = false;
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
        plageText.text = spanNames[currentPlage -1].ToString();
        _sc_epidemicManager = _Sc_EpidemicManager.instance;
        _ressourcesPremenption = _Sc_ressourcesPremeption.instance;
        SetLight();
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
            if(debugNofade == true)
            {
                EndAdvanceCalendar(false);
            }
            else
            {
                _Sc_DebugBlackScreen.instance.SetBlackScreen(true, false);
            }
        }
        else
        {
            currentDay += 1;
            currentPlage = 1;
            //Envoyer propagation maladie
            if(debugNofade == true)
            {
                EndAdvanceCalendar(true);
            }
            else
            {
                _Sc_DebugBlackScreen.instance.SetBlackScreen(true, true);
            }
        }


    }

    public void SetLight()
    {
        if(debugAffectLight == true)
        {
            _light.color = lightValues[currentPlage - 1];
        }
    }

    public void EndAdvanceCalendar(bool day)
    {
        if(day == true)
        {
            if (_sc_epidemicManager != null)
            {
                _sc_epidemicManager.AdvancedDay(currentDay);
            }

            //envoyer progression statut aux PNJS
            for (int i = 0; i < pnjs.Count; i++)
            {
                pnjs[i].GetComponent<_Sc_pnjState>().OnDayChange();
            }
        }

        //envoyer repousse au ressources de CROPS
        for (int i = 0; i < crops.Count; i++)
        {
            crops[i].GetComponent<_Sc_RessourcesSpawner>().OnSpanChange();
        }

        //envoyer progression aux ressource de l'inventaire
        _ressourcesPremenption.OnSpanChange();

        dayText.text = currentDay.ToString();
        plageText.text = spanNames[currentPlage - 1].ToString();
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
