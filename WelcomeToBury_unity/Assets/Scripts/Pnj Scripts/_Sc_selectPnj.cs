using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_selectPnj : MonoBehaviour
{
    public static _Sc_selectPnj Instance;
    Camera cam = null;
    [SerializeField] string layerName = "Pnj";
    WaitForSeconds selectDelayWFS = new WaitForSeconds(0.1f);

    public _Sc_pnjState currentPnjState = null;
    public _Sc_pnjState lastPnjState = null;

    [SerializeField] _Sc_pnjSelectSprite lastSelectedSprite = null;
    _Sc_fichePatientUI _sc_fichePatientUI = null;
    private int LayerPnj;
    [SerializeField] float minDist = 5.0f;
    Transform lastHitTransform = null;
    bool pnjSetted = false;
    bool unselectDone = false;
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
        if(currentPnjState != null)
        {
            if (pnjSetted == false)
            {
                if (Vector3.Distance(_Sc_cerveau.instance.transform.position, currentPnjState.transform.position) < minDist)
                {
                    Debug.Log("SelectPnj3");

                    currentPnjState.SetButtonsState();

                    SetUnitState(lastHitTransform.parent);
                    SetSelectedSprite(lastHitTransform.parent);
                    SetActionsUi(lastHitTransform.parent);

                    pnjSetted = true;
                    unselectDone = false;
                }
                else
                {
                    _Sc_movement.instance.setClosestDestination(currentPnjState.transform);
                }
            }
            else if (unselectDone == false)
            {
                if (Vector3.Distance(_Sc_cerveau.instance.transform.position, currentPnjState.transform.position) > minDist)
                {
                    unselectDone = true;
                    lastPnjState = currentPnjState;
                    pnjSetted = false;
                    currentPnjState = null;
                    UnSelectPnj(false);
                }
            }
        }
    }
    public void getMouseLeftClick(Vector2 _mousePos)
    {
        Ray ray = cam.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask(layerName)))
        {
            lastHitTransform = hit.transform;
            if (lastHitTransform.gameObject.layer == LayerPnj)
            {
                Debug.Log("Hit PNJ " + lastHitTransform.name);
                currentPnjState = (lastHitTransform.parent.GetComponent<_Sc_pnjState>());

                if (currentPnjState.CurrentState == _Sc_pnjState.State.Default)
                {
                    Debug.Log("Currenstateok");
                    //_sc_pnjState.SetButtonsState();
                    if (lastPnjState != null)
                    {
                        lastPnjState.SetActionsUi(false);
                        lastPnjState.CurrentState = _Sc_pnjState.State.Default;
                    }
                    if (lastSelectedSprite != null)
                    {
                        lastSelectedSprite.UnSelected();
                    }
                    lastSelectedSprite = currentPnjState.transform.GetChild(1).GetChild(1).GetComponent<_Sc_pnjSelectSprite>();

                    /*SetUnitState(lastHitTransform.parent);
                    SetSelectedSprite(lastHitTransform.parent);
                    SetActionsUi(lastHitTransform.parent);*/
                }
            }
            else
            {
                //UnSelectPnj();
            }
        }      
    }

    public void UnSelectPnj(bool _fromUI = false)
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

        if(_fromUI == false)
        {
            if (_sc_fichePatientUI != null)
            {
                _sc_fichePatientUI.setCanvas(false);
            }
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

