using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragWord : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler
{
    private bool isDragging;
    private RectTransform rectTransform;
   // private BoxCollider2D boxCollider;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
  //      boxCollider = gameObject.AddComponent<BoxCollider2D>();
  //      boxCollider.isTrigger = true;
 //       boxCollider.size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var canvas = GameObject.Find("Canvas");
        if (canvas == null)
            return;

        Debug.Log("DRAG");
        //transform.position = Input.mousePosition;
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform narrPanel = GameObject.Find("NarrationBg").transform as RectTransform;

        if(!RectTransformUtility.RectangleContainsScreenPoint(narrPanel, Input.mousePosition))
        {
            Debug.Log("INTERACT");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }

}
