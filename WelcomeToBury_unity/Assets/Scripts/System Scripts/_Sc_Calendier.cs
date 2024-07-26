using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Rewired;

public class _Sc_Calendrier : MonoBehaviour
{
    public static _Sc_Calendrier instance = null;
    [Header("UI")]
    [SerializeField] TextMeshProUGUI dayText = null;
    [SerializeField] _Sc_LoadTextLanguage plageText = null;
    public int currentDay = 1;
    public int currentPlage = 1;
    _Sc_EpidemicManager _sc_epidemicManager = null;
    [Header("Affected Objects")]
    [SerializeField] List<Transform> pnjs = null;
    [SerializeField] List<Transform> crops = null;
    _Sc_ressourcesPremeption _ressourcesPremenption = null;
    [SerializeField] List<string> spanNames = null;
    [SerializeField] List<Color> lightValues = null;
    [SerializeField] List<Light> _light = null;
    public bool debugNofade = false;
    public bool debugAutoSpread = false;
    public bool debugNoChaptering = false;
    [SerializeField] bool debugAffectLight = false;

    [System.Serializable]
    public class pnjSets
    {
        public GameObject pnjGameObject = null;
        public bool sick1 = false;
        public bool sick2 = false;
        public bool sick3 = false;
        public bool sick4 = false;
        public Vector2 newPos = Vector2.zero;
        public So_Dialogue _dataNormal = null;        
        public So_Dialogue _dataMalade = null;
        public So_Dialogue _dataConfiance = null;
    }

    [System.Serializable]
    public class Evenement
    {
        public string eventTitle = null;
        public  int triggerDay = 0;
        public List<pnjSets> pnjsToActivate = null;
        public List<GameObject> buildingsToActivate = null;
        public List<GameObject> pnjsToDesactivate = null;
        public List<pnjSets> pnjsToSicken = null;
        public List<pnjSets> pnjsToPlace = null;
        public List<pnjSets> pnjsToUpdate = null;        
        public bool greatEclipse = false;
        public _So_eclipseTextTrack eclipseTextData = null;
        public Vector3 newPlayerPosition = Vector3.zero;
        public bool passed = false;
    }

   
    [SerializeField] List<Evenement> evenements = new List<Evenement>();

    [SerializeField] NavMeshSurface navigationMesh = null;
    Transform player = null;
    _Sc_smallTalkCanvas _smallTalkCanvas = null;
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
        plageText.setText("",(spanNames[currentPlage -1]),"");
        _sc_epidemicManager = _Sc_EpidemicManager.instance;
        _ressourcesPremenption = _Sc_ressourcesPremeption.instance;
        _smallTalkCanvas = _Sc_smallTalkCanvas.instance;
        SetLight();
        player = _Sc_cerveau.instance.transform;
        if (debugNoChaptering == false)
        {
            LaunchFirstDay();
        }
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

