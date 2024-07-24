using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_pnjDialogue : MonoBehaviour
{
    public So_Dialogue myDialogue;
    public So_Dialogue myDialogueConfiance;
    public So_Dialogue myDialogueMalade;

    public int lastLineDialogueIndex = 0;

    public So_Dialogue currentDialogue = null;

    public void ChangeDialogues(So_Dialogue _dialogue, So_Dialogue _dialogueConfiance, So_Dialogue _dialogueMalade)
    {
        myDialogue = _dialogue;
        myDialogueConfiance = _dialogueConfiance;
        myDialogueMalade = _dialogueMalade;

        lastLineDialogueIndex = 0;
    }
}
