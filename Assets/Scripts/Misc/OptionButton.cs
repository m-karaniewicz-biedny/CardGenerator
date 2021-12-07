using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] public Button button;

    private string _buttonName;
    private string ButtonName
    {
        get { return ButtonName; }
        set
        {
            _buttonName = value;
            nameText.text = _buttonName;
        }
    }

    public void Initialize(string name)
    {
        ButtonName = name;
    }
}