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

    //Scripts Refs
    _Sc_movement _sc_movement = null;

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
    }

    private void getRefs()
    {
        if (inputManager == null)
        {
            inputManager = FindObjectOfType<InputManager>();
        }

        _sc_movement = _Sc_movement.instance;
    }
}
