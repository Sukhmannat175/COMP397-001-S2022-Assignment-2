// ShopItemButton.cs
// Yuk Yee Wong - 301234795
// 06/06/2022
// Shop Item Button
// Initial Script

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItemButton : MonoBehaviour
{
    [SerializeField] private string towerName;
    [SerializeField] private string towerDescription;
    [SerializeField] private int goldNeeded;
    [SerializeField] private int stoneNeeded;
    [SerializeField] private int woodNeeded;
    [SerializeField] private ShopItemDisplay shopItemDisplay;
    [SerializeField] private InventoryManager inventoryDisplay;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        shopItemDisplay.UpdateDisplay(towerName, towerDescription, goldNeeded, stoneNeeded, woodNeeded);
        InventoryManager.instance.BuildTower(goldNeeded, stoneNeeded, woodNeeded);
    }

    public void UpdateDisplay()
    {
        shopItemDisplay.UpdateDisplay(towerName, towerDescription, goldNeeded, stoneNeeded, woodNeeded);
    }
}
