using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _Sc_DialogueManager : MonoBehaviour
{
    public static _Sc_DialogueManager instance;
    [SerializeField] TextMeshProUGUI nameText = null;
    [SerializeField] TextMeshProUGUI dialogueText = null;

    [SerializeField] So_Dialogue currentDialogue = null;
    [SerializeField] int currentDialogueLineIndex = 0;

    CanvasGroup myCanvasGroup = null;
    _Sc_cerveau _sc_Cerveau = null;
    _Sc_pnjDialogue currentPnjDialogue = null;
    _Sc_pnjActions currentPnjActions = null;

    _Sc_Calendrier _sc_calendrier = null;
    _Sc_rewardItem _sc_rewardItem = null;

    bool isDialogueMalade = false;
    bool newDialogue = false;
    private void Awake()
    {
        instance = this;
        myCanvasGroup = GetComponent<CanvasGroup>();
        nameText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogueText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetCanvasGroup(false);
        _sc_Cerveau = _Sc_cerveau.instance;
        _sc_calendrier = _Sc_Calendrier.instance;
        _sc_rewardItem = _Sc_rewardItem.Instance;
    }
    public void GetDialogue(So_Dialogue _dialogue, int _dialogueIndex, _Sc_pnjDialogue _sc_pnjDialogue, bool _isDialogueMalade, _Sc_pnjActions _sc_pnjActions)
    {
        currentDialogue = _dialogue;
        _sc_pnjDialogue.currentDialogue = _dialogue;
        isDialogueMalade = _isDialogueMalade;
        currentPnjActions = _sc_pnjActions;
        newDialogue = true;

        currentDialogueLineIndex = _dialogueIndex;
        setDialogueLine(currentDialogueLineIndex);

        SetCanvasGroup(true);

        currentPnjDialogue = _sc_pnjDialogue;
    }

    private void setDialogueLine(int _dialogueIndex)
    {
        if(_dialogueIndex < currentDialogue.DialogueLines.Count)
        {
            if(currentDialogue.DialogueLines[_dialogueIndex].Speaker == 0) //PNJ parle
            {
                nameText.text = currentDialogue.DialogueLines[_dialogueIndex].FirstName + " " + currentDialogue.DialogueLines[_dialogueIndex].LastName;
            }
            else //Elsa parle
            {
                nameText.text = "Elsa";
            }
            
            dialogueText.text = currentDialogue.DialogueLines[_dialogueIndex].LineID;

            for (int i = 0; i < currentDialogue.DialogueLines[_dialogueIndex].DialogueRewards.ItemsList.Count; i++)
            {
                _sc_rewardItem.GiveReward(currentDialogue.DialogueLines[_dialogueIndex].DialogueRewards, 0);
            }

            if(newDialogue == true)
            {
                newDialogue = false;
                _Sc_cameraMovement.instance.ZoomPressed();
            }
        }
        else
        {
            currentPnjDialogue.lastLineDialogueIndex = 0;
            currentDialogueLineIndex = 0;
            QuitDialogue();

            if(isDialogueMalade == false)
            {
                _sc_calendrier.AdvanceCalendar();
            }
            else
            {
                currentPnjActions.SeSoucierAction();
            }
        }
    }

    public void NextDialogueLine()
    {
        currentDialogueLineIndex++;
        setDialogueLine(currentDialogueLineIndex);
        currentPnjDialogue.lastLineDialogueIndex = currentDialogueLineIndex;
    }

    public void QuitDialogue()
    {    
        SetCanvasGroup(false);
        _Sc_cameraMovement.instance.ZoomPressed();
    }

    private void SetCanvasGroup(bool _state)
    {
        if(_sc_Cerveau == null)
        {
            _sc_Cerveau = _Sc_cerveau.instance;
        }

        if (_state == true)
        {
            myCanvasGroup.alpha = 1.0f;
            myCanvasGroup.interactable = true;
            myCanvasGroup.blocksRaycasts = true;

            _sc_Cerveau.isInMenu = true;
            _sc_Cerveau.canMove = false;
        }
        else
        {
            myCanvasGroup.alpha = 0.0f;
            myCanvasGroup.interactable = false;
            myCanvasGroup.blocksRaycasts = false;

            _sc_Cerveau.isInMenu = false;
            _sc_Cerveau.canMove = true;
        }
    }
}
