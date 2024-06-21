using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class _Sc_inventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            _Sc_inventoryItem _sc_inventoryItem = eventData.pointerDrag.GetComponent<_Sc_inventoryItem>();
            _sc_inventoryItem.parentAfterDrag = transform;
        }
        else
        {
            _Sc_inventoryItem _sc_inventoryItemInSlot = transform.GetComponentInChildren<_Sc_inventoryItem>();
            _Sc_inventoryItem _sc_inventoryItemDragged = eventData.pointerDrag.GetComponent<_Sc_inventoryItem>();

            Transform currentParent = this.transform;
            Transform newParent = _sc_inventoryItemDragged.previousSlotParent;

            _sc_inventoryItemDragged.parentAfterDrag = currentParent;

            
            _sc_inventoryItemInSlot.parentAfterDrag = newParent;
            _sc_inventoryItemInSlot.EndMoveSlot();

            //SWAP ITEMS
        }
    }
}
