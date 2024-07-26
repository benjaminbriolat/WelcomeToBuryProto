using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Dialogue")]
public class So_Dialogue : ScriptableObject
{
    [SerializeField] public List<DialogueLine> DialogueLines = new List<DialogueLine>();
}

[System.Serializable]
public class DialogueLine
{
    public string FirstName = null;
    public string LastName = null;
    public int Speaker = 0;

    public string LineID = null;
    public DialogueReward DialogueRewards = null;
}

[System.Serializable]
public class DialogueReward
{
    public List<_So_item> ItemsList;
    public List<int> CountsList;
}
