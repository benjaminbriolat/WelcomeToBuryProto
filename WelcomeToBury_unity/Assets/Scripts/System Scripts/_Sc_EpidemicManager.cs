using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class _Sc_EpidemicManager : MonoBehaviour
{
    public static _Sc_EpidemicManager instance = null;
    [Header("Personnages")]
    [SerializeField] List<Transform> pnjs = null;
    [SerializeField] List<Transform> healthPnjs = null;
    [SerializeField] List<Transform> sickPnjs = null;
    [SerializeField] List<Transform> immunePnjs = null;

    [Header("Paramètres propagation")]
    [SerializeField] int progressionInterval = 3;
    [SerializeField] int debugStartValue = 3;
    [SerializeField] int debugCadenceValueMax = 3;
    [SerializeField] int debugCadenceValueMin = 2;
    [SerializeField] int debugSickCap = 11;


    [System.Serializable]
    public class Symptom
    {
        public string symptomName = null;
        public bool isActive = false;
        public float odds = 0;
    }
    [Header("Paramètres Symptomes")]
    [SerializeField] List<Symptom> symptoms = new List<Symptom>();
    [SerializeField] float totalOdds = 0;
    int chosenSymptom = 0;

    //privates
    int lastStoredDay = 1;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
               
        for (int i = 0; i < symptoms.Count; i++)
        {
            if(i >0)
            {
                symptoms[i].odds += symptoms[i - 1].odds;
            }
        }

        SpreadDisease(debugStartValue);
    }

    public void AdvancedDay(int currentDay)
    {
        if(Mathf.Abs(currentDay - lastStoredDay) >= progressionInterval)
        {
            lastStoredDay = currentDay;
            if (healthPnjs.Count > 0)
            {
                int value = Random.Range(debugCadenceValueMin, debugCadenceValueMax + 1);
                SpreadDisease(value);
            }
        }
        
    }

    public void SpreadDisease(int quantity)
    {
        if(sickPnjs.Count < debugSickCap)
        {
            if (healthPnjs.Count > 0)
            {
                int value = Random.Range(0, healthPnjs.Count);
                

                totalOdds = 0;
                for (int i = 0; i < symptoms.Count; i++)
                {
                    if (symptoms[i].isActive)
                    {
                        totalOdds = symptoms[i].odds;
                    }
                }

                float randomSympt = Random.Range(0, totalOdds);
                Debug.Log("Value = " + randomSympt);
                for (int i = 0; i < symptoms.Count; i++)
                {
                    Debug.Log("random1");
                    if (randomSympt < symptoms[i].odds)
                    {
                        chosenSymptom = i;
                        Debug.Log("random2");
                        break;
                    }
                }
               

                healthPnjs[value].GetComponent<_Sc_pnjState>().SymptomProgress(true,chosenSymptom);

                sickPnjs.Add(healthPnjs[value]);
                healthPnjs.Remove(healthPnjs[value]);
                quantity -= 1;
                if (quantity > 0)
                {
                    SpreadDisease(quantity);
                }
            }
        }             
    }
    public void CreateHealthyPop()
    {
        for (int i = 0; i < pnjs.Count; i++)
        {
            if (pnjs[i].GetComponent<_Sc_pnjState>().state <= 0)
            {
                if(healthPnjs.Contains(pnjs[i])== false)
                {
                    healthPnjs.Add(pnjs[i]);
                }
            }
        }
    }
    public void getNewSymptom( _Sc_pnjState patient)
    {
        for(int i = 0; i < symptoms.Count; i++)
        {
            if (patient.symptomes[i] == false)
            {
                symptoms[i].isActive = true;
                patient.SymptomProgress(true, i);
                break;
            }
        }
    }

    

    public void AddPnj(Transform newPnj)
    {
       if(pnjs.Contains(newPnj) == false)
       {
           pnjs.Add(newPnj);
           CreateHealthyPop();
        }
    }

    public void HealedPnj(Transform _pnj)
    {
        if(sickPnjs.Contains(_pnj))
        {
            immunePnjs.Add(_pnj);
            sickPnjs.Remove(_pnj);
        }
    }

    public void DeimmunizePnj(Transform _pnj)
    {
        if (immunePnjs.Contains(_pnj))
        {
            healthPnjs.Add(_pnj);
            immunePnjs.Remove(_pnj);
        }
    }
}
