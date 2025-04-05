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
        if (IsGridFull == false) // grid dolu mu diye kontrol ediyoruz. eðer boþ kutu varsa blok spawn etmeye devam ediyoruz.
        {
            for (int i = 0; i < gridPanel.childCount; i++) // panel üzerindeki her bir slotu kontrol ediyoruz.
            {
                Transform slot = gridPanel.GetChild(i); // kontrol ettiðimiz her slotu bir deðiþkene atýyoruz.
                if (slot.childCount > 0) // atadýðýmýz deðiþkenin bir alt objesi var mý diye kontrol ediyoruz.
                {
                    GameObject block = slot.GetChild(0).gameObject; // eðer bir alt objesi varsa bu alt objeyi bir deðiþkene atýyoruz.
                    Color blockColor = block.GetComponent<Image>().color; // atadýðýmýz deðiþkenin rengini kontrol ediyoruz. cümkü renk eþleþtirmesi yapacaðýz.

                    CheckHorizontalMatch(slot, blockColor);
                    CheckVerticalMatch(slot, blockColor);

                    foreach (Transform slot1 in gridPanel) // gridpaneldeki slotlarýn içinde dolanýyoruz. bu iþlemi burada yapma sebebimiz match durumunu kontrol ettikten sonra yapmamýz gerektiði yoksa son blokta yapýlan match anlamsýz kalýyor.
                    {
                        if (slot1.childCount == 0)
                        {
                            IsGridFull = false; // boþ kutu varsa deðerimizi false döndürüyoruz.
                            break;
                        }
                        IsGridFull = true; // boþ kutu kalmadýysa deðerimizi true döndürüyoruz.
                    }
                }
            }
            Instantiate(Blocks[Random.Range(0, Blocks.Length)], Spawnpoint); // bir blok yerleþtirildikten sonra diðer bloðu oluþturuyoruz.
        }
        else //  boþ yer kalmadýysa oyunu bitiriyoruz.
        {
            DeadCanvas.gameObject.SetActive(true);
        }
        
    }

    private void CheckHorizontalMatch(Transform slot, Color color) // yatay kontrol
    {
        int SiblingIndex = slot.GetSiblingIndex(); // yerleþtirdiðimiz objenin hangi indexte olduðunu alýyoruz.
        int row = Mathf.FloorToInt(SiblingIndex / 4f); // ChechMatch'te kontrol ettiðimiz slot objesinin hangi satýrda olduðunu buluyoruz.

        if (SiblingIndex + 1 < gridPanel.childCount && SiblingIndex + 2 < gridPanel.childCount) // eþleþme kontrolü
        {
            // slot objesininin bulunduðu indexi ve yanýndaki indexleri farklý deðiþkene atýyoruz.
            Transform slot1 = gridPanel.GetChild(SiblingIndex);
            Transform slot2 = gridPanel.GetChild(SiblingIndex + 1);
            Transform slot3 = gridPanel.GetChild(SiblingIndex + 2);

            // indexler sýralý ama ayný satýrda deðilse patlama olmamalý o yüzden atadýðýmýz indexlerin hangi satýrda olduðunu kontrol ediyoruz.
            int Slot1Row = Mathf.FloorToInt(SiblingIndex / 4f);
            int Slot2Row = Mathf.FloorToInt((SiblingIndex + 1) / 4f);
            int Slot3Row = Mathf.FloorToInt((SiblingIndex + 2) / 4f);

            if ((Slot1Row == Slot2Row) && (Slot2Row == Slot3Row))
            {
                // atadýðýmýz deðiþkenlerin alt objeleri var mý diye kontrol ediyoruz.
                if (slot1.childCount > 0 && slot2.childCount > 0 && slot3.childCount > 0)
                {
                    // eðer alt objeleri varsa bunlarýn renklerinin kontrol ediyoruz.
                    if (slot1.GetChild(0).GetComponent<Image>().color == color &&
                        slot2.GetChild(0).GetComponent<Image>().color == color &&
                        slot3.GetChild(0).GetComponent<Image>().color == color)
                    {
                        // renkler aynýysa bu objeleri yok ediyoruz.
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
        int column = slot.GetSiblingIndex() % 4; // kontrol ettiðimiz slot objesinin hangi sütunda olduðunu buluyoruz.

        if (SiblingIndex + 4 < gridPanel.childCount && SiblingIndex + 8 < gridPanel.childCount) // eþleþme kontrolü
        {
            // slot objesininin bulunduðu indexi ve altýndaki indexleri farklý deðiþkene atýyoruz.
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
