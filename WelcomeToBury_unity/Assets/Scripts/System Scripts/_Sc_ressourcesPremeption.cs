using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_ressourcesPremeption : MonoBehaviour
{
    [SerializeField] bool createRottenItems = true;
    public static _Sc_ressourcesPremeption instance = null;
    [System.Serializable]
    public class Clean
    {
        public _So_item itemToClean;
        public int delayClean;
    }
    [SerializeField] List<Clean> cleans = new List<Clean>();
    [SerializeField] List<_So_item> rottenItems = new List<_So_item>();

    _Sc_inventoryManager _inventoryManager = null;

    int garbageNumber = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        rottenItems = new List<_So_item>();
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
        bool removedAnItem = false;
        for(int i = 0; i < cleans.Count; i++)
        {
            cleans[i].delayClean -= 1;
            if(cleans[i].delayClean <= 0)
            {
                removedAnItem = true;
                _inventoryManager.RemoveItem(cleans[i].itemToClean);
                if(createRottenItems)
                {
                    rottenItems.Add(cleans[i].itemToClean.rottenVersion);
                }
                cleans.Remove(cleans[i]);                
                i -= 1;
            }
        }        
        StartCoroutine(DelayAddRottenItems(removedAnItem));
        
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

    private IEnumerator DelayAddRottenItems(bool removedItem)
    {
        yield return new WaitForSeconds(0.1f);
        if (createRottenItems == false && removedItem == true)
        {
            //_inventoryManager.Reorganize();
        }
        else
        {
            for (int i = 0; i < rottenItems.Count; i++)
            {
                _inventoryManager.AddItem(rottenItems[i]);
                rottenItems.Remove(rottenItems[i]);
                i -= 1;
            }
        }
        
    }
}
