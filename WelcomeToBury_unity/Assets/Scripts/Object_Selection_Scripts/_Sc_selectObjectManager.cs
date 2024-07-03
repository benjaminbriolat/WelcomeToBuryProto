using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class _Sc_selectObjectManager : MonoBehaviour
{
    public static _Sc_selectObjectManager instance;
    private Camera cam;

    [SerializeField] LayerMask myLayerMask;

    [SerializeField] string SelectableObjectLayer = "SelectableObject";

    [SerializeField] Transform HightlightedObjectTransform = null;
    [SerializeField] Transform SelectedObjectTransform = null;

    ISelectable _ISelectable = null;

    public float RessourceMinDistance = 1.0f;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        cam = Camera.main;
    }

    //Inputs
    public void getMousePos(Vector2 _mousePos)
    {
        Ray ray = cam.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, myLayerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer(SelectableObjectLayer))
            {
                if (_Sc_mouseHoverManager.instance.CurrentObject == null)
                {
                    OnObjectHighlight(hit.transform);
                }
            }
            else
            {
                OnObjectUnHighlight();
            }
        }
    }
    public void OnCLick()
    {
        if (HightlightedObjectTransform != null)
        {
            OnObjectSelect();
        }
        else
        {
            OnObjectDeselect();
        }
    }
    //Inputs END

    public void OnObjectHighlight(Transform hit)
    {
        HightlightedObjectTransform = hit.transform;
        _ISelectable = hit.transform.GetComponent<ISelectable>();

        if (_ISelectable != null)
        {
            _ISelectable.OnHighlighted();
        }
    }

    public void OnObjectUnHighlight()
    {
        if (_ISelectable != null)
        {
            _ISelectable.OnUnHighlighted();
            _ISelectable = null;
        }
        if (HightlightedObjectTransform != null)
        {
            HightlightedObjectTransform = null;
        }
    }

    public void OnObjectSelect()
    {
        SelectedObjectTransform = HightlightedObjectTransform;
        if (_ISelectable != null)
        {
            _ISelectable.OnSelected();
        }
    }

    public void OnObjectDeselect()
    {
        if (_ISelectable != null)
        {
            _ISelectable.OnDeselect();
        }
        _ISelectable = null;
        SelectedObjectTransform = null;
    }
}
