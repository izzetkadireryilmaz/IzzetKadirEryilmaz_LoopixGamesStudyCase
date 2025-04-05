using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    private MatchChecker matchChecker;

    private void Awake()
    {
        matchChecker = FindObjectOfType<MatchChecker>(); // DropSlot kodu prefab objesi üzerinde olduðu için bu yöntemi kullandým.
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) // slotun boþ olup olmadýðýný kontrol ediyoruz.
        {
            GameObject dropped = eventData.pointerDrag; // sürüklediðimiz bloðu bir deðiþkene atýyoruz.
            dropped.transform.SetParent(transform); // bu deðiþkeni kullanaraki, sürüklediðimiz bloðu býraktýðýmýz slotun bir objesi, çocuðu haline getiriyoruz.
            dropped.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // býrakýlan bloðun slotun tam ortasýnda yer almasýný saðlýyoruz.
            dropped.GetComponent<DraggableBlock>().PlaceBlock(); // býrakýlan bloðun tekrar hareket ettirilememesini saðlýyoruz.
            if (matchChecker != null) // hata almamak için kontrol ediyoruz.
            {
                matchChecker.CheckMatches();
            }
        }
    }
}
