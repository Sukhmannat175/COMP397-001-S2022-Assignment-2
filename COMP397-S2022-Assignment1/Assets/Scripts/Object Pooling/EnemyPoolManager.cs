/*  Filename:           EnemyPoolManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Description:        For creating enemy object pooling
 *  Revision History:   Auguest 1, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class EnemyPoolManager : PoolManager<Enemy>
{
    public Enemy GetPooledEnemy(Vector3 spawnPosition, Quaternion spawnRotation, Transform wayPointsContainer, EnemyStaticData enemyStaticData)
    {
        var newEnemy = GetPooledObject(spawnPosition, false); // avoid update when waypoint is null
        newEnemy.transform.rotation = spawnRotation;
        newEnemy.Intialize(enemyStaticData, wayPointsContainer);
        newEnemy.gameObject.SetActive(true);
        return newEnemy;
    }

    public void ReturnPooledEnemy(Enemy returnedProjectile)
    {
        ReturnPooledObject(returnedProjectile.gameObject);
    }
}
