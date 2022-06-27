/*  Filename:           GruntGolemController.cs
 *  Author:             Sukhmannat Singh (301168420)
 *  Last Update:        June 11, 2022
 *  Description:        Controls grunt golem.
 *  Revision History:   June 11, 2022 (Sukhmannat Singh): Initial script.
 *                      June 18, 2022 (Yuk Yee Wong): Most of the script is moved to the EnemyBaseBehaviour to allow for inheritance.
 */

using UnityEngine;

public class GruntGolemController : EnemyBaseBehaviour
{
    [HideInInspector] public EnemyData enemyData;

    public override void EnemyStartBehaviour()
    {
        base.EnemyStartBehaviour();

        if (string.IsNullOrEmpty(enemyData.enemyId))
        {
            enemyData.enemyId = "GruntGolem" + Random.Range(0, int.MaxValue).ToString();
            enemyData.enemyType = EnemyType.GRUNTGOLEM;
            GameController.instance.current.enemies.Add(enemyData);
        }
    }
    public override void EnemyUpdateBehaviour()
    {
        base.EnemyUpdateBehaviour();
        Walk(wayPoints[path]);

        enemyData.enemyPosition = transform.position;
        enemyData.enemyRotation = transform.rotation;
    }
}
