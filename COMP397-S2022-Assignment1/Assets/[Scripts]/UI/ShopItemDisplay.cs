/*  Filename:           ShopItemDisplay.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 6, 2022
 *  Description:        Shop Item Display.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Initial script.
 */

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

    public void UpdateDisplay(Tower.TowerType name, string description, int gold, int stone, int wood)
    {
        nameLabel.text = name.ToString();
        descriptionLabel.text = description;
        goldLabel.text = gold.ToString();
        stoneLabel.text = stone.ToString();
        woodLabel.text = wood.ToString();
    }
}
