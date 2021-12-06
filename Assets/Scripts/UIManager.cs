using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Button generateButton;
    [SerializeField] Button applyEffectButton;
    [SerializeField] Button saveButton;

    public static Action OnGenerateButtonClick;
    public static Action OnApplyButtonClick;
    public static Action OnSaveButtonClick;

    private void Awake()
    {
        generateButton.onClick.AddListener(GenerateButtonClick);
        applyEffectButton.onClick.AddListener(ApplyButtonClick);
        saveButton.onClick.AddListener(SaveButtonClick);
    }

    private void GenerateButtonClick() => OnGenerateButtonClick?.Invoke();
    private void ApplyButtonClick() => OnApplyButtonClick?.Invoke();
    private void SaveButtonClick() => OnSaveButtonClick?.Invoke();
}