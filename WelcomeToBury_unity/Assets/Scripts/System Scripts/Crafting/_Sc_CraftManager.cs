using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class _Sc_CraftManager : MonoBehaviour
{
    public static _Sc_CraftManager instance = null;
    _So_item currentItem = null;
    public Image customCursor = null;

    public _Sc_craftSlot[] craftingSlots = null;
    [SerializeField] int minDist = 5;

    [SerializeField] string formula = null;

    _Sc_cookbook _sc_cookbook = null;
    _Sc_formulaDisplay _sc_formulaDisplay = null;
    _Sc_cerveau _sc_cerveau = null;

    public Image resultSlot = null;
    public _So_item garbage = null;
    bool isOpen = false;
    CanvasGroup canvaGroup = null;
    [SerializeField] Transform receipesParent = null;
     List<_Sc_receipe> receipes = null;
    _Sc_inventoryManager _sc_iventoryManager = null;

    [SerializeField] bool canHandRemoveIngredients = false;

    public _Sc_inventoryItem lastUsedIventoryItem = null;
    public _Sc_craftSlot lastUsedSlot = null;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _sc_cookbook = _Sc_cookbook.instance;
        _sc_formulaDisplay = _Sc_formulaDisplay.instance;
        _sc_iventoryManager = _Sc_inventoryManager.instance;
        canvaGroup = transform.GetComponent<CanvasGroup>();
        _sc_cerveau = _Sc_cerveau.instance;
        receipes = new List<_Sc_receipe>();
        for (int i = 0; i < receipesParent.childCount; i++)
        {
            if(receipesParent.GetChild(i).GetComponent<_Sc_receipe>() != null)
            {
                receipes.Add(receipesParent.GetChild(i).GetComponent<_Sc_receipe>());
            }
        }

        //close canvas if open on start
        canvaGroup.alpha = 0;
        canvaGroup.interactable = false;
        canvaGroup.blocksRaycasts = false;
        ClearCraftTable(false);
        isOpen = false;

    }
    private void Update()
    {
        if(isOpen)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (currentItem != null)
                {
                    customCursor.gameObject.SetActive(false);
                    _Sc_craftSlot nearestSlot = null;
                    float shortestDistance = float.MaxValue;

                    foreach (_Sc_craftSlot slot in craftingSlots)
                    {
                        float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);

                        if (dist < shortestDistance)
                        {
                            shortestDistance = dist;
                            nearestSlot = slot;
                        }
                    }
                    if (shortestDistance <= minDist)
                    {
                        if (nearestSlot._item != null)
                        {
                            if (lastUsedSlot != null)
                            {
                                lastUsedSlot.GetComponent<Image>().sprite = nearestSlot._item.image;
                                lastUsedSlot.gameObject.SetActive(true);
                                lastUsedSlot._item = nearestSlot._item;
                                lastUsedSlot.SetTool();
                                lastUsedSlot.transform.DORewind();
                                lastUsedSlot.transform.DOKill();
                                lastUsedSlot.transform.DOPunchScale(new Vector3(-0.25f, 0.25f, 0.0f), 0.35f, 10, 1);
                                lastUsedSlot = null;
                            }
                            else
                            {
                                _sc_iventoryManager.AddItem(nearestSlot._item, 1);
                            }
                        }
                        nearestSlot.GetComponent<Image>().sprite = currentItem.image;
                        nearestSlot.gameObject.SetActive(true);
                        nearestSlot._item = currentItem;
                        nearestSlot.SetTool();
                        nearestSlot.transform.DORewind();
                        nearestSlot.transform.DOKill();
                        nearestSlot.transform.DOPunchScale(new Vector3(-0.25f, 0.25f, 0.0f), 0.35f, 10, 1);
                        if (lastUsedIventoryItem != null)
                        {
                            lastUsedIventoryItem.count -= 1;
                            lastUsedIventoryItem.SetCount();
                            lastUsedIventoryItem = null;
                        }
                        else
                        {
                            StartCoroutine(removeFromInventoryDelay(currentItem));
                        }

                    }

                    currentItem = null;

                }
                lastUsedIventoryItem = null;
                lastUsedSlot = null;
            }
        }
        

        //Debug openCanvas
        if(Input.GetKeyDown(KeyCode.C))
        {
            OpenCraftCanvas();
        }
        
    }
    public void OpenCraftCanvas()
    {
        if ((isOpen))
        {            
            canvaGroup.alpha = 0;
            canvaGroup.interactable = false;
            canvaGroup.blocksRaycasts = false;
            ClearCraftTable(true);
            isOpen = false;
            _sc_cerveau.isInMenu = false;
        }
        else
        {
            lastUsedIventoryItem = null;
            lastUsedSlot = null;
            _sc_cerveau.isInMenu = true;
            for (int i = 0; i < receipes.Count; i++)
            {
                receipes[i].checkStatus();
            }
            canvaGroup.alpha = 1;
            canvaGroup.interactable = true;
            canvaGroup.blocksRaycasts = true;
            isOpen = true;
        }
    }
    //[Button]
    public void combine()
    {
        formula = null;
        _So_item result = null;
        //Obtain formula from ingredients
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            if (craftingSlots[i]._item != null)
            {
                formula += craftingSlots[i]._item.formulaId;
            }          
        }

        //Compare formula to cookbook
        if (_sc_cookbook.getMatchingReceipe(formula) != null)
        {
            result = _sc_cookbook.getMatchingReceipe(formula);
            resultSlot.sprite = result.image;         
        }
        else
        {
            if(formula != null)
            {
                result = garbage;
                resultSlot.sprite = garbage.image;
            }
            
        }
        if (formula != null)
        {
            resultSlot.gameObject.SetActive(true);
            resultSlot.transform.DORewind();
            resultSlot.transform.DOKill();
            resultSlot.transform.DOPunchScale(new Vector3(-0.25f, 0.25f, 0.0f), 0.35f, 10, 1);

            ClearCraftTable(false);
            StartCoroutine(DelaySendToInventory(result));
        }
        

    }

    public void AutoFillSlot(int slotIndex, _So_item sentItem)
    {
        craftingSlots[slotIndex].GetComponent<Image>().sprite = sentItem.image;
        craftingSlots[slotIndex]._item = sentItem;
        craftingSlots[slotIndex].gameObject.SetActive(true);
        _sc_iventoryManager.RemoveItem(sentItem);
        craftingSlots[slotIndex].SetTool();
        craftingSlots[slotIndex].transform.DORewind();
        craftingSlots[slotIndex].transform.DOKill();
        craftingSlots[slotIndex].transform.DOPunchScale(new Vector3(-0.25f, 0.25f, 0.0f), 0.35f, 10, 1);
    }

    
    public void OnMouseDownItem( _So_item item)
    {
        if(currentItem == null)
        {
            currentItem = item;
            //customCursor.gameObject.SetActive(true);
            //customCursor.sprite = currentItem.image;
        }


    }

    public int getItemInSLots(_So_item sentItem)
    {
        int itemCount = 0;
        for(int i = 0; i < craftingSlots.Length; i++)
        {
            if (craftingSlots[i]._item != null)
            {
                if (craftingSlots[i]._item == sentItem)
                {
                    itemCount +=1;
                }
            }
        }
        return itemCount;
    }

    

   /* public void OnMouseDownItem2()
    {
        if (currentItem == null)
        {
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
        }
    }*/

    public void OnMouseDownSlot(_Sc_craftSlot slot)
    {
        if (canHandRemoveIngredients)
        {
            if (currentItem == null)
            {
                if (slot._item != null)
                {
                    lastUsedSlot = slot;
                    currentItem = slot._item;
                    customCursor.gameObject.SetActive(true);
                    customCursor.sprite = currentItem.image;
                    _sc_iventoryManager.AddItem(currentItem, 1);
                    slot.GetComponent<Image>().sprite = null;
                    slot.gameObject.SetActive(false);
                    slot._item = null; ;
                   
                }

            }
        }
    }

    public void ClearCraftTable(bool cancel)
    {
        foreach (_Sc_craftSlot slot in craftingSlots)
        {
            if (cancel)
            {
                if (slot._item != null)
                {
                    _sc_iventoryManager.AddItem(slot._item, 1);
                }
            }
            slot.GetComponent<Image>().sprite = null;
            slot.gameObject.SetActive(false);
            slot._item = null;       
        }
    }

    private IEnumerator DelaySendToInventory(_So_item newItem)
    {
        yield return new WaitForSeconds(1);
        _sc_formulaDisplay.OpenFormula(false);
        resultSlot.gameObject.SetActive(false);
        resultSlot.sprite = null;
        _sc_iventoryManager.AddItem(newItem, 1);

    }

    private IEnumerator removeFromInventoryDelay(_So_item _itemToRemove)
    {
        Debug.Log("RemoveItemSend");
        yield return new WaitForSeconds(0.0f);
        _sc_iventoryManager.RemoveItem(_itemToRemove);
        Debug.Log("RemoveItemSend 2");
        /*for (int i = 0; i < craftingSlots.Length; i++)
        {
            if (craftingSlots[i]._item != null)
            {
                _sc_iventoryManager.RemoveItem(craftingSlots[i]._item);
            }
        }*/
    }
}
