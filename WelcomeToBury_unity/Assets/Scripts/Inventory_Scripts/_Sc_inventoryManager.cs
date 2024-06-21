using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_inventoryManager : MonoBehaviour
{
    public static _Sc_inventoryManager instance;
    public int maxStackItem = 64;
    public _Sc_inventorySlot[] inventorySlots;
    public GameObject iventoryItemPrefab;

    private void Awake()
    {
        instance = this;
    }
    public bool AddItem(_So_item _item, int _count = 1)
    {
        //Check slot with same item and count not max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            _Sc_inventorySlot slot = inventorySlots[i];
            _Sc_inventoryItem itemInSlot = slot.GetComponentInChildren<_Sc_inventoryItem>();
            if (itemInSlot != null && itemInSlot._item == _item && itemInSlot.count < maxStackItem && itemInSlot._item.stackable == true)
            {
                if(itemInSlot.count + _count <= maxStackItem)
                {
                    itemInSlot.count += _count;
                    itemInSlot.SetCount();
                    return true;
                }
                else
                {
                    for (int j = 0; j < _count; i++)
                    {
                        if (itemInSlot.count == maxStackItem)
                        {
                            itemInSlot.SetCount();
                            /*AddItem(_item, _count);
                            Debug.Log("SlotMax, CreateNexAddItem");*/
                            break;
                        }
                        else
                        {
                            itemInSlot.count++;
                            _count--;
                        }                                              
                    }
                }
            }
        }

        //Find empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            _Sc_inventorySlot slot = inventorySlots[i];
            _Sc_inventoryItem itemInSlot = slot.GetComponentInChildren<_Sc_inventoryItem>();
            if(itemInSlot == null)
            {
                SpawnNewItem(_item, slot);

                itemInSlot = slot.GetComponentInChildren<_Sc_inventoryItem>();
                itemInSlot.count = _count;
                itemInSlot.SetCount();
                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(_So_item _item, _Sc_inventorySlot _sc_inventorySlot)
    {
        GameObject newItemGameObject = Instantiate(iventoryItemPrefab, _sc_inventorySlot.transform);
        _Sc_inventoryItem _sc_inventoryItem = newItemGameObject.GetComponent<_Sc_inventoryItem>();
        _sc_inventoryItem.InitializeItem(_item);
    }
}
