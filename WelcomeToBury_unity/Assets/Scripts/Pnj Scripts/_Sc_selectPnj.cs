using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [SerializeField] float maxDist = 4.0f;

    Transform lastHitTransform = null;

    public bool pnjSetted = false;
    public bool unselectDone = false;

    [SerializeField] bool autoDialogue = true;

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
                    currentPnjState.SetButtonsState();

                    SetUnitState(lastHitTransform.parent);
                    SetSelectedSprite(lastHitTransform.parent);
                    SetActionsUi(lastHitTransform.parent);

                    pnjSetted = true;
                    unselectDone = false;

                    if(autoDialogue == true)
                    {
                        currentPnjState.GetComponent<_Sc_pnjActions>().PnjDialogue(false);
                    }
                }
                else
                {
                    if(_Sc_movement.instance.NewCustomDestination == false)
                    {
                        _Sc_movement.instance.setClosestDestination(currentPnjState.transform);
                    }
                }
            }
            else if (unselectDone == false)
            {
                if (Vector3.Distance(_Sc_cerveau.instance.transform.position, currentPnjState.transform.position) > maxDist)
                {                    
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
            if (_Sc_mouseHoverManager.instance.CurrentObject == null)
            {
                lastHitTransform = hit.transform;
                if (lastHitTransform.gameObject.layer == LayerPnj)
                {
                    if(lastHitTransform.parent.GetComponent<_Sc_pnjState>().cracheurDeNoir == false)
                    {
                        UnSelectPnj(false);

                        Debug.Log("Hit PNJ " + lastHitTransform.name);
                        currentPnjState = (lastHitTransform.parent.GetComponent<_Sc_pnjState>());

                        if (currentPnjState.CurrentState == _Sc_pnjState.State.Default)
                        {
                            //_sc_pnjState.SetButtonsState();
                            if (lastPnjState != null)
                            {
                                lastPnjState.SetActionsUi(false);
                                lastPnjState.CurrentState = _Sc_pnjState.State.Default;
                            }
                            if (lastSelectedSprite != null)
                            {
                                //lastSelectedSprite.UnSelected();
                            }
                            lastSelectedSprite = currentPnjState.transform.GetChild(1).GetChild(1).GetComponent<_Sc_pnjSelectSprite>();

                            _Sc_movement.instance.NewCustomDestination = false;
                            /*SetUnitState(lastHitTransform.parent);
                            SetSelectedSprite(lastHitTransform.parent);
                            SetActionsUi(lastHitTransform.parent);*/
                        }
                    }
                }
                else
                {
                    //UnSelectPnj();
                }
            }
        }
    }

    public void UnSelectPnj(bool _fromUI = false)
    {
        unselectDone = true;
        lastPnjState = currentPnjState;
        pnjSetted = false;
        currentPnjState = null;

        if (lastPnjState != null)
        {
            lastPnjState.SetActionsUi(false);
            lastPnjState.CurrentState = _Sc_pnjState.State.Default;
        }
        if (lastSelectedSprite != null)
        {
            lastSelectedSprite.UnSelected();
        }

        lastPnjState = null;
        lastSelectedSprite = null;

        if (_fromUI == false)
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

        SetFichePatient(0, lastPnjState);
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
    public void SetFichePatient(float delay, _Sc_pnjState _pnjState)
    {
        if(delay>0)
        {
            StartCoroutine(delayFichePatient(delay, _pnjState));
            Debug.Log("delay");
        }
        else
        {
            Debug.Log("nodelay");
            _sc_fichePatientUI.SetFicheValues(lastPnjState, lastPnjState._so_pnjInfos, lastPnjState.state, lastPnjState.GroupLevel,
           lastPnjState.symptome1, lastPnjState.symptome2, lastPnjState.symptome3, lastPnjState.symptome4);
        }
    }

    private IEnumerator delayFichePatient(float delay, _Sc_pnjState _pnjState)
    {
        yield return new WaitForSeconds(delay);
        _sc_fichePatientUI.SetFicheValues(_pnjState, _pnjState._so_pnjInfos, _pnjState.state, _pnjState.GroupLevel,
           _pnjState.symptome1, _pnjState.symptome2, _pnjState.symptome3, _pnjState.symptome4);
    }
}

