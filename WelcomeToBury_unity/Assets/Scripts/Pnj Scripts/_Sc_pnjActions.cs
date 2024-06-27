using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _Sc_pnjActions : MonoBehaviour
{
    _Sc_pnjState _sc_pnjState = null;
    _Sc_selectPnj _sc_selectPnj = null;
    _Sc_cookbook _sc_cookBook = null;
    _Sc_Calendrier _sc_calendrier = null;
    _Sc_inventoryManager _sc_inventoryManager = null;
    _Sc_smallTalkCanvas  _sc_smallTalkCanvas = null;
    _Sc_smallTalkData _sc_smallTalkData = null;
    Transform smalltalkAnchor = null;
    _Sc_DebugBlackScreen _sc_debugBlackScreen = null;
    private void Awake()
    {
        _sc_pnjState = GetComponent<_Sc_pnjState>();
    }

    private void Start()
    {
        _sc_selectPnj = _Sc_selectPnj.Instance;
        _sc_calendrier = _Sc_Calendrier.instance;
        _sc_cookBook = _Sc_cookbook.instance;
        _sc_inventoryManager = _Sc_inventoryManager.instance;
        _sc_smallTalkData = _Sc_smallTalkData.instance;
        _sc_smallTalkCanvas = _Sc_smallTalkCanvas.instance;
        _sc_debugBlackScreen = _Sc_DebugBlackScreen.instance;


        smalltalkAnchor = transform.GetChild(1).GetChild(4);

    }

    public void PnjDialogue(bool _passTime)
    {
        _sc_pnjState.DialogueOk = true;
        _sc_pnjState.CanSoucier = true;
        _sc_pnjState.SetButtonsState();

        _sc_selectPnj.SetFichePatient();
        
        if (_passTime == true)
        {
            _sc_calendrier.AdvanceCalendar();
        }
        if (_sc_pnjState.state == 0)
        {
            _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(0), _sc_debugBlackScreen.getFadingTime());
        }
        else if (_sc_pnjState.state == 1)
        {
            _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(1), _sc_debugBlackScreen.getFadingTime());
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
            _sc_calendrier.AdvanceCalendar();
        }

        if (_sc_pnjState.state == 0)
        {
            _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(2), _sc_debugBlackScreen.getFadingTime());
        }
        else if (_sc_pnjState.state == 1)
        {
            _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(3), _sc_debugBlackScreen.getFadingTime());
        }

       
    }

    public void PnjRemede(bool _passTime)
    {        
        _sc_pnjState.SetButtonsState();

        _sc_selectPnj.SetFichePatient();
        bool hasHeal = false;
        if (_sc_pnjState.symptome1 == true && hasHeal == false)
        {          
            if(_sc_inventoryManager.checkItem(_sc_cookBook.getRemede("treatment1"), 1) == true)
            {
                hasHeal = true;
                _sc_pnjState.HealSymptome1();
                _sc_inventoryManager.RemoveItem(_sc_cookBook.getRemede("treatment1"));

                if (_passTime == true)
                {
                    _sc_calendrier.AdvanceCalendar();
                }

                if (_sc_pnjState.state == 0)
                {
                    _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(4), _sc_debugBlackScreen.getFadingTime());
                }
                else if (_sc_pnjState.state == 1)
                {
                    _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(5), _sc_debugBlackScreen.getFadingTime());
                }
            }           
        }

        if (_sc_pnjState.symptome2 == true && hasHeal == false)
        {
            if (_sc_inventoryManager.checkItem(_sc_cookBook.getRemede("treatment2"), 1) == true)
            {
                hasHeal = true;
                _sc_pnjState.HealSymptome2();
                _sc_inventoryManager.RemoveItem(_sc_cookBook.getRemede("treatment2"));

                if (_passTime == true)
                {
                    _sc_calendrier.AdvanceCalendar();
                }

                if (_sc_pnjState.state == 0)
                {
                    _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(4), _sc_debugBlackScreen.getFadingTime());
                }
                else if (_sc_pnjState.state == 1)
                {
                    _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(5), _sc_debugBlackScreen.getFadingTime());
                }
            }
        }

        if (_sc_pnjState.symptome3 == true && hasHeal == false)
        {
            if (_sc_inventoryManager.checkItem(_sc_cookBook.getRemede("treatment3"), 1) == true)
            {
                hasHeal = true;
                _sc_pnjState.HealSymptome3();
                _sc_inventoryManager.RemoveItem(_sc_cookBook.getRemede("treatment3"));

                if (_passTime == true)
                {
                    _sc_calendrier.AdvanceCalendar();
                }

                if (_sc_pnjState.state == 0)
                {
                    _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(4), _sc_debugBlackScreen.getFadingTime());
                }
                else if (_sc_pnjState.state == 1)
                {
                    _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(5), _sc_debugBlackScreen.getFadingTime());
                }
            }
        }

        if (_sc_pnjState.symptome4 == true && hasHeal == false)
        {
            if (_sc_inventoryManager.checkItem(_sc_cookBook.getRemede("treatment4"), 1) == true)
            {
                hasHeal = true;
                _sc_pnjState.HealSymptome4();
                _sc_inventoryManager.RemoveItem(_sc_cookBook.getRemede("treatment4"));

                if (_passTime == true)
                {
                    _sc_calendrier.AdvanceCalendar();
                }

                if (_sc_pnjState.state == 0)
                {
                    _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(4), _sc_debugBlackScreen.getFadingTime());
                }
                else if (_sc_pnjState.state == 1)
                {
                    _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(5), _sc_debugBlackScreen.getFadingTime());
                }
            }
        }

    }

    public void PnjGesteSoin(bool _passTime)
    {
        bool hasHeal = false;
        _sc_pnjState.SetButtonsState();

        _sc_selectPnj.SetFichePatient();

        if(_sc_pnjState.capTrustReached == true)
        {
            if(_sc_pnjState.symptome1 == true && hasHeal == false)
            {
                _sc_pnjState.HealSymptome1Care();
                hasHeal = true;
            }
            if(_sc_pnjState.symptome2 == true && hasHeal == false)
            {
                _sc_pnjState.HealSymptome2Care();
                _sc_pnjState.symptomes[1] = false;
                hasHeal = true;
            }
            if(_sc_pnjState.symptome3 == true && hasHeal == false)
            {
                _sc_pnjState.HealSymptome3Care();
                _sc_pnjState.symptomes[2] = false;
                hasHeal = true;
            }
            if(_sc_pnjState.symptome4 == true && hasHeal == false)
            {
                _sc_pnjState.HealSymptome4Care();
                _sc_pnjState.symptomes[3] = false;
                hasHeal = true;
            }
        }

        hasHeal = false;

        if (_passTime == true)
        {
            _sc_calendrier.AdvanceCalendar();
        }

        if (_sc_pnjState.state == 0)
        {
            _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(4), _sc_debugBlackScreen.getFadingTime());
        }
        else if (_sc_pnjState.state == 1)
        {
            _sc_smallTalkCanvas.SetDisplay(smalltalkAnchor, _sc_smallTalkData.getText(5), _sc_debugBlackScreen.getFadingTime());
        }
    }
}
