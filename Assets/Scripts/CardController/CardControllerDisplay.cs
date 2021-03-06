using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardControllerDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleDisplay;
    [SerializeField] TextMeshProUGUI descriptionDisplay;
    [SerializeField] Image imageDisplay;

    public void DisplayCard(CardData card)
    {
        titleDisplay.text = card.title;
        descriptionDisplay.text = card.description;
        imageDisplay.sprite = card.image;
    }
}
