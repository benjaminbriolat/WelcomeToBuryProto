using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Sc_RessourcesSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] _So_item _item;    
    [SerializeField] GameObject objectToSpawn = null;

    [Header("SapwnValues")]
    [HideInInspector] int delayResapwn = 3;
    [SerializeField] int minRespawnQuantity = 2;
    [SerializeField] int maxRespawnQuantity = 3;
    int currentSpan = 0;
    [SerializeField] int minRessourcesInLdo = 1;
    [SerializeField] int maxRessourcesInLdo = 3;
    [SerializeField] int maxAmountPossible = 6;
    int currenrAmount = 0;
    [SerializeField] float distance = 10;
    [SerializeField] int toSpawnOnStart = 3;
    [SerializeField] bool fullrespawn = false;
    [SerializeField] GameObject canvasChild = null;
    [SerializeField] bool usePicto = false;
    public enum Growth
    {
        fast,
        mid,
        slow,
    }

    public Growth growthSpeed;


    //references
    _Sc_Calendrier _calendrier = null;
    private void Start()
    {
        if(growthSpeed.ToString() == "fast")
        {
            delayResapwn = 6;
        }
        if (growthSpeed.ToString() == "mid")
        {
            delayResapwn = 9;
        }
        if (growthSpeed.ToString() == "slow")
        {
            delayResapwn = 12;
        }

        _calendrier = _Sc_Calendrier.instance;
        _calendrier.AddCrop(this.transform);
        if (toSpawnOnStart > 0)
        {
            SpawnLoop(toSpawnOnStart);
        }
        if(usePicto == true)
        {
            canvasChild.SetActive(true);
            transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = _item.image;
        }
        
    }
   
    public void OnSpanChange()
    {
        currentSpan += 1;
        if(currentSpan >= delayResapwn)
        {
            currentSpan = 0;
            int quantity = 0;
            if(fullrespawn == true)
            {
                quantity = maxAmountPossible - currenrAmount;
            }
            else
            {
                quantity = Random.Range(minRespawnQuantity, maxRespawnQuantity + 1);
            }
            SpawnLoop(quantity);
        }
    }

    public void SpawnLoop(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            SpawnItem();
        }
    }
    public void SpawnItem()
    {
        if(currenrAmount < maxAmountPossible)
        {
            GameObject clone = Instantiate(objectToSpawn, RandomPointInCircle(transform.position, distance), Quaternion.identity);
            clone.transform.position = new Vector3(clone.transform.position.x, 0.25f, clone.transform.position.z);
            clone.GetComponent<_Sc_itemLdo>()._ressourcesSpawner = this;
            clone.GetComponent<_Sc_itemLdo>()._item = _item;
            clone.GetComponent<_Sc_itemLdo>().count = Random.Range(minRessourcesInLdo, maxRessourcesInLdo + 1);
            currenrAmount += 1;
        }       
    }

    public Vector3 RandomPointInCircle(Vector3 origin, float maxRadius)
    {
        Vector3 centerOfRadius = origin;
        float radius = maxRadius;
        Vector3 target = centerOfRadius + (Vector3)(radius * UnityEngine.Random.insideUnitSphere);
        return target;
    }

    public void ItemPicked(int amount = 1)
    {
        for(int  i = 0; i < amount; i++)
        {
            currenrAmount -= 1;
        }
    }
}
