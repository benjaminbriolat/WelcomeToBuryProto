using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_selectableObject : MonoBehaviour, ISelectable
{
    [SerializeField] Transform mesh = null;
    _Sc_movement _sc_movement = null;
    [SerializeField] bool hasBeenSelected = false;
    _Sc_cerveau _cerveau = null;
    _Sc_selectObjectManager _sc_SelectObjectManager = null;
    _Sc_itemLdo _sc_ItemLdo = null;
    private void Start()
    {
        _sc_movement = _Sc_movement.instance;
        _cerveau = _Sc_cerveau.instance;
        _sc_SelectObjectManager = _Sc_selectObjectManager.instance;
        _sc_ItemLdo = transform.root.GetComponentInChildren<_Sc_itemLdo>();
    }

    private void Update()
    {
        if(hasBeenSelected == true)
        {
            if(Vector3.Distance(_cerveau.transform.position, transform.position) <= _sc_SelectObjectManager.RessourceMinDistance)
            {
                //Check if InventoryFull
                _sc_ItemLdo.GetItem();
                hasBeenSelected = false;
            }
        }
    }
    public void OnHighlighted()
    {
        //Enable outline mesh?
    }

    public void OnUnHighlighted()
    {
        //Disable outline mesh?
    }

    public void OnSelected()
    {
        _sc_movement.setClosestDestination(transform);
        hasBeenSelected = true;
    }

    public void OnDeselect()
    {
        hasBeenSelected = false;
    }
}
