using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchChecker : MonoBehaviour
{
    public Transform gridPanel, Spawnpoint;
    public TMP_Text ScoreText;
    public Image[] Blocks;
    public Canvas DeadCanvas;
    bool IsGridFull;
    int Score;

    private void Start()
    {
        Instantiate(Blocks[Random.Range(0, Blocks.Length)], Spawnpoint);
    }

    public void CheckMatches()
    {
        if (IsGridFull == false) // grid dolu mu diye kontrol ediyoruz. e�er bo� kutu varsa blok spawn etmeye devam ediyoruz.
        {
            for (int i = 0; i < gridPanel.childCount; i++) // panel �zerindeki her bir slotu kontrol ediyoruz.
            {
                Transform slot = gridPanel.GetChild(i); // kontrol etti�imiz her slotu bir de�i�kene at�yoruz.
                if (slot.childCount > 0) // atad���m�z de�i�kenin bir alt objesi var m� diye kontrol ediyoruz.
                {
                    GameObject block = slot.GetChild(0).gameObject; // e�er bir alt objesi varsa bu alt objeyi bir de�i�kene at�yoruz.
                    Color blockColor = block.GetComponent<Image>().color; // atad���m�z de�i�kenin rengini kontrol ediyoruz. c�mk� renk e�le�tirmesi yapaca��z.

                    CheckHorizontalMatch(slot, blockColor);
                    CheckVerticalMatch(slot, blockColor);

                    foreach (Transform slot1 in gridPanel) // gridpaneldeki slotlar�n i�inde dolan�yoruz. bu i�lemi burada yapma sebebimiz match durumunu kontrol ettikten sonra yapmam�z gerekti�i yoksa son blokta yap�lan match anlams�z kal�yor.
                    {
                        if (slot1.childCount == 0)
                        {
                            IsGridFull = false; // bo� kutu varsa de�erimizi false d�nd�r�yoruz.
                            break;
                        }
                        IsGridFull = true; // bo� kutu kalmad�ysa de�erimizi true d�nd�r�yoruz.
                    }
                }
            }
            Instantiate(Blocks[Random.Range(0, Blocks.Length)], Spawnpoint); // bir blok yerle�tirildikten sonra di�er blo�u olu�turuyoruz.
        }
        else //  bo� yer kalmad�ysa oyunu bitiriyoruz.
        {
            DeadCanvas.gameObject.SetActive(true);
        }
        
    }

    private void CheckHorizontalMatch(Transform slot, Color color) // yatay kontrol
    {
        int SiblingIndex = slot.GetSiblingIndex(); // yerle�tirdi�imiz objenin hangi indexte oldu�unu al�yoruz.
        int row = Mathf.FloorToInt(SiblingIndex / 4f); // ChechMatch'te kontrol etti�imiz slot objesinin hangi sat�rda oldu�unu buluyoruz.

        if (SiblingIndex + 1 < gridPanel.childCount && SiblingIndex + 2 < gridPanel.childCount) // e�le�me kontrol�
        {
            // slot objesininin bulundu�u indexi ve yan�ndaki indexleri farkl� de�i�kene at�yoruz.
            Transform slot1 = gridPanel.GetChild(SiblingIndex);
            Transform slot2 = gridPanel.GetChild(SiblingIndex + 1);
            Transform slot3 = gridPanel.GetChild(SiblingIndex + 2);

            // indexler s�ral� ama ayn� sat�rda de�ilse patlama olmamal� o y�zden atad���m�z indexlerin hangi sat�rda oldu�unu kontrol ediyoruz.
            int Slot1Row = Mathf.FloorToInt(SiblingIndex / 4f);
            int Slot2Row = Mathf.FloorToInt((SiblingIndex + 1) / 4f);
            int Slot3Row = Mathf.FloorToInt((SiblingIndex + 2) / 4f);

            if ((Slot1Row == Slot2Row) && (Slot2Row == Slot3Row))
            {
                // atad���m�z de�i�kenlerin alt objeleri var m� diye kontrol ediyoruz.
                if (slot1.childCount > 0 && slot2.childCount > 0 && slot3.childCount > 0)
                {
                    // e�er alt objeleri varsa bunlar�n renklerinin kontrol ediyoruz.
                    if (slot1.GetChild(0).GetComponent<Image>().color == color &&
                        slot2.GetChild(0).GetComponent<Image>().color == color &&
                        slot3.GetChild(0).GetComponent<Image>().color == color)
                    {
                        // renkler ayn�ysa bu objeleri yok ediyoruz.
                        DestroyBlock(slot1);
                        DestroyBlock(slot2);
                        DestroyBlock(slot3);
                        Score += 10;
                        ScoreText.text = Score.ToString();
                    }
                }
            }
            
        }
    }

    private void CheckVerticalMatch(Transform slot, Color color) // dikey kontrol
    {
        int SiblingIndex = slot.GetSiblingIndex();
        int column = slot.GetSiblingIndex() % 4; // kontrol etti�imiz slot objesinin hangi s�tunda oldu�unu buluyoruz.

        if (SiblingIndex + 4 < gridPanel.childCount && SiblingIndex + 8 < gridPanel.childCount) // e�le�me kontrol�
        {
            // slot objesininin bulundu�u indexi ve alt�ndaki indexleri farkl� de�i�kene at�yoruz.
            Transform slot1 = gridPanel.GetChild(SiblingIndex);
            Transform slot2 = gridPanel.GetChild(SiblingIndex + 4);
            Transform slot3 = gridPanel.GetChild(SiblingIndex + 8);

            if (slot1.childCount > 0 && slot2.childCount > 0 && slot3.childCount > 0)
            {
                if (slot1.GetChild(0).GetComponent<Image>().color == color &&
                    slot2.GetChild(0).GetComponent<Image>().color == color &&
                    slot3.GetChild(0).GetComponent<Image>().color == color)
                {
                    DestroyBlock(slot1);
                    DestroyBlock(slot2);
                    DestroyBlock(slot3);
                    Score += 10;
                    ScoreText.text = Score.ToString();
                }
            }

        }
    }

    private void DestroyBlock(Transform slot)
    {
        Destroy(slot.GetChild(0).gameObject);
        Debug.Log("destroy");
    }

}
