using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_pnjActionsParent : MonoBehaviour
{
    public _Sc_debugPnjButton DialogueButton;
    public _Sc_debugPnjButton SoucierButton;
    public _Sc_debugPnjButton RemedeButton;
    public _Sc_debugPnjButton GesteSoinButton;

    private void Awake()
    {
        DialogueButton = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<_Sc_debugPnjButton>();
        SoucierButton = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<_Sc_debugPnjButton>();
        RemedeButton = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<_Sc_debugPnjButton>();
        GesteSoinButton = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<_Sc_debugPnjButton>();
    }
    public void GetButtonsState(bool _dialogue, bool _soucier, bool _remede, bool _gesteSoin, bool trusteReached)
    {
        DialogueButton.SetUsable(_dialogue);
        SoucierButton.SetUsable(_soucier);
        RemedeButton.SetUsable(_remede);

        if(_gesteSoin == true && trusteReached == true)
        {
            GesteSoinButton.SetUsable(true);
        }
        else
        {
            GesteSoinButton.SetUsable(false);
        }
    }
}
