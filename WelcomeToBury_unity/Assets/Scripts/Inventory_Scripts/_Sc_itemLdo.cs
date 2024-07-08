using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_itemLdo : MonoBehaviour
{
    public _So_item _item;
    public int count = 1;
    _Sc_tooltipTrigger _sc_tolltipTrigger = null;
    _Sc_inventoryManager _inventoryManager;
    [HideInInspector] public _Sc_RessourcesSpawner _ressourcesSpawner = null;
    private void Awake()
    {
        _sc_tolltipTrigger = transform.GetComponentInChildren<_Sc_tooltipTrigger>();
    }

    private void Start()
    {
        _inventoryManager = _Sc_inventoryManager.instance;
        if (_sc_tolltipTrigger != null )
        {
            if(count > 1)
            {
                _sc_tolltipTrigger.header = _item.itemName.ToString() + "(" + count.ToString() + ")";
            }
            else
            {
                _sc_tolltipTrigger.header = _item.itemName.ToString();
            }      
        }
    }

    public void GetItem()
    {
        if(_Sc_inventoryManager.instance.inventoryFull == false)
        {
            _inventoryManager.AddItem(_item, count);
            if(_ressourcesSpawner != null)
            {
                _ressourcesSpawner.ItemPicked();
            }
            Destroy(gameObject);
        }
    }
}
