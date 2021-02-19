using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class DragWord : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler
{
    private bool isDragging;
    private RectTransform rectTransform;
    // private BoxCollider2D boxCollider;
    private static string draggedWord;

    int layer;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        draggedWord = GetComponent<TextMeshProUGUI>().text;
  //      boxCollider = gameObject.AddComponent<BoxCollider2D>();
  //      boxCollider.isTrigger = true;
  //       boxCollider.size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var canvas = GameObject.Find("Canvas");
        if (canvas == null)
            return;

        //Debug.Log("DRAG: " + draggedWord);
        //transform.position = Input.mousePosition;
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform narrPanel = GameObject.Find("NarrationBg").transform as RectTransform;
        //Debug.Log(narrPanel.rect);

        if(RectTransformUtility.RectangleContainsScreenPoint(narrPanel, Input.mousePosition, Camera.main))
        {
            Debug.Log("INTERACT");
            Narration.InteractWithDraggedWord(draggedWord);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //transform.localPosition = Vector3.zero;
    }

}
