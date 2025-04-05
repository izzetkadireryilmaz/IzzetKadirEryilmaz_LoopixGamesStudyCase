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
            canvasGroup = gameObject.AddComponent<CanvasGroup>(); //blockRaycast kontrolü yapacaðýmýz için gerekli
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
            transform.SetParent(transform.root); // sürüklediðimiz bloðu en üst katmana taþýyoruz, böylelikle grid'in bloðun üstünde kalmasý gibi olasý hatalardan kaçýnmýþ olýuyoruz.
            canvasGroup.blocksRaycasts = false; // bloðu sürüklerken raycast engellemesini kapatýyoruz. bu iþlem slotlarýn sürüklediðimz bloðu görmesini ve üzerinde iþlem yapabilmesini saðlýyor. (DropSlot'taki OnDrop methodu gibi)
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
            rectTransform.anchoredPosition += eventData.delta / transform.root.GetComponent<Canvas>().scaleFactor; // bloðun sürükleme iþlemini yapýyoruz.
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
            canvasGroup.blocksRaycasts = true; // bloðu bir slot üzerine býrakmadýysak tekrar sürükleyebilmemiz lazým, bu yüzden raycast engelini açýyoruz.
        }
    }
    public void PlaceBlock()
    {
        isPlaced = true;
    }
}
