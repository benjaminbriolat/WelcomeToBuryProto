using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_pnjSelectSprite : MonoBehaviour
{
    GameObject selectedImage = null;

    private void Awake()
    {
        selectedImage = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        UnSelected();
    }

    public void SetSelected()
    {
        selectedImage.SetActive(true);
    }
    public void UnSelected()
    {
        selectedImage.SetActive(false);
    }
}
