/*  Filename:           ShopItemButton.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 6, 2022
 *  Description:        Shop Item Button.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItemButton : MonoBehaviour
{
    [SerializeField] private Tower.TowerType towerName;
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
        if (TutorialController.instance.state == TutorialController.TutorialState.TUTORIAL)
        {
            TutorialController.instance.PlayTowerSelectTutorial();
        }

        shopItemDisplay.UpdateDisplay(towerName, towerDescription, goldNeeded, stoneNeeded, woodNeeded);
        FindObjectOfType<TowerPlacer>().PreviewTower(towerName, goldNeeded, stoneNeeded, woodNeeded);
    }

    public void UpdateDisplay()
    {
        shopItemDisplay.UpdateDisplay(towerName, towerDescription, goldNeeded, stoneNeeded, woodNeeded);
    }

    public int GetGoldNeeded()
    {
        return goldNeeded;
    }

    public int GetWoodNeeded()
    {
        return woodNeeded;
    }

    public int GetStoneNeeded()
    {
        return stoneNeeded;
    }



}
