using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class _Sc_messagesManager : MonoBehaviour
{
    public static _Sc_messagesManager instance;
    [SerializeField] GameObject messagePrefab = null;
    [SerializeField] float messageTime = 3.0f;

    private void Awake()
    {
        instance = this;
    }

    public void SetMessageText(string _message, bool _remede = false)
    {
        GameObject _messageClone = Instantiate(messagePrefab);

        TextMeshProUGUI _messageText = _messageClone.GetComponentInChildren<TextMeshProUGUI>();
        _Sc_messageHolder _sc_messageHolder = _messageClone.GetComponent<_Sc_messageHolder>();
        _messageText.text = _message;

        _messageClone.transform.SetParent(transform.GetChild(0));

        _sc_messageHolder.MessageCreated(messageTime);

        _sc_messageHolder.setBoxColor(_remede);
    }

    //Debug//
    [Button]
    public void CreateMessage()
    {
        SetMessageText("Remede 1 progress");
    }
}
