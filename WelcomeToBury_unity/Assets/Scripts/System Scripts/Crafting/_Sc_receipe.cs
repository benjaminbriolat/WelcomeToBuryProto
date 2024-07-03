using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class _Sc_receipe : MonoBehaviour
{
    [SerializeField] _So_item itemToCraft = null;
    [SerializeField] List<_So_item> items = null;
    _Sc_formulaDisplay _sc_formulaDisplay = null;
    _Sc_cookbook _sc_cookBook = null;
    [SerializeField] TextMeshProUGUI resultName = null;
    [SerializeField] Image resultPicto = null;

    private void Start()
    {
        items = new List<_So_item>();
        _sc_formulaDisplay = _Sc_formulaDisplay.instance;
        _sc_cookBook = _Sc_cookbook.instance;
        _sc_cookBook.SetReceipe(itemToCraft.formulaId, this);
    }
    public void AddIngredient(_So_item newIngredient)
    {
        items.Add(newIngredient);       
    }
    public void SetNameAndPicto(_So_item newItem)
    {
        resultName.text = newItem.itemName;
        resultPicto.sprite = newItem.image;
    }
    public void checkStatus()
    {
        Debug.Log("activateReceipe");
        transform.gameObject.SetActive(_sc_cookBook.CheckIfReceipeDiscovered(itemToCraft.formulaId));       
    }
    public void SendFormula()
    {
        _sc_formulaDisplay.ClearDisplay();
        for (int i = 0; i< items.Count; i++)
        {
            _sc_formulaDisplay.setDisplay(i, items[i]);
        }
        _sc_formulaDisplay.SetPluses();
        if(_sc_formulaDisplay.currentReceipe == this)
        {
            if (_sc_formulaDisplay.isOpen == false)
            {
                _sc_formulaDisplay.OpenFormula(true);
            }
            else
            {
                _sc_formulaDisplay.OpenFormula(false);
            }
        }
        else
        {
            _sc_formulaDisplay.OpenFormula(true);
        }
        
    }
}
