using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Sc_DebugGiveItem : MonoBehaviour
{
    public _Sc_inventoryManager _sc_inventoryManager = null;
    public _So_item[] itemsToPickGive;
    Transform buttonParent = null;

    private void Start()
    {
        _sc_inventoryManager = _Sc_inventoryManager.instance;
        buttonParent = transform.GetChild(0);
        for(int i = 0; i < buttonParent.childCount; i++)
        {
            if (buttonParent.GetChild(i).GetComponent<Button>() != null)
            {
                if(buttonParent.GetChild(i).GetChild(0).GetComponent<Image>() != null)
                {
                    buttonParent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = itemsToPickGive[i].image;
                }
            }
        }
    }
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

    public void GiveAll()
    {
        for(int i = 0; i < itemsToPickGive.Length; i++)
        {
            GiveItem(i);
        }
    }
}
