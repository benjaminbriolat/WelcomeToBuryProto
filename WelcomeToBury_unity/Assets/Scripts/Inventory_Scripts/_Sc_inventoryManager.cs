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
    public bool inventoryFull = false;
    _Sc_ressourcesPremeption _ressourcesPeremtion;
    _Sc_messagesManager _sc_messageManager = null;
    bool canShowMessageRotten = true;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _ressourcesPeremtion = _Sc_ressourcesPremeption.instance;
        _sc_messageManager = _Sc_messagesManager.instance;
    }
    public bool AddItem(_So_item _item, int _count = 1)
    {
        if (inventoryFull == false)
        {
            if(_item.Rotten == false)
            {
                if (_count > 1)
                {
                    _sc_messageManager.SetMessageText("+" + _item.itemName.ToString() + "(" + _count.ToString() + ("") + ")");
                }
                else
                {
                    _sc_messageManager.SetMessageText("+" + _item.itemName.ToString());
                }

            }
            else if (_item.Rotten == true)
            {
                if (canShowMessageRotten == true)
                {
                    canShowMessageRotten = false;
                    _sc_messageManager.SetMessageText("Some resources have rotted");
                    StartCoroutine(DelayRottenMessage());
                }
            }       
        }


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
                    if(_item.peremptible == true)
                    {
                        _ressourcesPeremtion.AddItem(_item, _item.peremption, _count);
                    }
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
            if (inventoryFull == true)
            {
                Debug.Log("InventoryFull");
            }
            _Sc_inventorySlot slot = inventorySlots[i];
            _Sc_inventoryItem itemInSlot = slot.GetComponentInChildren<_Sc_inventoryItem>();
            if(itemInSlot == null)
            {
                SpawnNewItem(_item, slot,_count);

                itemInSlot = slot.GetComponentInChildren<_Sc_inventoryItem>();
                itemInSlot.count = _count;
                itemInSlot.SetCount();
                
                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(_So_item _item, _Sc_inventorySlot _sc_inventorySlot, int _count)
    {        
        
        GameObject newItemGameObject = Instantiate(iventoryItemPrefab, _sc_inventorySlot.transform);
        _Sc_inventoryItem _sc_inventoryItem = newItemGameObject.GetComponent<_Sc_inventoryItem>();
        _sc_inventoryItem.InitializeItem(_item);
        if (_item.peremptible == true)
        {
            _ressourcesPeremtion.AddItem(_item, _item.peremption, _count);
        }
        CheckInventory();
    }

    public void RemoveItem(_So_item removalTarget)
    {
        Debug.Log("RemoveItem received");
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].transform.childCount > 0)
            {
                if(inventorySlots[i].transform.GetChild(0).GetComponent<_Sc_inventoryItem>()._item == removalTarget)
                {
                    inventorySlots[i].transform.GetChild(0).GetComponent<_Sc_inventoryItem>().count -= 1;
                    inventorySlots[i].transform.GetChild(0).GetComponent<_Sc_inventoryItem>().SetCount();
                    Debug.Log("RemoveItem Done");
                }
            }
        }
    }

    public void CheckInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            _Sc_inventorySlot Slot = inventorySlots[i];
            _Sc_inventoryItem itemInSlot = Slot.GetComponentInChildren<_Sc_inventoryItem>();
            if (itemInSlot == null)
            {
                inventoryFull = false;
                break;
            }
            else
            {
                inventoryFull = true;
            }
        }
    }

    public bool checkItem(_So_item _item, int countNeeded)
    {
        Debug.Log("itemreceived = " + _item.name);
        bool hasItem = false;
        int itemCount = 0;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            _Sc_inventorySlot Slot = inventorySlots[i];
            _Sc_inventoryItem _sc_inventoryItem = Slot.GetComponentInChildren<_Sc_inventoryItem>();

            if(_sc_inventoryItem != null)
            {
                _So_item itemInSlot = _sc_inventoryItem._item;

                 if(itemInSlot != null)
                {
                    if (itemInSlot == _item)
                    {
                        hasItem = true;
                        itemCount += _sc_inventoryItem.count;
                    }
                }
            }  
        }

        Debug.Log("HasItem = " + hasItem);
        Debug.Log("ItemCount = " + itemCount);

        if(hasItem == true && itemCount >= countNeeded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator CheckInventoryDelay()
    {
        yield return new WaitForSeconds(0.1f);
        CheckInventory();
    }

    public IEnumerator DelayRottenMessage()
    {
        yield return new WaitForSeconds(1.0f);
        canShowMessageRotten = true;
    }
}
