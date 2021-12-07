using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData
{
    public string title;
    public string description;
    public Sprite image;
    public CardEffectData effect;

    public CardData(string title, string description, Sprite image, CardEffectData effect)
    {
        this.title = title;
        this.description = description;
        this.image = image;
        this.effect = effect;
    }

    public CardData()
    {
        title = "Empty title";
        description = "Empty description";
        image = null;
        effect = null;
    }

    //From JSON
    public CardData(string JSON)
    {
        CopyValuesFrom(JsonUtility.FromJson<CardData>(JSON));
    }

    //From indexes
    public CardData(CardIndexes ind, CardGenerationDataAsset genData)
    {
        title = genData.cardTitles[ind.titleIndex];
        description = genData.cardDescriptions[ind.descriptionIndex];
        image = genData.cardImages[ind.imageIndex];
        effect = genData.cardEffects[ind.effectIndex].data;
    }

    //To JSON
    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }

    //To indexes
    public CardIndexes GetMatchingIndexes(CardGenerationDataAsset genData)
    {
        CardIndexes indexes = new CardIndexes();

        for (int i = 0; i < genData.cardTitles.Length; i++)
            if (title == genData.cardTitles[i]) { indexes.titleIndex = i; break; }

        for (int i = 0; i < genData.cardTitles.Length; i++)
            if (description == genData.cardDescriptions[i]) { indexes.descriptionIndex = i; break; }

        for (int i = 0; i < genData.cardTitles.Length; i++)
            if (image == genData.cardImages[i]) { indexes.imageIndex = i; break; }

        for (int i = 0; i < genData.cardTitles.Length; i++)
            if (effect == genData.cardEffects[i].data) { indexes.effectIndex = i; break; }

        return indexes;
    }

    public void CopyValuesFrom(CardData cd)
    {
        title = cd.title;
        description = cd.description;
        image = cd.image;
        effect = cd.effect;
    }

    [System.Serializable]
    public class CardIndexes
    {
        public int titleIndex;
        public int descriptionIndex;
        public int imageIndex;
        public int effectIndex;
    }
}
