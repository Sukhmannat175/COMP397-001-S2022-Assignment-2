/*  Filename:           TowerPoolManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Description:        For creating tower object pooling
 *  Revision History:   Auguest 1, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class TowerPoolManager : PoolManager<Tower>
{
    public Tower GetPooledTower(Vector3 spawnPosition, Quaternion spawnRotation, TowerStaticData towerStaticData)
    {
        var Tower = GetPooledObject(spawnPosition, false); // avoid update when waypoint is null
        Tower.transform.rotation = spawnRotation;
        Tower.Intialize(towerStaticData);
        Tower.gameObject.SetActive(true);
        return Tower;
    }

    public void ReturnPooledTower(Tower returnedTower)
    {
        ReturnPooledObject(returnedTower.gameObject);
    }
}
