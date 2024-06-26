using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Items")]
public class _So_item : ScriptableObject
{
    public string itemName;
    public Sprite image;
    public ItemType itemType;
    public bool stackable = true;
    public ActionType actionType;
    public string formulaId = null;
    public int peremption = 3;
}

public enum ItemType
{
    ressource,
}
public enum ActionType
{
    none,
}
