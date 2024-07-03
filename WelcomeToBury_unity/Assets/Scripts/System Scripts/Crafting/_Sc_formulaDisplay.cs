using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Sc_formulaDisplay : MonoBehaviour
{
    public static _Sc_formulaDisplay instance = null;
    [SerializeField] List<_Sc_formulaItem> ingredients = new List<_Sc_formulaItem>();
    [SerializeField] List<Image> pluses = new List<Image>();
    CanvasGroup autoCanvasGroup = null;
    CanvasGroup canvasGroup = null;
    _Sc_inventoryManager inventoryManager = null;
    _Sc_CraftManager _sc_craftManager = null;

    
    [HideInInspector] public bool isOpen = false;
    [HideInInspector] public _Sc_receipe currentReceipe = null;
    private void Awake()
    {
        instance = this;
        canvasGroup = transform.GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        canvasGroup.alpha = 0;
        inventoryManager = _Sc_inventoryManager.instance;
        _sc_craftManager = _Sc_CraftManager.instance;
        autoCanvasGroup = transform.GetChild(2).GetChild(0).GetComponent<CanvasGroup>();
    }
    public void setDisplay(int index, _So_item newItem)
    {
        ingredients[index].SetItem(newItem);
    }

    public void OpenFormula(bool open)
    {
        if ((open == false))
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            ClearDisplay();
            isOpen = false;
            autoCanvasGroup.alpha = 0;
            autoCanvasGroup.interactable = false;
            autoCanvasGroup.blocksRaycasts = false;
            currentReceipe = null;
        }
        else
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            isOpen = true;

            if(checkIfAllItemsAvailable() == true)
            {
                autoCanvasGroup.alpha = 1;
                autoCanvasGroup.interactable = true;
                autoCanvasGroup.blocksRaycasts = true;
            }
            else
            {
                autoCanvasGroup.alpha = 0;
                autoCanvasGroup.interactable = false;
                autoCanvasGroup.blocksRaycasts = false;
            }
        }
    }
    public void SetPluses()
    {
        int ingredientTotal = 0;
        for( int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i]._item != null)
            {
                ingredientTotal += 1;
                ingredients[i].GetComponent<Image>().enabled = true;
            }
        }
        for(int i = 0; i < ingredientTotal-1; i++)
        {
            pluses[i].enabled = true;
        }
    }

    public void ClearDisplay()
    {
        for (int i = 0; i < pluses.Count; i++)
        {
            pluses[i].enabled = true;
        }
        for (int i = 0; i < ingredients.Count; i++)
        {
            ingredients[i].GetComponent<Image>().enabled = false;
            ingredients[i]._item = null;

        }
    }

    public void UpdateAutoButton()
    {
        if (isOpen == true)
        {
            bool canPrepare = true;
            for (int i = 0; i < ingredients.Count; i++)
            {
                if (inventoryManager.checkItem(ingredients[i]._item, 1, true) == false)
                {
                    canPrepare = false;
                }
            }
            if (canPrepare == true)
            {
                Debug.Log("CanPrep");
                autoCanvasGroup.alpha = 1;
                autoCanvasGroup.interactable = true;
                autoCanvasGroup.blocksRaycasts = true;
            }
            else
            {
                autoCanvasGroup.alpha = 0;
                autoCanvasGroup.interactable = false;
                autoCanvasGroup.blocksRaycasts = false;
            }
        }        
    }

    public bool checkIfAllItemsAvailable()
    {
        bool canPrepare = true;

        for (int i = 0; i < ingredients.Count; i++)
        {
            if (inventoryManager.checkItem(ingredients[i]._item, 1,true) == false)
            {
                canPrepare = false;
            }
        }
        return canPrepare;
    }

    public void AutoPrepare()
    {
        bool canPrepare = true;

        for(int i = 0; i < ingredients.Count; i++)
        {
            if (inventoryManager.checkItem(ingredients[i]._item,1,true) == false)
            {
                canPrepare = false;
            }
        }

        if(canPrepare)
        {
            _sc_craftManager.ClearCraftTable(true);
            for (int i = 0; i < ingredients.Count; i++)
            {
                _sc_craftManager.AutoFillSlot(i, ingredients[i]._item);
            }
        }
        else
        {
            Debug.Log("NotAllItems");
        }
    }
}
