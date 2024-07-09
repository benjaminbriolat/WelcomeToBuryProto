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

    _Sc_tooltipTrigger _sc_tooltipTrigger = null;

    [SerializeField] _So_item _so_remede1;
    [SerializeField] _So_item _so_remede2;
    [SerializeField] _So_item _so_remede3;
    [SerializeField] _So_item _so_remede4;

    public bool receivedSeSoucier = false;

    Transform raySocket = null;
    [SerializeField] LayerMask _layerMask;
    private void Awake()
    {
        gameObject.name = ("PNJ_" + _so_pnjInfos.pnjFirstName + _so_pnjInfos.pnjLastName);
        _sc_pnjColor = transform.GetChild(0).GetComponent<_Sc_pnjColor>();
        symptomeLayoutGroup = transform.GetChild(1).GetChild(1);
        pnjActionsUiParent = transform.GetChild(1).GetChild(3);
        _sc_PnjActionsParent = GetComponentInChildren<_Sc_pnjActionsParent>();
        _sc_tooltipTrigger = GetComponentInChildren<_Sc_tooltipTrigger>();
        raySocket = transform.GetChild(4);
    }
    private void Start()
    {
        pnjGroupsParent = _Sc_pnjGroupParent.instance.transform;
        _sc_SelectPnj = _Sc_selectPnj.Instance;
        _sc_epidemiManager = _Sc_EpidemicManager.instance;
        _sc_calendrier = _Sc_Calendrier.instance;
        _sc_debugBlackScreen = _Sc_DebugBlackScreen.instance;
        pnjActionsUiParent.GetComponent<CanvasGroup>().alpha = 0;
        AddPnjToGroup();
        setSymptomeIcon();
        SetButtonsState();
        _sc_calendrier.AddPnj(this.transform);
        _sc_epidemiManager.AddPnj(this.transform);

        if(_sc_tooltipTrigger != null)
        {
            _sc_tooltipTrigger.header = _so_pnjInfos.pnjFirstName + " " + _so_pnjInfos.pnjLastName;
        }

        transform.position = new Vector3(transform.position.x, GetHeight() + 1.7f, transform.position.z);
    }

    public void SetButtonsState()
    {
        int _amitie = myPnjGroup.GetComponent<_Sc_pnjGroup>().groupTrustLevel;

        bool _canRemedeCheck = false;
        bool _canGesteSoinCheck = false;

        _So_item _so_remedeToCheck = null;
        string treatmentToCheck = null;

        if (state == 1)
        {
            
            if (symptome1 == true)
            {
                _so_remedeToCheck = _so_remede1;
                treatmentToCheck = "treatment1";
            }
            else if (symptome2 == true)
            {
                _so_remedeToCheck = _so_remede2;
                treatmentToCheck = "treatment2";
            }
            else if (symptome3 == true)
            {
                _so_remedeToCheck = _so_remede3;
                treatmentToCheck = "treatment3";
            }
            else if (symptome4 == true)
            {
                _so_remedeToCheck = _so_remede4;
                treatmentToCheck = "treatment4";
            }

            if (_Sc_cookbook.instance.CheckIfReceipeDiscovered(treatmentToCheck) == true) //temp allow remede sans se soucier
            {
                if (_Sc_inventoryManager.instance.checkItem(_so_remedeToCheck, 1) == true)
                {
                    CanRemede = true;
                    _canRemedeCheck = true;
                }
            }
            //

            if (CanRemede == true)
            {
                if(_Sc_cookbook.instance.CheckIfReceipeDiscovered(treatmentToCheck) == true)
                {
                    if (_Sc_inventoryManager.instance.checkItem(_so_remedeToCheck, 1) == true)
                    {
                        _canRemedeCheck = true;
                    }
                    else
                    {
                        _canRemedeCheck = false;
                    }
                }
                else
                {
                    _canRemedeCheck = false;
                }
            }
            else
            {
                _canRemedeCheck = false;
            }

            if(capTrustReached == true && (_Sc_cookbook.instance.CheckIfReceipeDiscovered(treatmentToCheck) == true))
            {
                _canGesteSoinCheck = true;
            }
        }
        else
        {
            _canRemedeCheck = false;
            _canGesteSoinCheck = false;
        }

        _sc_PnjActionsParent.GetButtonsState(CanDialogue, CanSoucier, _canRemedeCheck, _canGesteSoinCheck, capTrustReached);
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

        SetButtonsState();
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
        if(_value)
        {
            pnjActionsUiParent.GetComponent<CanvasGroup>().alpha = 1;
            pnjActionsUiParent.GetComponent<CanvasGroup>().blocksRaycasts = true;
            pnjActionsUiParent.GetComponent<CanvasGroup>().interactable = true;
        }
        else
        {
            pnjActionsUiParent.GetComponent<CanvasGroup>().alpha = 0;
            pnjActionsUiParent.GetComponent<CanvasGroup>().blocksRaycasts = false;
            pnjActionsUiParent.GetComponent<CanvasGroup>().interactable = false;
        }
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
            else
            {
                _sc_epidemiManager.DeimmunizePnj(this.transform);
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
        currentSymptoms -= 1;
        currentProgression = 0;
        onSymptomeChange();
        UpdateTrustLevel();
    }
    [Button]
    public void HealSymptome2()
    {
        symptome2 = false;
        symptomes[1] = false;
        currentSymptoms -= 1;
        currentProgression = 0;
        onSymptomeChange();
        UpdateTrustLevel();
    }
    [Button]
    public void HealSymptome3()
    {
        symptome3 = false;
        symptomes[2] = false;
        currentSymptoms -= 1;
        currentProgression = 0;
        onSymptomeChange();
        UpdateTrustLevel();
    }
    [Button]
    public void HealSymptome4()
    {
        symptome4 = false;
        symptomes[3] = false;
        currentSymptoms -= 1;
        currentProgression = 0;
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
            currentSymptoms -= 1;
            currentProgression = 0;
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
            currentSymptoms -= 1;
            currentProgression = 0;
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
            currentSymptoms -= 1;
            currentProgression = 0;
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
            currentSymptoms -= 1;
            currentProgression = 0;
            onSymptomeChange();
            UpdateTrustLevel();
        }        
    }
    public void UpdateTrustLevel()
    {
        myPnjGroup.GetComponent<_Sc_pnjGroup>().UpdateTrustLevel();
        if (_Sc_selectPnj.Instance.lastPnjState != null)
        {
            _Sc_selectPnj.Instance.SetFichePatient(0, _Sc_selectPnj.Instance.lastPnjState);
            _Sc_selectPnj.Instance.lastPnjState.SetButtonsState();
            Debug.Log("CookBookSetFichePatient & button");
        }
    }

    public float GetHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(raySocket.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, _layerMask))
        {
            return hit.point.y;
        }
        else
        {
            return 0;
        }
    }
}
