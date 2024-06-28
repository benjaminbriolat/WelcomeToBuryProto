using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static _Sc_EpidemicManager;

public class _Sc_pnjState : MonoBehaviour
{
    public enum State
    {
        Default,
        Selected,
    }

    public State CurrentState;

    [Expandable]
    public _So_pnjInfos _so_pnjInfos;

    public int GroupLevel = 0;
    public bool capTrustReached = false;

    [Header("-Symptomes")]
    public bool symptome1 = false;
    public bool symptome2 = false;
    public bool symptome3 = false;
    public bool symptome4 = false;
    public List<bool> symptomes = new List<bool>();
    public int totalImmuneDays = 3;
    public int currentImmuneDays = 0;

    [Header("-Progression")]
    public int progressionCap = 5;
    public int currentProgression = 0;
    public int maxSymptoms = 4;
    public int currentSymptoms = 0;

    [Header("-Recidives")]
    public bool recidive1 = false;
    public bool recidive2 = false;
    public bool recidive3 = false;
    public bool recidive4 = false;
    [Space(10)]

    public bool cracheurDeNoir = false;
    [Space(10)]
    public int state = 0;

    [SerializeField] Transform pnjGroupsParent;
    [SerializeField] Transform myPnjGroup;

    _Sc_pnjColor _sc_pnjColor = null;

    Transform symptomeLayoutGroup = null;
    Transform pnjActionsUiParent = null;

    _Sc_selectPnj _sc_SelectPnj = null;
    _Sc_EpidemicManager _sc_epidemiManager = null;
    _Sc_Calendrier _sc_calendrier = null;

    public bool DialogueOk = false;
    public bool SoucierOk = false;
    public bool hasSpokenToPlayer = false;

    //Buttons booleans
    public bool CanDialogue = true;
    public bool CanSoucier = false;
    public bool CanRemede = false;
    public bool CanGesteSoin = false;

    _Sc_pnjActionsParent _sc_PnjActionsParent = null;
    _Sc_DebugBlackScreen _sc_debugBlackScreen = null;



    private void Awake()
    {
        gameObject.name = ("PNJ_" + _so_pnjInfos.pnjFirstName + _so_pnjInfos.pnjLastName);
        _sc_pnjColor = transform.GetChild(0).GetComponent<_Sc_pnjColor>();
        symptomeLayoutGroup = transform.GetChild(1).GetChild(1);
        pnjActionsUiParent = transform.GetChild(1).GetChild(3);
        _sc_PnjActionsParent = GetComponentInChildren<_Sc_pnjActionsParent>();
    }
    private void Start()
    {
        pnjGroupsParent = _Sc_pnjGroupParent.instance.transform;
        _sc_SelectPnj = _Sc_selectPnj.Instance;
        _sc_epidemiManager = _Sc_EpidemicManager.instance;
        _sc_calendrier = _Sc_Calendrier.instance;
        _sc_debugBlackScreen = _Sc_DebugBlackScreen.instance;
        pnjActionsUiParent.gameObject.SetActive(false);
        AddPnjToGroup();
        setSymptomeIcon();
        SetButtonsState();
        _sc_calendrier.AddPnj(this.transform);
        _sc_epidemiManager.AddPnj(this.transform);
    }

    public void SetButtonsState()
    {
        int _amitie = myPnjGroup.GetComponent<_Sc_pnjGroup>().groupTrustLevel;
        _sc_PnjActionsParent.GetButtonsState(CanDialogue, CanSoucier, CanRemede, CanGesteSoin, capTrustReached);
    }
    private void AddPnjToGroup()
    {
        myPnjGroup = pnjGroupsParent.GetChild((int)_so_pnjInfos.pnjGroup);
        myPnjGroup.GetComponent<_Sc_pnjGroup>().pnjGroup.Add(this.transform);

        
        GroupLevel = myPnjGroup.GetComponent<_Sc_pnjGroup>().groupTrustLevel;
        if (GroupLevel >= myPnjGroup.GetComponent<_Sc_pnjGroup>().capTrustLevel)
        {
            capTrustReached = true;
        }
        else
        {
            capTrustReached = false;
        }
    }

    public void SymptomProgress(bool newlySick, int symptom)
    {
        if(newlySick)
        {
            if(symptom == 0)
            {
                GiveSymptome1();
            }
            if (symptom == 1)
            {
                GiveSymptome2();
            }
            if (symptom == 2)
            {
                GiveSymptome3();
            }
            if (symptom == 3)
            {
                GiveSymptome4();
            }
            currentSymptoms += 1;
        }
    }

