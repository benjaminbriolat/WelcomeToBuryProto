using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Sc_formulaItem : MonoBehaviour
{
    public _So_item _item = null;
    _Sc_tooltipTrigger _sc_tolltipTrigger = null;

    private void Start()
    {
        _sc_tolltipTrigger = transform.GetComponentInChildren<_Sc_tooltipTrigger>();
    }

    public void SetItem(_So_item newItem)
    {
        _item = newItem;
        transform.GetComponent<Image>().sprite = _item.image;
        SetTool();
    }

    public void SetTool()
    {
        if (_sc_tolltipTrigger != null)
        {
            if (_item != null)
            {
                _sc_tolltipTrigger.header = _item.itemName.ToString();
            }
        }
    }
}
