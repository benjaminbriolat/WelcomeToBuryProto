using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class _Sc_messageHolder : MonoBehaviour
{
    Animator _messageAnimator = null;
    [SerializeField] Color baseColor = Color.white;
    [SerializeField] Color RemedeColor = Color.white;

    private void Awake()
    {
        _messageAnimator = GetComponent<Animator>();

        transform.GetChild(0).DOPunchPosition(new Vector3(50.0f, 0.0f, 0.0f), 0.25f, 10, 1, false);
        transform.GetChild(1).DOPunchPosition(new Vector3(50.0f, 0.0f, 0.0f), 0.25f, 10, 1, false);
    }
    public void MessageCreated(float _messageDuration)
    {
        StartCoroutine(DestroyMessage(_messageDuration));
    }

    public void setBoxColor(bool remede)
    {
        if(remede == false)
        {
            transform.GetChild(0).GetComponent<Image>().color = baseColor;
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().color = RemedeColor;
        }
    }

    private IEnumerator DestroyMessage(float _time)
    {
        yield return new WaitForSeconds(_time);
        _messageAnimator.SetBool("end", true);

        if(_messageAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && _messageAnimator.IsInTransition(0) == false)
        {
            Destroy(gameObject);
        }
    }
}
