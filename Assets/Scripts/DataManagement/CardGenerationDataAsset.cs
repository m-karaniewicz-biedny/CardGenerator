using UnityEngine;

[CreateAssetMenu(fileName = "Card Generation Data", menuName = "Card Data/Card Generation Data")]
public class CardGenerationDataAsset : ScriptableObject
{
    public string[] cardTitles;
    [TextArea]
    public string[] cardDescriptions;
    public Sprite[] cardImages;
    public CardEffectDataAsset[] cardEffects;
}
