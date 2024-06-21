using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_DebugGiveItem : MonoBehaviour
{
    public _Sc_inventoryManager _sc_inventoryManager = null;
    public _So_item[] itemsToPickGive;

    public void GiveItem(int id)
    {
        bool result = _sc_inventoryManager.AddItem(itemsToPickGive[id]);

        if(result == true)
        {
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("Item not added");
        }
    }
}
