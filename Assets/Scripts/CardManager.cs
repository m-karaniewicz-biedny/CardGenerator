using UnityEngine;
using System;


public class CardManager : MonoBehaviour
{
    [SerializeField] CardGenerationDataAsset cardGenerationData;

    [SerializeField] CardController targetCardController;
    [SerializeField] EffectReceiver targetEffectReceiver;

    private string[] loadableCardFilePaths;

    public static Action<int, string> RequestLoadOption;

    private void Start()
    {
        RerollCard();
    }

    private void OnEnable()
    {
        UIManager.OnGenerateButtonClick += RerollCard;
        UIManager.OnApplyButtonClick += ApplyEffect;
        UIManager.OnSaveButtonClick += SaveCard;
        UIManager.OnLoadButtonClick += StartLoadSelection;
        UIManager.OnFileToLoadClick += LoadCardFromIndex;
        UIManager.OnDeleteAllButtonClick += DeleteAll;
    }

    private void OnDisable()
    {
        UIManager.OnGenerateButtonClick -= RerollCard;
        UIManager.OnApplyButtonClick -= ApplyEffect;
        UIManager.OnSaveButtonClick -= SaveCard;
        UIManager.OnLoadButtonClick -= StartLoadSelection;
        UIManager.OnFileToLoadClick -= LoadCardFromIndex;
        UIManager.OnDeleteAllButtonClick -= DeleteAll;
    }

    //Select random entries from card generation data arrays
    private CardData GenerateNewCardData()
    {
        string title = cardGenerationData.cardTitles[UnityEngine.Random.Range(0, cardGenerationData.cardTitles.Length)];
        string desc = cardGenerationData.cardDescriptions[UnityEngine.Random.Range(0, cardGenerationData.cardDescriptions.Length)];
        Sprite image = cardGenerationData.cardImages[UnityEngine.Random.Range(0, cardGenerationData.cardImages.Length)];
        CardEffectData effect = cardGenerationData.cardEffects[UnityEngine.Random.Range(0, cardGenerationData.cardEffects.Length)].data;
        return new CardData(title, desc, image, effect);
    }

    //Apply target CardController's effect to target EffecReceiver and reroll card
    private void ApplyEffect()
    {
        if (targetCardController == null || targetEffectReceiver == null) return;
        targetCardController.ApplyEffectToReceiver(targetEffectReceiver);
        RerollCard();
    }

    //Set target CardController's CardData to a new generated card
    private void RerollCard()
    {
        if (targetCardController == null) return;
        targetCardController.CurrentCard = GenerateNewCardData();
    }

    private void SaveCard()
    {
        if (targetCardController == null) return;
        CardSaveLoad.SaveCardDataToFileWithIndexes(targetCardController.CurrentCard, cardGenerationData);
    }

    //Populate the loadable cards list and request an option button for each entry
    private void StartLoadSelection()
    {
        if (targetCardController == null) return;
        loadableCardFilePaths = CardSaveLoad.GetLoadableCardIndexesFilePaths();

        for (int i = 0; i < loadableCardFilePaths.Length; i++)
        {
            RequestLoadOption?.Invoke(i, System.IO.Path.GetFileNameWithoutExtension(loadableCardFilePaths[i]));
        }
    }

    //Load card from loadable paths array index
    private void LoadCardFromIndex(int index)
    {
        targetCardController.CurrentCard = CardSaveLoad.LoadCardDataFromPathWithIndexes(loadableCardFilePaths[index], cardGenerationData);
    }

    private void DeleteAll()
    {
        CardSaveLoad.DeleteAllCardIndexFiles();
    }

    //To implement target change, subscribe functions below to target-selecting events ex. Action<CardController>
    private void SetTargetCardController(CardController cc) => targetCardController = cc;
    private void SetTargetEffectReceiver(EffectReceiver er) => targetEffectReceiver = er;

}
