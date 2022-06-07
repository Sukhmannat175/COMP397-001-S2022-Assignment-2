// KeyBindingHelper.cs
// Yuk Yee Wong - 301234795
// 06/06/2022
// Key Binding Helper
// Initial Script

using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingHelper : MonoBehaviour
{
    [SerializeField] private KeyBindingManager.KeyAction keyAction;
    [SerializeField] private Text keyNameLabel;
    [SerializeField] private Text buttonLabel;

    [Header("Debug")]
    [SerializeField] private bool waitingForInput = false;
    [SerializeField] private string originalButtonText;

    private string waitingKeyPressText = "Press any key";
    private bool preserveAcronyms = true;

    private void OnEnable()
    {
        UpdateKeyLabel();
        originalButtonText = buttonLabel.text;
    }

    public void OnButtonPressed()
    {
        waitingForInput = true;
        buttonLabel.text = waitingKeyPressText;
    }

    public void UpdateKeyLabel()
    {
        if (KeyBindingManager.instance != null)
        {
            keyNameLabel.text = KeyNameWithSpace;
        }
    }

    string KeyNameWithSpace
    {
        get
        {
            string keyName = KeyBindingManager.instance.GetKey(keyAction).ToString();
            if (string.IsNullOrWhiteSpace(keyName))
                return string.Empty;

            StringBuilder newText = new StringBuilder(keyName.Length * 2);
            newText.Append(keyName[0]);

            for (int i = 1; i < keyName.Length; i++)
            {
                if (char.IsUpper(keyName[i]) || char.IsNumber(keyName[i]))
                {
                    if ((keyName[i - 1] != ' ' && !char.IsUpper(keyName[i - 1]))
                       || (preserveAcronyms && char.IsUpper(keyName[i - 1])
                       && i < keyName.Length - 1 
                       && !char.IsUpper(keyName[i + 1])))
                    {
                        newText.Append(' ');
                    }
                }
                newText.Append(keyName[i]);
            }
            return newText.ToString();
        }
    }

    private void OnGUI()
    {
        if (waitingForInput && Event.current.isKey && KeyBindingManager.instance != null)
        {
            KeyBindingManager.instance.Rebind(keyAction, Event.current.keyCode);
            buttonLabel.text = originalButtonText;
            UpdateKeyLabel();
            waitingForInput = false;
        }
    }
}
