/*  Filename:           InventoryManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *                      Marcus Ngooi (301147411)
 *                      Ikamjot Hundal (301134374)
 *  Last Update:        June 25, 2022
 *  Description:        Inventory Manager.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Initial script including tower preview and purchase script.
 *                      June 25, 2022 (Marcus Ngooi): Added resource tower logic on lines 24-25.
 *                      June 26, 2022 (Ikamjot Hundal): Added Cannon Tower variables && logics on lins 30-31.  
 */

using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private bool freeToBuild = true;

    [SerializeField] private int initialGold;
    [SerializeField] private int initialStone;
    [SerializeField] private int initialWood;

    [SerializeField] private InventoryDisplay inventoryDisplay;

    /*[SerializeField] private GameObject crossbowTower;
    [SerializeField] GameObject crossbowTowerPreview;

    [SerializeField] private GameObject resourceTower;
    [SerializeField] GameObject resourceTowerPreview;

    [SerializeField] private GameObject cannonTower;
    [SerializeField] GameObject cannonTowerPreview;*/

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
        if (gold > 0 || stone > 0 || wood > 0)
        {
            SoundManager.instance.PlayCollectResourcesSfx();
        }

        goldOnHand += gold;
        stoneOnHand += stone;
        woodOnHand += wood;
        UpdateDisplay();
    }

    public void BuyTower(int gold, int stone, int wood)
    {
        if (!freeToBuild)
        {
            goldOnHand -= gold;
            stoneOnHand -= stone;
            woodOnHand -= wood;
            UpdateDisplay();
        }
    }

    public bool EnoughResources(int gold, int stone, int wood)
    {
        if(freeToBuild || (goldOnHand >= gold && stoneOnHand >= stone && woodOnHand >= wood))
        {
            return true;
        }
        else { return false; }
    }

    public void DecreaseResources(int gold, int stone, int wood)
    {
        if (!freeToBuild)
        {
            goldOnHand -= gold;
            stoneOnHand -= stone;
            woodOnHand -= wood;
            UpdateDisplay();
        }
    }

    public void UpdateDisplay()
    {
        inventoryDisplay.UpdateDisplay(goldOnHand, stoneOnHand, woodOnHand);
    }
}
