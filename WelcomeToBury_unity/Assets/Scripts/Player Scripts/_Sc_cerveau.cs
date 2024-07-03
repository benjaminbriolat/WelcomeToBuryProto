using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Rewired;
using UnityEngine.InputSystem.UI;

public class _Sc_cerveau : MonoBehaviour
{
    public static _Sc_cerveau instance;
    private InputManager inputManager = null;

    //Inputs Variables
    bool LeftClick = false;
    bool leftClickRelease = false;
    [SerializeField] bool leftClickReleased = true;
    bool FkeyPress = false;
    bool GkeyPress = false;
    bool leftArrowPress = false;
    bool rightArrowPress = false;
    bool downArrowPress = false;

    //Scripts Refs
    _Sc_movement _sc_movement = null;
    _Sc_cameraMovement _sc_cameraMovement = null;
    _Sc_selectPnj _sc_selectPnj = null;
    _Sc_selectObjectManager _sc_selectObjectManager = null;

    public bool isInMenu = false;
    public bool canMove = true;
    public Vector3 mousePos;
    public Ray mouseRay;
    EventSystem eventSystem = null;
    private Rewired.Player player
    {
        get
        {
            return ReInput.players.GetPlayer(0);
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        getRefs();
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
    }

    private void Update()
    {
        if (ReInput.isReady == false)
        {
            return;
        }    
        if(player == null)
        {
            return;
        }

        LeftClick = player.GetButton("LeftClick");
        leftClickRelease = player.GetButtonUp("LeftClick");

        if (player.GetButtonUp("LeftClick"))
        {
            leftClickReleased = true;
        }

        FkeyPress = player.GetButtonDown("F");
        GkeyPress = player.GetButtonDown("G");
        leftArrowPress = player.GetButtonDown("LeftArrow");
        rightArrowPress = player.GetButtonDown("RightArrow");
        downArrowPress = player.GetButtonDown("DownArrow");

        mousePos = ReInput.controllers.Mouse.screenPosition;
        mouseRay = Camera.main.ScreenPointToRay(mousePos);
        //
        ProcessInputs();
    }

    private void ProcessInputs()
    {
        if(LeftClick == true)
        {
            leftClickReleased = false;
            if(canMove == true && isInMenu == false)
            {
                _sc_movement.getMouseLeftClick(mousePos);
            }
        }

        if (leftClickRelease == true)
        {
            _sc_selectPnj.getMouseLeftClick(mousePos);
            if (_sc_selectObjectManager != null)
            {
                _sc_selectObjectManager.OnCLick();
            }
        }

        if (leftClickReleased == true)
        {
            _sc_movement.canSetSpeed = true;
        }
        else
        {
            _sc_movement.canSetSpeed = false;
        }

        if(FkeyPress == true)
        {
            _sc_cameraMovement.ZoomPressed();
        }
        if(GkeyPress == true)
        {
            _sc_cameraMovement.InclinePressed();
        }
        if(leftArrowPress == true)
        {
            _sc_cameraMovement.LeftArrowPressed();
        }
        if(rightArrowPress == true)
        {
            _sc_cameraMovement.RightArrowPressed();
        }
        if(downArrowPress == true)
        {
            _sc_cameraMovement.DownArrowPressed();
        }

        if (_sc_selectObjectManager != null)
        {
            _sc_selectObjectManager.getMousePos(ReInput.controllers.Mouse.screenPosition);
        }
    }

    private void getRefs()
    {
        if (inputManager == null)
        {
            inputManager = FindObjectOfType<InputManager>();
        }

        _sc_movement = _Sc_movement.instance;
        _sc_cameraMovement = _Sc_cameraMovement.instance;
        _sc_selectPnj = _Sc_selectPnj.Instance;
        _sc_selectObjectManager = _Sc_selectObjectManager.instance;
    }
}
