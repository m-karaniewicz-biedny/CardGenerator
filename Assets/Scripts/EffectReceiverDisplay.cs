using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectReceiverDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statsDisplay;

    public void UpdateStatsDisplay(EffectReceiverStats stats)
    {
        statsDisplay.text = $"Health: {stats.health}\nMana: {stats.mana}\nSpeed: {stats.speed}";
    }
}
