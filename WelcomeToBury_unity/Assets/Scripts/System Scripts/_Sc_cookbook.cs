using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class _Sc_cookbook : MonoBehaviour
{
    public static _Sc_cookbook instance = null;
    _Sc_messagesManager _sc_MessagesManager = null;

    [System.Serializable]
    public enum Ingredients
    {
        valerian,
        oil,
        beewax,
        snaildrool,
        waspspit,
    }
    [System.Serializable] 
    public class Receipe
    {
        public string name = null;
        public string ailmentName = null;
        public string formula = null;
        public List<_So_item> ingredients = null;
        public int discoveryProgress = 0;
        public int discoveryCap = 3;
        public bool discovered = false;
        public _So_item resultItem = null;
    }

    public List<Receipe> receipes = new List<Receipe>();

    private void Awake()
    {
        instance = this;
        for(int i = 0; i < receipes.Count; i++)
        {
            receipes[i].formula = null;

            for(int y = 0; y < receipes[i].ingredients.Count;y++)
            {
                receipes[i].formula = receipes[i].formula + receipes[i].ingredients[y].formulaId;
            }
        }
    }
    private void Start()
    {
        _sc_MessagesManager = _Sc_messagesManager.instance;
    }
    private void Update()
    {
        //DebugTestProgress
        /*if(Input.GetKeyDown(KeyCode.H))
        {
            AdvanceDiscovery(receipes[0].name);
        }*/
    }
    public void AdvanceDiscovery(string targetTreatment)
    {
        for(int i = 0; i < receipes.Count; i++)
        {
            if (receipes[i].name == targetTreatment)
            {
                if (receipes[i].discovered == false)
                {
                    receipes[i].discoveryProgress += 1;
                    if (receipes[i].discoveryProgress >= receipes[i].discoveryCap)
                    {
                        receipes[i].discovered = true;
                        StartCoroutine(RemedeProgressFeedback(i+1, true));
                    }
                    else
                    {
                        StartCoroutine(RemedeProgressFeedback(i + 1, false));
                    }
                }                
            }
        }
    }

    public void SetReceipe(string treatmentName, _Sc_receipe targetReceipe)
    {
        Debug.Log("SetReceipe sentname = " + treatmentName);
        int matchingRecipeIndex = 0;
        bool matchfound = false;
        for(int i = 0; i < receipes.Count; i++)
        {
            if (receipes[i].name == treatmentName)
            {
                matchingRecipeIndex = i;
                matchfound = true;
                break;
            }
        }
        if(matchfound == true)
        {
            Debug.Log("SetReceipe sentname matchfound");
            for (int i = 0; i < receipes[matchingRecipeIndex].ingredients.Count; i++)
            {
                Debug.Log("SetReceipe SendValue");
                targetReceipe.AddIngredient(receipes[matchingRecipeIndex].ingredients[i]);
            }
            targetReceipe.SetNameAndPicto(receipes[matchingRecipeIndex].resultItem);
        }
    }

    public bool CheckIfReceipeDiscovered(string treatmentName)
    {
        int matchingRecipeIndex = 0;
        for (int i = 0; i < receipes.Count; i++)
        {
            if (receipes[i].name == treatmentName)
            {
                matchingRecipeIndex = i;
                break;
            }
        }
        return receipes[matchingRecipeIndex].discovered;
    }
    
    public _So_item getRemede(string _symptome)
    {
        _So_item matchingItem = null;
        for (int i = 0; i < receipes.Count; i++)
        {
            if (receipes[i].name == _symptome)
            {
                matchingItem = receipes[i].resultItem;
            }
        }
        return matchingItem;
    }

    public _So_item getMatchingReceipe(string newFormula)
    {
        _So_item matchingItem = null;
        for (int i = 0; i < receipes.Count; i++)
        {
            if (receipes[i].formula == newFormula)
            {
                matchingItem = receipes[i].resultItem;
            }
        }
        return matchingItem;
    }
    public bool getDiscoveredSymptom(int symptom)
    {
        return receipes[symptom].discovered;
    }

    public string getAilmentName(int symptom)
    {
        return receipes[symptom].ailmentName;
    }

    private IEnumerator RemedeProgressFeedback(int remede, bool discovered)
    {
        _Sc_Calendrier _sc_calendrier = _Sc_Calendrier.instance;
        _Sc_DebugBlackScreen _sc_blackScreen = _Sc_DebugBlackScreen.instance;

        float _waitTime = 0f;
        if(_sc_calendrier.currentPlage < 3)
        {
            _waitTime = _sc_blackScreen.fadeTime + _sc_blackScreen.spanWait;
        }
        else
        {
            _waitTime = _sc_blackScreen.fadeTime + _sc_blackScreen.dayWait;
        }

        yield return new WaitForSeconds(_waitTime + 1.0f);
        if(discovered == false)
        {
            _sc_MessagesManager.SetMessageText("Votre compréhension"+ " " + "progresse !", true);
        }
        else
        {
            _sc_MessagesManager.SetMessageText("Vous avez découvert la recette du" + " " + "Remède" + remede.ToString() + "!", true);
        }

        if (_Sc_selectPnj.Instance.lastPnjState != null)
        {
            _Sc_selectPnj.Instance.SetFichePatient(0, _Sc_selectPnj.Instance.lastPnjState);
            _Sc_selectPnj.Instance.lastPnjState.SetButtonsState();
        }
    }
}
