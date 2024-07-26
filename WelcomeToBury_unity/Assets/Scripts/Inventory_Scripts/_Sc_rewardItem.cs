using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class _Sc_rewardItem : MonoBehaviour
{
    public static _Sc_rewardItem Instance;
    _Sc_inventoryManager _sc_inventoryManager = null;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _sc_inventoryManager = _Sc_inventoryManager.instance;
    }

    public void GiveReward(DialogueReward _dialogueReward, float _delay)
    {
        if(_delay == 0)
        {
            for (int i = 0; i < _dialogueReward.ItemsList.Count; i++)
            {
                _sc_inventoryManager.AddItem(_dialogueReward.ItemsList[i], _dialogueReward.CountsList[i]);
            }
        }
        else
        {
            StartCoroutine(delayReward(_dialogueReward, _delay));
        }
    }

    private IEnumerator delayReward(DialogueReward _dialogueReward, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        for (int i = 0; i < _dialogueReward.ItemsList.Count; i++)
        {
            _sc_inventoryManager.AddItem(_dialogueReward.ItemsList[i], _dialogueReward.CountsList[i]);
        }
    }
}
