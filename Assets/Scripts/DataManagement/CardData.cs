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
        this.title = "Empty title";
        this.description = "Empty description";
        this.image = null;
        this.effect = null;
    }

    public CardData(string JSON)
    {
        throw new System.NotImplementedException();
    }


    public string ToJSON()
    {
        throw new System.NotImplementedException();
    }
}
