using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private bool isPlaced = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>(); //blockRaycast kontrol� yapaca��m�z i�in gerekli
        }
            
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlaced)
        {
            return;
        }
        else
        {
            originalParent = transform.parent;
            transform.SetParent(transform.root); // s�r�kledi�imiz blo�u en �st katmana ta��yoruz, b�ylelikle grid'in blo�un �st�nde kalmas� gibi olas� hatalardan ka��nm�� ol�uyoruz.
            canvasGroup.blocksRaycasts = false; // blo�u s�r�klerken raycast engellemesini kapat�yoruz. bu i�lem slotlar�n s�r�kledi�imz blo�u g�rmesini ve �zerinde i�lem yapabilmesini sa�l�yor. (DropSlot'taki OnDrop methodu gibi)
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isPlaced)
        {
            return;
        }
        else
        {
            rectTransform.anchoredPosition += eventData.delta / transform.root.GetComponent<Canvas>().scaleFactor; // blo�un s�r�kleme i�lemini yap�yoruz.
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPlaced)
        {
            return;
        }
        else
        {
            transform.SetParent(originalParent);
            canvasGroup.blocksRaycasts = true; // blo�u bir slot �zerine b�rakmad�ysak tekrar s�r�kleyebilmemiz laz�m, bu y�zden raycast engelini a��yoruz.
        }
    }
    public void PlaceBlock()
    {
        isPlaced = true;
    }
}
