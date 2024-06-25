using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Sc_formulaDisplay : MonoBehaviour
{
    public static _Sc_formulaDisplay instance = null;
    [SerializeField] List<Image> ingredients = new List<Image>();
    [SerializeField] List<Image> pluses = new List<Image>();
    CanvasGroup canvasGroup = null;
    bool isOpen = false;
    private void Awake()
    {
        instance = this;
        canvasGroup = transform.GetComponent<CanvasGroup>();
    }
    public void setDisplay(int index, Sprite newSprite)
    {
        ingredients[index].sprite = newSprite;       
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
            if (ingredients[i].sprite != null)
            {
                ingredientTotal += 1;
                ingredients[i].enabled = true;
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
            ingredients[i].enabled = false;
            ingredients[i].sprite = null;

        }
    }
}
