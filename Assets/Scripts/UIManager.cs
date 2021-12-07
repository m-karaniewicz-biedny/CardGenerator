using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] OptionButton loadOptionButtonPrefab;
    [Header("References")]
    [SerializeField] Button generateButton;
    [SerializeField] Button applyEffectButton;
    [SerializeField] Button saveButton;
    [SerializeField] Button loadButton;
    [SerializeField] Button cancelLoadButton;
    [SerializeField] Button deleteAllButton;
    [SerializeField] GameObject loadParent;
    [SerializeField] GameObject loadOptionsPanel;

    public static Action OnGenerateButtonClick;
    public static Action OnApplyButtonClick;
    public static Action OnSaveButtonClick;
    public static Action OnLoadButtonClick;
    public static Action<int> OnFileToLoadClick;
    public static Action OnDeleteAllButtonClick;

    private void Awake()
    {
        generateButton.onClick.AddListener(GenerateButtonClick);
        applyEffectButton.onClick.AddListener(ApplyButtonClick);
        saveButton.onClick.AddListener(SaveButtonClick);
        loadButton.onClick.AddListener(LoadButtonClick);
        cancelLoadButton.onClick.AddListener(CancelLoadButtonClick);
        deleteAllButton.onClick.AddListener(DeleteAllButtonClick);
    }

    private void Start()
    {
        loadParent.SetActive(false);
    }

    private void OnEnable()
    {
        CardManager.RequestLoadOption += CreateLoadOption;
    }

    private void GenerateButtonClick() => OnGenerateButtonClick?.Invoke();
    private void ApplyButtonClick() => OnApplyButtonClick?.Invoke();
    private void SaveButtonClick() => OnSaveButtonClick?.Invoke();
    private void CancelLoadButtonClick() => loadParent.SetActive(false);
    private void LoadButtonClick()
    {
        //Clear load buttons TODO: pooling
        for (int i = 0; i < loadOptionsPanel.transform.childCount; i++)
            Destroy(loadOptionsPanel.transform.GetChild(i).gameObject);
        loadParent.SetActive(true);
        OnLoadButtonClick?.Invoke();
    }
    private void DeleteAllButtonClick()
    {
        OnDeleteAllButtonClick?.Invoke();
        loadParent.SetActive(false);
    }

    //Create a load button and subscribe a file load event with specific ID to onClick
    private void CreateLoadOption(int fileID, string filename)
    {
        OptionButton opt = Instantiate(loadOptionButtonPrefab.gameObject).GetComponent<OptionButton>();
        opt.transform.SetParent(loadOptionsPanel.transform, false);
        opt.Initialize(filename);

        //Invoke load event with correct ID when onClick
        opt.button.onClick.AddListener(() => 
        {
            OnFileToLoadClick?.Invoke(fileID);
            loadParent.SetActive(false);
        });
    }
}