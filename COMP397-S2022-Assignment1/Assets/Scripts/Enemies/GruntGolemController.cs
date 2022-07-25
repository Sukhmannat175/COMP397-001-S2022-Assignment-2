/*  Filename:           GruntGolemController.cs
 *  Author:             Sukhmannat Singh (301168420)
 *  Last Update:        June 11, 2022
 *  Description:        Controls grunt golem.
 *  Revision History:   June 11, 2022 (Sukhmannat Singh): Initial script.
 *                      June 18, 2022 (Yuk Yee Wong): Most of the script is moved to the EnemyBaseBehaviour to allow for inheritance.
 *                      June 26, 2022 (Sukhmannat Singh): Added logic to add data to save file
 */

using UnityEngine;

public class GruntGolemController : EnemyBaseBehaviour
{
    [HideInInspector] public EnemyData enemyData;

    public override void EnemyStartBehaviour()
    {
        base.EnemyStartBehaviour();

        id = "GruntGolem" + Random.Range(0, int.MaxValue).ToString();

        if (string.IsNullOrEmpty(enemyData.enemyId))
        {
            enemyData.enemyId = id;
            enemyData.enemyType = EnemyType.GRUNTGOLEM;
            GameController.instance.current.enemies.Add(enemyData);
        }
    }
    public override void EnemyUpdateBehaviour()
    {
        base.EnemyUpdateBehaviour();
        Walk(wayPoints[path]);

        enemyData.health = healthDisplay.CurrentHealthValue;
        enemyData.enemyPosition = transform.position;
        enemyData.enemyRotation = transform.rotation;
    }
}
