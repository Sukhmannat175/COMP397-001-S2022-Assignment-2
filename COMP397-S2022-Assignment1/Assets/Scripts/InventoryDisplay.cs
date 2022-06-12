/*  Filename:           InventoryDisplay.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 6, 2022
 *  Description:        Inventory Display.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private Text goldLabel;
    [SerializeField] private Text stoneLabel;
    [SerializeField] private Text woodLabel;

    public void UpdateDisplay(int goldOnHand, int stoneOnHand, int woodOnHand)
    {
        goldLabel.text = goldOnHand.ToString();
        stoneLabel.text = stoneOnHand.ToString();
        woodLabel.text = woodOnHand.ToString();
    }
}
