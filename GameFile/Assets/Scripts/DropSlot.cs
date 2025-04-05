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
        matchChecker = FindObjectOfType<MatchChecker>(); // DropSlot kodu prefab objesi �zerinde oldu�u i�in bu y�ntemi kulland�m.
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) // slotun bo� olup olmad���n� kontrol ediyoruz.
        {
            GameObject dropped = eventData.pointerDrag; // s�r�kledi�imiz blo�u bir de�i�kene at�yoruz.
            dropped.transform.SetParent(transform); // bu de�i�keni kullanaraki, s�r�kledi�imiz blo�u b�rakt���m�z slotun bir objesi, �ocu�u haline getiriyoruz.
            dropped.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // b�rak�lan blo�un slotun tam ortas�nda yer almas�n� sa�l�yoruz.
            dropped.GetComponent<DraggableBlock>().PlaceBlock(); // b�rak�lan blo�un tekrar hareket ettirilememesini sa�l�yoruz.
            if (matchChecker != null) // hata almamak i�in kontrol ediyoruz.
            {
                matchChecker.CheckMatches();
            }
        }
    }
}
