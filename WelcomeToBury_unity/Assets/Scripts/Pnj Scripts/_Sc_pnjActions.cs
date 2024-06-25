using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_pnjActions : MonoBehaviour
{
    _Sc_pnjState _sc_pnjState = null;
    _Sc_selectPnj _sc_selectPnj = null;
    _Sc_cookbook _sc_cookBook = null;
    _Sc_Calendrier _sc_calendrier = null;

    private void Awake()
    {
        _sc_pnjState = GetComponent<_Sc_pnjState>();
    }

    private void Start()
    {
        _sc_selectPnj = _Sc_selectPnj.Instance;
        _sc_calendrier = _Sc_Calendrier.instance;
        _sc_cookBook = _Sc_cookbook.instance;
    }

    public void PnjDialogue(bool _passTime)
    {
        _sc_pnjState.DialogueOk = true;
        _sc_pnjState.CanSoucier = true;
        _sc_pnjState.SetButtonsState();

        _sc_selectPnj.SetFichePatient();

        if (_passTime == true)
        {
            //Send passage de temps
        }
    }

    public void PnjSoucier(bool _passTime)
    {
        _sc_pnjState.SoucierOk = true;
        _sc_pnjState.CanRemede = true;
        _sc_pnjState.CanGesteSoin = true;
        _sc_pnjState.SetButtonsState();

        _sc_selectPnj.SetFichePatient();

        if (_sc_pnjState.symptome1 == true)
        {
            _sc_cookBook.AdvanceDiscovery("treatment1");
        }
        else if (_sc_pnjState.symptome2 == true)
        {
            _sc_cookBook.AdvanceDiscovery("treatment2");
        }
        else if (_sc_pnjState.symptome3 == true)
        {
            _sc_cookBook.AdvanceDiscovery("treatment3");
        }
        else if (_sc_pnjState.symptome4 == true)
        {
            _sc_cookBook.AdvanceDiscovery("treatment4");
        }
        _sc_selectPnj.SetFichePatient();

        if (_passTime == true)
        {
            Debug.Log("careCallTime");
            _sc_calendrier.AdvanceCalendar();
        }
    }

    public void PnjRemede(bool _passTime)
    {        
        _sc_pnjState.SetButtonsState();

        _sc_selectPnj.SetFichePatient();

        //check si a remede
        //soigne symptome
        //remove remede

        if (_passTime == true)
        {
            //Send passage de temps
        }
    }

    public void PnjGesteSoin(bool _passTime)
    {
        _sc_pnjState.SetButtonsState();

        _sc_selectPnj.SetFichePatient();

        //soigne symptome

        if (_passTime == true)
        {
            //Send passage de temps
        }
    }
}
