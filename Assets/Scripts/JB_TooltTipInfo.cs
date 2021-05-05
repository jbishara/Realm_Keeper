using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JB_TooltTipInfo : MonoBehaviour
{
    [SerializeField] private RectTransform canvasRectTrasnform;

    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;

    public bool mouseOver;

    private void Awake()
    {
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        textMeshPro = transform.Find("tooltiptext").GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }



    private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8, 8);

        backgroundRectTransform.sizeDelta = textSize + paddingSize;
    }

    private void Update()
    {
        if (mouseOver)
        {
            rectTransform.anchoredPosition = Input.mousePosition / canvasRectTrasnform.localScale.x;
        }
        
    }
}
