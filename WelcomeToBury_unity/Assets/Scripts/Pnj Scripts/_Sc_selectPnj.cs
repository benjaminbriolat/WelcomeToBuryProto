using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_selectPnj : MonoBehaviour
{
    public static _Sc_selectPnj Instance;
    Camera cam = null;
    [SerializeField] string layerName = "Pnj";
    WaitForSeconds selectDelayWFS = new WaitForSeconds(0.1f);
    public _Sc_pnjState lastPnjState = null;

    [SerializeField] _Sc_pnjSelectSprite lastSelectedSprite = null;
    _Sc_fichePatientUI _sc_fichePatientUI = null;
    private int LayerPnj;
    [SerializeField] float deselectDistance = 5.0f;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
        LayerPnj = LayerMask.NameToLayer("Pnj");
        _sc_fichePatientUI = _Sc_fichePatientUI.Instance;
    }

    private void Update()
    {
        //A faire plus tard + ne pouvoir select un pnj que si assez proche?
        /*if(lastPnjState != null)
        {
            if(Vector3.Distance(_Sc_cerveau.instance.transform.position, lastPnjState.transform.position) > deselectDistance)
            {
                UnSelectPnj();
            }
        }*/
    }
    public void getMouseLeftClick(Vector2 _mousePos)
    {
        Ray ray = cam.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask(layerName)))
        {
            if (hit.transform.gameObject.layer == LayerPnj)
            {
                Debug.Log("Hit PNJ " + hit.transform.name);
                _Sc_pnjState _sc_pnjState = (hit.transform.parent.GetComponent<_Sc_pnjState>());

                if (_sc_pnjState.CurrentState == _Sc_pnjState.State.Default)
                {
                    if (lastPnjState != null)
                    {
                        lastPnjState.SetActionsUi(false);
                        lastPnjState.CurrentState = _Sc_pnjState.State.Default;
                    }
                    if (lastSelectedSprite != null)
                    {
                        lastSelectedSprite.UnSelected();
                    }
                    lastSelectedSprite = _sc_pnjState.transform.GetChild(1).GetChild(1).GetComponent<_Sc_pnjSelectSprite>();

                    SetUnitState(hit.transform.parent);
                    SetSelectedSprite(hit.transform.parent);
                    SetActionsUi(hit.transform.parent);
                }
            }
            else
            {
                UnSelectPnj();
            }
        }      
    }

    public void UnSelectPnj()
    {
        if (lastPnjState != null)
        {
            lastPnjState.SetActionsUi(false);
            lastPnjState.CurrentState = _Sc_pnjState.State.Default;
            Debug.Log("DESELECT PNJ");
        }
        if (lastSelectedSprite != null)
        {
            lastSelectedSprite.UnSelected();
        }
        lastPnjState = null;

        if(_sc_fichePatientUI != null)
        {
            _sc_fichePatientUI.setCanvas(false);
        }
    }
    private void SetUnitState(Transform _pnjTransform)
    {
        _pnjTransform.GetComponent<_Sc_pnjState>().CurrentState = _Sc_pnjState.State.Selected;
        lastPnjState = _pnjTransform.GetComponent<_Sc_pnjState>();

        SetFichePatient();
    }
    private void SetSelectedSprite(Transform _pnjTransform)
    {
        _pnjTransform.GetChild(1).GetChild(2).GetComponent<_Sc_pnjSelectSprite>().SetSelected();
        lastSelectedSprite = _pnjTransform.GetChild(1).GetChild(2).GetComponent<_Sc_pnjSelectSprite>();
    }

    public void SetActionsUi(Transform _pnjTransform)
    {
        _pnjTransform.GetComponent<_Sc_pnjState>().SetActionsUi(true);
    }
    public void SetFichePatient()
    {
        _sc_fichePatientUI.SetFicheValues(lastPnjState, lastPnjState._so_pnjInfos, lastPnjState.state, lastPnjState.GroupLevel,
            lastPnjState.symptome1, lastPnjState.symptome2, lastPnjState.symptome3, lastPnjState.symptome4);
    }
}

