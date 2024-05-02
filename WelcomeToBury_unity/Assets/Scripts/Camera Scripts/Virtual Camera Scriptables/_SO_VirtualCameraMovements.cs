using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/VirtualCameraScriptable", order = 1)]
public class _SO_VirtualCameraMovements : ScriptableObject
{

    public bool followActive = true;

    public bool zoomActive = true;
    public float zoomValueOrigin = 50;
    public float zoomValueDestination = 10;

    public bool inclineActive = true;
    public float inclinaisonValueOrigin = 50;
    public float inclinaisonValueDestination = 10;

    public float defaultAngleX = 0;
    public float defaultAngleY = 0;
    public float defaultAngleZ = 0;
}
