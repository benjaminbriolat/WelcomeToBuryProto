using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_ressourcesPremeption : MonoBehaviour
{
    public static _Sc_ressourcesPremeption instance = null;
    [System.Serializable]
    public class Clean
    {
        public _So_item itemToClean;
        public int delayClean;
    }
    [SerializeField] List<Clean> cleans = new List<Clean>();

    _Sc_inventoryManager _inventoryManager = null;

    [SerializeField] _So_item garbage = null;

    int garbageNumber = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        cleans = new List<Clean>();
        _inventoryManager = _Sc_inventoryManager.instance;
    }
    public void AddItem(_So_item newItem, int delay, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Clean newClean = new Clean();
            cleans.Add(newClean);
            cleans[cleans.Count - 1].itemToClean = newItem;
            cleans[cleans.Count - 1].delayClean = delay;
        }        
    }

    public void OnSpanChange()
    {
        for(int i = 0; i < cleans.Count; i++)
        {
            cleans[i].delayClean -= 1;
            if(cleans[i].delayClean <= 0)
            {
                _inventoryManager.RemoveItem(cleans[i].itemToClean);
                cleans.Remove(cleans[i]);                
                i -= 1;
                garbageNumber +=1;
            }
        }
        StartCoroutine(DelayAddGarbage());
    }

    public void ClearList(_So_item itemToRemove)
    {
        for (int i = 0; i < cleans.Count; i++)
        {            
            if (cleans[i].itemToClean == itemToRemove)
            {
                cleans.Remove(cleans[i]);
                i -= 1;
            }
        }
    }

    private IEnumerator DelayAddGarbage()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < garbageNumber; i++)
        {
            _inventoryManager.AddItem(garbage);
        }
        garbageNumber = 0;
    }
}
