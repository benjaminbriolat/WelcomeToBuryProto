using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_inventoryManager : MonoBehaviour
{
    public int maxStackItem = 64;
    public _Sc_inventorySlot[] inventorySlots;
    public GameObject iventoryItemPrefab;
    public bool AddItem(_So_item _item)
    {
        //Check slot with same item and count not max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            _Sc_inventorySlot slot = inventorySlots[i];
            _Sc_inventoryItem itemInSlot = slot.GetComponentInChildren<_Sc_inventoryItem>();
            if (itemInSlot != null && itemInSlot._item == _item && itemInSlot.count < maxStackItem && itemInSlot._item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.SetCount();
                return true;
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
