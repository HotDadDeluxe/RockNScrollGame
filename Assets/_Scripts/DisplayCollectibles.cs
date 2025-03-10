using UnityEngine;
using TMPro;

public class DisplayCollectibles : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI notesText;

    void Start()
    {
        int coinsCount = PlayerCollectibles.Instance.GetCollectibleCount("coin");
        int notesCount = PlayerCollectibles.Instance.GetCollectibleCount("note");

        coinsText.text = "Coins: " + coinsCount;
        notesText.text = "Notes: " + notesCount;
    }
}