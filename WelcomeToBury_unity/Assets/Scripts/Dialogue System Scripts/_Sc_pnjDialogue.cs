using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_pnjDialogue : MonoBehaviour
{
    public So_Dialogue myDialogue;
    public So_Dialogue myDialogueMalade;
    public So_Dialogue myDialogueConfiance;
    

    public int lastLineDialogueIndex = 0;

    public So_Dialogue currentDialogue = null;

    public void ChangeDialogues(So_Dialogue _dialogue, So_Dialogue _dialogueMalade , So_Dialogue _dialogueConfiance)
    {
        myDialogue = _dialogue;
        myDialogueMalade = _dialogueMalade;
        myDialogueConfiance = _dialogueConfiance;
        

        lastLineDialogueIndex = 0;
    }
}
