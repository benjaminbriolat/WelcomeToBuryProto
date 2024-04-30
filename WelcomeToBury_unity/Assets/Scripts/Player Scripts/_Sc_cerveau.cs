using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class _Sc_cerveau : MonoBehaviour
{
    public static _Sc_cerveau instance;
    private InputManager inputManager = null;

    //Inputs Variables
    bool LeftClick = false;
    [SerializeField] bool leftClickReleased = true;
    bool FkeyPress = false;
    bool GkeyPress = false;
    bool leftArrowPress = false;
    bool rightArrowPress = false;
    bool downArrowPress = false;

    //Scripts Refs
    _Sc_movement _sc_movement = null;
    _Sc_cameraMovement _sc_cameraMovement = null;
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
    }

    private void Update()
    {
        if(ReInput.isReady == false)
        {
            return;
        }    
        if(player == null)
        {
            return;
        }

        LeftClick = player.GetButton("LeftClick");
        
        if(player.GetButtonUp("LeftClick"))
        {
            leftClickReleased = true;
        }

        FkeyPress = player.GetButtonDown("F");
        GkeyPress = player.GetButtonDown("G");
        leftArrowPress = player.GetButtonDown("LeftArrow");
        rightArrowPress = player.GetButtonDown("RightArrow");
        downArrowPress = player.GetButtonDown("DownArrow");
        //
        ProcessInputs();
    }

    private void ProcessInputs()
    {
        if(LeftClick == true)
        {
            leftClickReleased = false;
            _sc_movement.getMouseLeftClick(ReInput.controllers.Mouse.screenPosition);
        }

        if(leftClickReleased == true)
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
    }

    private void getRefs()
    {
        if (inputManager == null)
        {
            inputManager = FindObjectOfType<InputManager>();
        }

        _sc_movement = _Sc_movement.instance;
        _sc_cameraMovement = _Sc_cameraMovement.instance;
    }
}
