using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_itemLdo : MonoBehaviour
{
    public _So_item _item;
    public int count = 1;
    _Sc_tooltipTrigger _sc_tolltipTrigger = null;
    private void Awake()
    {
        _sc_tolltipTrigger = transform.GetComponentInChildren<_Sc_tooltipTrigger>();
    }

    private void Start()
    {
        if(_sc_tolltipTrigger != null )
        {
            _sc_tolltipTrigger.header = _item.itemName.ToString() + "(" + count.ToString() + ")";
        }
    }
}
