using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardDisplay))]
public class CardController : MonoBehaviour
{
    CardData _currentCard;
    CardDisplay _cardDisplay;

    private void Awake()
    {
        _cardDisplay = GetComponent<CardDisplay>();
        if (_cardDisplay == null) Debug.LogError("Missing card display");
        CurrentCard = new CardData();
    }

    public CardData CurrentCard
    {
        get
        {
            return _currentCard;
        }

        set
        {
            _currentCard = value; 
            OnCardModified();
        }
    }

    private void OnCardModified() => _cardDisplay.DisplayCard(_currentCard);

    public void ApplyEffectToReceiver(EffectReceiver receiver)
    {
        if (CurrentCard.effect == null) return;
        receiver.ApplyEffect(CurrentCard.effect);
    }
}
