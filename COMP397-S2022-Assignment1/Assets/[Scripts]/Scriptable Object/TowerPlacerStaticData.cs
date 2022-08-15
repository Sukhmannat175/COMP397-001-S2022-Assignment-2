/*  Filename:           TowerPlacerStaticData.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 26, 2022
 *  Description:        TowerPlacerData, for save functionality
 *  Revision History:   June 26, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TowerPlacerStaticData", menuName = "ScriptableObjects/TowerPlacerStaticData", order = 2)]
public class TowerPlacerStaticData : ScriptableObject
{
    public List<TowerStaticData> towerStaticDataList;
}
