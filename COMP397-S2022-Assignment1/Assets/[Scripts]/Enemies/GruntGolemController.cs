/*  Filename:           GruntGolemController.cs
 *  Author:             Sukhmannat Singh (301168420)
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        August 1, 2022
 *  Description:        Controls grunt golem.
 *  Revision History:   June 11, 2022 (Sukhmannat Singh): Initial script.
 *                      June 18, 2022 (Yuk Yee Wong): Most of the script is moved to the EnemyBaseBehaviour to allow for inheritance.
 *                      June 26, 2022 (Sukhmannat Singh): Added logic to add data to save file
 *                      August 1, 2022 (Yuk Yee Wong): Reorganised the code and adapted object pooling.
 */

using UnityEngine;

public class GruntGolemController : EnemyBaseBehaviour
{
    protected override string idPrefix { get { return "GruntGolem"; } }

    protected override EnemyType enemyType { get { return EnemyType.GRUNTGOLEM; } }

    public override void EnemyStartBehaviour() { }

    public override void EnemyOnEnableBehaviour() { }

    public override void EnemyUpdateBehaviour()
    {
        base.EnemyUpdateBehaviour();
        Walk(wayPoints[path]);
    }

    protected override void ReturnToPool()
    {
        EnemyFactory.Instance.ReturnPooledGruntGolem(this);
    }
}
