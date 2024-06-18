using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class _Sc_cookbook : MonoBehaviour
{
    public static _Sc_cookbook instance = null;
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
        public List<Ingredients> ingredients = null;
        public int discoveryProgress = 0;
        public int discoveryCap = 3;
        public bool discovered = false;
    }

    [SerializeField] List<Receipe> receipes = new List<Receipe>();

    private void Awake()
    {
        instance = this;
        for(int i = 0; i < receipes.Count; i++)
        {
            receipes[i].formula = null;

            for(int y = 0; y < receipes[i].ingredients.Count;y++)
            {
                receipes[i].formula = receipes[i].formula + receipes[i].ingredients[y];
            }
        }
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
                    }
                }                
            }
        }
    }

    public bool getDiscoveredSymptom(int symptom)
    {
        return receipes[symptom].discovered;
    }

    public string getAilmentName(int symptom)
    {
        return receipes[symptom].ailmentName;
    }

    public void GetCraftResult(string newReceipe)
    {
        for(int i = 0; i < receipes.Count; i++)
        {
            if(newReceipe == receipes[i].formula)
            {

            }
        }

    }
}
