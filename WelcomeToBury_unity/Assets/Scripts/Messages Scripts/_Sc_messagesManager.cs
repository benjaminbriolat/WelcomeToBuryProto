using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class _Sc_messagesManager : MonoBehaviour
{
    public static _Sc_messagesManager instance;
    [SerializeField] GameObject messagePrefab = null;
    [SerializeField] GameObject messagePrefab2 = null;
    [SerializeField] float messageTime = 5.0f;

    private void Awake()
    {
        instance = this;
    }

    public void SetMessageText(string _message, bool _remede = false)
    {
        GameObject _messageClone;
        if (_remede == false)
        {
            _messageClone = Instantiate(messagePrefab);
        }
        else
        {
            _messageClone = Instantiate(messagePrefab2);
        }

        TextMeshProUGUI _messageText = _messageClone.GetComponentInChildren<TextMeshProUGUI>();
        _Sc_messageHolder _sc_messageHolder = _messageClone.GetComponent<_Sc_messageHolder>();

        if(_messageText != null)
        {
            _messageText.text = _message;
        }

        if (_remede == false)
        {
            _messageClone.transform.SetParent(transform.GetChild(0));
        }
        else
        {
            _messageClone.transform.SetParent(transform.GetChild(1));
        }

        _sc_messageHolder.MessageCreated(messageTime);

        //_sc_messageHolder.setBoxColor(_remede);
    }
}