            if (debugNoChaptering == false)
            {
                bool matchFound = false;
                int targetEvent = 0;
                for (int i = 0; i < evenements.Count; i++)
                {
                    if (evenements[i].triggerDay == currentDay && evenements[i].passed == false)
                    {
                        matchFound = true;
                        targetEvent = i;
                        StartCoroutine(delayLaunchEvent(i));
                        break;
                    }
                }
                if(matchFound == true && evenements[targetEvent].greatEclipse == true)
                {
                    _Sc_DebugBlackScreen.instance.SetBlackScreen(true, false,true, evenements[targetEvent].eclipseTextData);
                }
                else
                {
                    _Sc_DebugBlackScreen.instance.SetBlackScreen(true, true);
                }
            }
            else
            {
                if (debugNofade == true)
                {
                    EndAdvanceCalendar(true);
                }
                else
                {
                    _Sc_DebugBlackScreen.instance.SetBlackScreen(true, true);
                }

            }
            
        }
              
    }
    public void LaunchFirstDay()
    {
        //desactivate all buildings
        for(int i = 0; i < evenements.Count; i++)
        {
            for(int j = 0; j < evenements[i].buildingsToActivate.Count; j ++)
            {
                evenements[i].buildingsToActivate[j].SetActive(false);
            }
        }

        for (int i = 0; i < evenements.Count; i++)
        {
            for (int j = 0; j < evenements[i].pnjsToActivate.Count; j++)
            {
                evenements[i].pnjsToActivate[j].pnjGameObject.SetActive(false);
            }
        }
        // bake
        CallBake();

    }

    public void LaunchNewEvent(int newEvent)
    {
        

        if(_smallTalkCanvas.transform.parent != null)
        {
            _smallTalkCanvas.transform.SetParent(null);
        }
        //block event for future loops
        evenements[newEvent].passed = true;

        // activate buildings
        for (int i = 0; i < evenements[newEvent].buildingsToActivate.Count;i++)
        {
            evenements[newEvent].buildingsToActivate[i].SetActive(true);
        }
        // activate pnjs and set as sick
        for (int i = 0; i < evenements[newEvent].pnjsToActivate.Count; i++)
        {
            evenements[newEvent].pnjsToActivate[i].pnjGameObject.SetActive(true);
            if(evenements[newEvent].pnjsToActivate[i].sick1 == true)
            {
                evenements[newEvent].pnjsToActivate[i].pnjGameObject.transform.GetComponent<_Sc_pnjState>().SymptomProgress(true, 0);
            }
            if (evenements[newEvent].pnjsToActivate[i].sick2 == true)
            {
                evenements[newEvent].pnjsToActivate[i].pnjGameObject.transform.GetComponent<_Sc_pnjState>().SymptomProgress(true, 2);
            }
            if (evenements[newEvent].pnjsToActivate[i].sick3 == true)
            {
                evenements[newEvent].pnjsToActivate[i].pnjGameObject.transform.GetComponent<_Sc_pnjState>().SymptomProgress(true, 2);
            }
            if (evenements[newEvent].pnjsToActivate[i].sick4 == true)
            {
                evenements[newEvent].pnjsToActivate[i].pnjGameObject.transform.GetComponent<_Sc_pnjState>().SymptomProgress(true, 3);
            }
        }
        // give symptoms to existing pnjs
        for (int i = 0; i < evenements[newEvent].pnjsToSicken.Count; i++)
        {
            /*for(int j = 0; j < evenements[newEvent].pnjsToSicken[i].symptoms.Length; j ++)
            {
                if (evenements[newEvent].pnjsToSicken[i].symptoms[j] == true)
                {
                    evenements[newEvent].pnjsToSicken[i].pnjGameObject.transform.GetComponent<_Sc_pnjState>().SymptomProgress(true, i);
                }
            }*/

            if(evenements[newEvent].pnjsToSicken[i].pnjGameObject.activeSelf ==true)
            {
                if (evenements[newEvent].pnjsToSicken[i].sick1 == true)
                {
                    evenements[newEvent].pnjsToSicken[i].pnjGameObject.transform.GetComponent<_Sc_pnjState>().SymptomProgress(true, 0);
                }
                if (evenements[newEvent].pnjsToSicken[i].sick2 == true)
                {
                    evenements[newEvent].pnjsToSicken[i].pnjGameObject.transform.GetComponent<_Sc_pnjState>().SymptomProgress(true, 2);
                }
                if (evenements[newEvent].pnjsToSicken[i].sick3 == true)
                {
                    evenements[newEvent].pnjsToSicken[i].pnjGameObject.transform.GetComponent<_Sc_pnjState>().SymptomProgress(true, 2);
                }
                if (evenements[newEvent].pnjsToSicken[i].sick4 == true)
                {
                    evenements[newEvent].pnjsToSicken[i].pnjGameObject.transform.GetComponent<_Sc_pnjState>().SymptomProgress(true, 3);
                }
            }            
        }

        // Update PNJs dialogues
        for (int i = 0; i < evenements[newEvent].pnjsToUpdate.Count; i++)
        {
            evenements[newEvent].pnjsToUpdate[i].pnjGameObject.transform.GetComponent<_Sc_pnjDialogue>().ChangeDialogues(
                evenements[newEvent].pnjsToUpdate[i]._dataNormal,
                evenements[newEvent].pnjsToUpdate[i]._dataMalade,
                evenements[newEvent].pnjsToUpdate[i]._dataConfiance
                
                );
        }

        // desactivate PNJs
        for (int i = 0; i < evenements[newEvent].pnjsToDesactivate.Count; i++)
        {
            evenements[newEvent].pnjsToDesactivate[i].SetActive(false);
        }
        // Move PNJs
        for (int i = 0; i < evenements[newEvent].pnjsToPlace.Count; i++)
        {
            evenements[newEvent].pnjsToPlace[i].pnjGameObject.transform.GetComponent<_Sc_pnjState>().setNewPosition(evenements[newEvent].pnjsToPlace[i].newPos);
        }
        // Place player based on story progress
        if(evenements[newEvent].greatEclipse == true)
        {
            Debug.Log("Move player at " + evenements[newEvent].newPlayerPosition);
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.GetComponent<NavMeshModifier>().enabled = false;
            player.position = evenements[newEvent].newPlayerPosition;
            player.GetComponent<NavMeshAgent>().enabled = true;
            player.GetComponent<NavMeshModifier>().enabled = true;
        }


        // REBAKE SCENE
        if(evenements[newEvent].buildingsToActivate.Count> 0 || evenements[newEvent].pnjsToDesactivate.Count> 0 || evenements[newEvent].pnjsToActivate.Count >0)
        {
            Debug.Log("Bake called");
            CallBake();
        }
        
    }

    public void SetLight()
    {
        if(debugAffectLight == true)
        {
            for(int i = 0; i < _light.Count; i++)
            {
                _light[i].color = lightValues[currentPlage - 1];
            }
            
        }
    }

    public void EndAdvanceCalendar(bool day)
    {
        if(day == true)
        {
            if (_sc_epidemicManager != null)
            {
                if(debugAutoSpread == true)
                {
                    _sc_epidemicManager.AdvancedDay(currentDay);
                }
            } 
        }

        //envoyer progression statut aux PNJS
        for (int i = 0; i < pnjs.Count; i++)
        {
            pnjs[i].GetComponent<_Sc_pnjState>().OnDayChange();
        }

        //envoyer repousse au ressources de CROPS
        for (int i = 0; i < crops.Count; i++)
        {
            crops[i].GetComponent<_Sc_RessourcesSpawner>().OnSpanChange();
        }

        //envoyer progression aux ressource de l'inventaire
        _ressourcesPremenption.OnSpanChange();

        dayText.text = currentDay.ToString();
        plageText.setText("", spanNames[currentPlage - 1],"");
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

    public void CallBake()
    {
        navigationMesh.BuildNavMesh();
    }

    private IEnumerator delayLaunchEvent(int newEvent)
    {
        yield return new WaitForSeconds(1f);
        LaunchNewEvent(newEvent);

    }
}
