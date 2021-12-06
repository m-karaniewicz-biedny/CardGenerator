using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] CardGenerationDataAsset cardGenerationData;

    [SerializeField] CardController targetCardController;
    [SerializeField] EffectReceiver targetEffectReceiver;

    private void Start()
    {
        GenerateCardAndApplyToTargetController();
    }

    private void OnEnable()
    {
        UIManager.OnGenerateButtonClick += GenerateCardAndApplyToTargetController;
        UIManager.OnApplyButtonClick += ApplyTargetControllerEffectToTargetReceiver;
        UIManager.OnSaveButtonClick += SaveCardFromTargetController;
    }

    private void OnDisable()
    {
        UIManager.OnGenerateButtonClick -= GenerateCardAndApplyToTargetController;
        UIManager.OnApplyButtonClick -= ApplyTargetControllerEffectToTargetReceiver;
        UIManager.OnSaveButtonClick -= SaveCardFromTargetController;
    }

    private CardData GenerateNewCardData()
    {
        string title = cardGenerationData.cardTitles[Random.Range(0, cardGenerationData.cardTitles.Length)];
        string desc = cardGenerationData.cardDescriptions[Random.Range(0, cardGenerationData.cardDescriptions.Length)];
        Sprite image = cardGenerationData.cardImages[Random.Range(0, cardGenerationData.cardImages.Length)];
        CardEffectData effect = cardGenerationData.cardEffects[Random.Range(0, cardGenerationData.cardEffects.Length)].data;
        return new CardData(title,desc,image,effect);
    }

    private void ApplyCardDataToTargetController(CardData card)
    {
        targetCardController.CurrentCard = card;
    }

    public void ApplyTargetControllerEffectToTargetReceiver()
    {
        targetCardController.ApplyEffectToReceiver(targetEffectReceiver);
    }

    public void GenerateCardAndApplyToTargetController()
    {
        ApplyCardDataToTargetController(GenerateNewCardData());
    }

    public void SaveCardFromTargetController()
    {
        Debug.Log("Card save! (TODO)");
    }

}
