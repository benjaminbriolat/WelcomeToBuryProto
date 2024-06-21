using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Rewired;

[ExecuteInEditMode]
public class _Sc_tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (Application.isEditor)
        {
            int headerLenght = headerField.text.Length;
            int contentLenght = contentField.text.Length;

            layoutElement.enabled = Mathf.Max(headerField.preferredWidth, contentField.preferredWidth) >= layoutElement.preferredWidth;
        }
        if (Application.isPlaying)
        {
            Vector2 _mousePosition = _Sc_cerveau.instance.mousePos;

            float x = _mousePosition.x / Screen.width;
            float y = _mousePosition.y / Screen.height;
            if (x <= y && x <= 1 - y) //left
                rectTransform.pivot = new Vector2(-0.15f, y);
            else if (x >= y && x <= 1 - y) //bottom
                rectTransform.pivot = new Vector2(x, -0.1f);
            else if (x >= y && x >= 1 - y) //right
                rectTransform.pivot = new Vector2(1.1f, y);
            else if (x <= y && x >= 1 - y) //top
                rectTransform.pivot = new Vector2(x, 1.3f);

            transform.position = _mousePosition;
        }
    }

    public void SetText(string header, string content = "")
    {
        if (string.IsNullOrEmpty(content))
        {
            contentField.gameObject.SetActive(false);
            contentField.text = content;
        }
        else
        {
            contentField.gameObject.SetActive(true);
            contentField.text = content;
        }

        headerField.text = header;

        int headerLenght = headerField.text.Length;
        int contentLenght = contentField.text.Length;

        layoutElement.enabled = Mathf.Max(headerField.preferredWidth, contentField.preferredWidth) >= layoutElement.preferredWidth;
    }
}
