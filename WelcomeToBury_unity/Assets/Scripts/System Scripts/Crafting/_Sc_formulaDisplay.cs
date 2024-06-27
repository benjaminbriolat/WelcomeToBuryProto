using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Sc_formulaDisplay : MonoBehaviour
{
    public static _Sc_formulaDisplay instance = null;
    [SerializeField] List<_Sc_formulaItem> ingredients = new List<_Sc_formulaItem>();
    [SerializeField] List<Image> pluses = new List<Image>();
    CanvasGroup canvasGroup = null;
    bool isOpen = false;
    private void Awake()
    {
        instance = this;
        canvasGroup = transform.GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        canvasGroup.alpha = 0;
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
        }
        else
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            isOpen = true;
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
}
