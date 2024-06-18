using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Sc_CraftManager : MonoBehaviour
{
    _Sc_craftItem currentItem = null;
    public Image customCursor = null;

    public _Sc_craftSlot[] craftingSlots = null;
    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if (currentItem != null)
            {
                customCursor.gameObject.SetActive(false);
                _Sc_craftSlot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach(_Sc_craftSlot slot in craftingSlots)
                {
                    float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);

                    if(dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        nearestSlot = slot;
                    }
                }                
                nearestSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                nearestSlot.gameObject.SetActive(true);
                nearestSlot._sc_craftItem = currentItem;
                currentItem = null;
            }
        }
        
    }
    public void OnMouseDownItem( _Sc_craftItem item)
    {
        if(currentItem == null)
        {
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
        }
    }
}
