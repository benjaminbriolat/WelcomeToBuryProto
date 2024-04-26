using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_debugCharAnimations : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = transform.GetChild(0).GetChild(1).GetComponent<Animator>();
    }

    public void setMoving(float _value)
    {
        animator.SetFloat("Idle Walk Run", _value);
    }
}
