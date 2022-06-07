// ShopItemDisplay.cs
// Yuk Yee Wong - 301234795
// 06/06/2022
// Shop Item Display
// Initial Script

using UnityEngine;
using UnityEngine.UI;

public class ShopItemDisplay : MonoBehaviour
{
    [SerializeField] private Text nameLabel;
    [SerializeField] private Text descriptionLabel;
    [SerializeField] private Text goldLabel;
    [SerializeField] private Text stoneLabel;
    [SerializeField] private Text woodLabel;
    [SerializeField] private ShopItemButton defaultDisplayButton;

    private void OnEnable()
    {
        defaultDisplayButton.UpdateDisplay();
    }

    public void UpdateDisplay(string name, string description, int gold, int stone, int wood)
    {
        nameLabel.text = name;
        descriptionLabel.text = description;
        goldLabel.text = gold.ToString();
        stoneLabel.text = stone.ToString();
        woodLabel.text = wood.ToString();
    }
}