    public void onSymptomeChange()
    {
        if(hasSpokenToPlayer == true)
        {
            CanSoucier = true;
        }
        if (!symptome1 && !symptome2 && !symptome3 && !symptome4)
        {
            _sc_pnjColor.SetBaseColor();

            state = 0;
        }

        if (symptome1 || symptome2 || symptome3 || symptome4)
        {
            _sc_pnjColor.SetSymptomeColor();

            state = 1;
        }

        if (symptome1 && symptome2 && symptome3 && symptome4)
        {
            cracheurDeNoir = true;
            _sc_pnjColor.SetCracheurColor();

            state = 2;
        }
        else
        {
            cracheurDeNoir = false;
        }
        if(state == 0)
        {
            currentImmuneDays = totalImmuneDays;
            _sc_epidemiManager.HealedPnj(this.transform);
        }
        setSymptomeIcon();

        if(_sc_SelectPnj.lastPnjState == this)
        {
            _sc_SelectPnj.SetFichePatient(0, _sc_SelectPnj.lastPnjState);
        }
    }

    private void setSymptomeIcon()
    {
        if(symptome1)
        {
            symptomeLayoutGroup.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            symptomeLayoutGroup.GetChild(0).gameObject.SetActive(false);
        }

        if (symptome2)
        {
            symptomeLayoutGroup.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            symptomeLayoutGroup.GetChild(1).gameObject.SetActive(false);
        }

        if (symptome3)
        {
            symptomeLayoutGroup.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            symptomeLayoutGroup.GetChild(2).gameObject.SetActive(false);
        }

        if (symptome4)
        {
            symptomeLayoutGroup.GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            symptomeLayoutGroup.GetChild(3).gameObject.SetActive(false);
        }
    }

    public void SetActionsUi(bool _value)
    {
        pnjActionsUiParent.gameObject.SetActive(_value);
    }

    public void OnDayChange()
    {
        if(state == 1)
        {
            currentProgression += 1;
            if (currentProgression > progressionCap)
            {
                if(currentSymptoms < maxSymptoms)
                {
                    _sc_epidemiManager.getNewSymptom(this);
                    currentProgression = 0;
                }
            }
        }    
        else if (state == 0)
        {
            if(currentImmuneDays > 0)
            {
                currentImmuneDays -= 1;
                if(currentImmuneDays <= 0)
                {
                    _sc_epidemiManager.DeimmunizePnj(this.transform);
                }
            }

        }
    }

    //DEBUG//
    [Button] private void GiveSymptome1()
    {
        symptome1 = true;
        symptomes[0] = true;
        onSymptomeChange();
    }
    [Button]private void GiveSymptome2()
    {
        symptome2 = true;
        symptomes[1] = true;
        onSymptomeChange();
    }
    [Button]private void GiveSymptome3()
    {
        symptome3 = true;
        symptomes[2] = true;
        onSymptomeChange();
    }
    [Button]private void GiveSymptome4()
    {
        symptome4 = true;
        symptomes[3] = true;
        onSymptomeChange();
    }

    [Button]
    public void HealSymptome1()
    {
        symptome1 = false;
        symptomes[0] = false;
        onSymptomeChange();
        UpdateTrustLevel();
    }
    [Button]
    public void HealSymptome2()
    {
        symptome2 = false;
        symptomes[1] = false;
        onSymptomeChange();
        UpdateTrustLevel();
    }
    [Button]
    public void HealSymptome3()
    {
        symptome3 = false;
        symptomes[2] = false;
        onSymptomeChange();
        UpdateTrustLevel();
    }
    [Button]
    public void HealSymptome4()
    {
        symptome4 = false;
        symptomes[3] = false;
        onSymptomeChange();
        UpdateTrustLevel();
    }

    /// bouron soin + care
    [Button]
    public void HealSymptome1Care()
    {
        if (capTrustReached == true)
        {
            symptome1 = false;
            symptomes[0] = false;
            onSymptomeChange();
            UpdateTrustLevel();
        }       
    }
    [Button]
    public void HealSymptome2Care()
    {
        if (capTrustReached == true)
        {
            symptome2 = false;
            symptomes[1] = false;
            onSymptomeChange();
            UpdateTrustLevel();
        }        
    }
    [Button]
    public void HealSymptome3Care()
    {
        if (capTrustReached == true)
        {
            symptome3 = false;
            symptomes[2] = false;
            onSymptomeChange();
            UpdateTrustLevel();
        }        
    }
    [Button]
    public void HealSymptome4Care()
    {
        if(capTrustReached == true)
        {
            symptome4 = false;
            symptomes[3] = false;
            onSymptomeChange();
            UpdateTrustLevel();
        }        
    }
    public void UpdateTrustLevel()
    {
        myPnjGroup.GetComponent<_Sc_pnjGroup>().UpdateTrustLevel();
    }
}
