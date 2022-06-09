// InventoryManager.cs
// Yuk Yee Wong - 301234795
// 06/06/2022
// Inventory Manager
// Initial Script
// Added tower preview + purchase script

using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private bool freeToBuild = true;

    [SerializeField] private int initialGold;
    [SerializeField] private int initialStone;
    [SerializeField] private int initialWood;

    [SerializeField] private InventoryDisplay inventoryDisplay;

    [SerializeField] private GameObject crossbowTower;
    [SerializeField] GameObject crossbowTowerPreview;


    [Header("Debug")]
    [SerializeField] private int goldOnHand;
    [SerializeField] private int stoneOnHand;
    [SerializeField] private int woodOnHand;

    public static InventoryManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        ResetInitialResources();
        UpdateDisplay();
    }


    private void ResetInitialResources()
    {
        goldOnHand = initialGold;
        stoneOnHand = initialStone;
        woodOnHand = initialWood;
    }

    public void CollectResources(int gold, int stone, int wood)
    {
        goldOnHand += gold;
        stoneOnHand += stone;
        woodOnHand += wood;
        UpdateDisplay();
    }

    public void BuyTower(int gold, int stone, int wood)
    {
        if (freeToBuild || (goldOnHand >= gold && stoneOnHand >= stone && woodOnHand >= wood))
        {

            if (!freeToBuild)
            {
                goldOnHand -= gold;
                stoneOnHand -= stone;
                woodOnHand -= wood;
                UpdateDisplay();
            }

        }
    }


    public void UpdateDisplay()
    {
        inventoryDisplay.UpdateDisplay(goldOnHand, stoneOnHand, woodOnHand);
    }
}
