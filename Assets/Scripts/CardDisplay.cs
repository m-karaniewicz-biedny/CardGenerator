using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
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
