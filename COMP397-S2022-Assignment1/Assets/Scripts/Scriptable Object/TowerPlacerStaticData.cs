using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TowerPlacerStaticData", menuName = "ScriptableObjects/TowerPlacerStaticData", order = 2)]
public class TowerPlacerStaticData : ScriptableObject
{
    public List<TowerStaticData> towerStaticDataList;
}
