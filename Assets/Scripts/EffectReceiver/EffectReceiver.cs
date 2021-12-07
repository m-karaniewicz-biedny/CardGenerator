using UnityEngine;

[RequireComponent(typeof(EffectReceiverDisplay))]
public class EffectReceiver : MonoBehaviour
{
    EffectReceiverStats stats;

    EffectReceiverDisplay _effectReceiverDisplay;

    private void Awake()
    {
        _effectReceiverDisplay = GetComponent<EffectReceiverDisplay>();
        if (_effectReceiverDisplay == null) Debug.LogError("Missing effect receiver display");
    }

    private void Start() => ResetStatsToDefaultValues();

    private void ResetStatsToDefaultValues()
    {
        stats = new EffectReceiverStats(30, 10, 5);
        _effectReceiverDisplay.UpdateStatsDisplay(stats);
    }

    public void ApplyEffect(CardEffectData effect)
    {
        stats = new EffectReceiverStats(
            stats.health + effect.healthModifier,
            stats.mana + effect.manaModifier,
            stats.speed + effect.speedModifier);
        _effectReceiverDisplay.UpdateStatsDisplay(stats);
    }
}

public struct EffectReceiverStats
{
    public int health;
    public int mana;
    public int speed;

    public EffectReceiverStats(int health, int mana, int speed)
    {
        this.health = health;
        this.mana = mana;
        this.speed = speed;
    }
}