/*  Filename:           Arrow.cs
 *  Author:             Han Bi (301176547)
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        August 1, 2022
 *  Description:        Use for arrow tower projectiles.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script.
 *                      June 26, 2022 (Yuk Yee Wong): Removed the damage float to Projectile.
 *                      August 1, 2022 (Yuk Yee Wong): Reorganised the code and adapted object pooling.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : ProjectileBehaviour
{
    protected override void OnHit()
    {
        ReturnToPool();
    }

    protected override void ReturnToPool()
    {
        // Object pooling
        if (ProjectileFactory.Instance != null)
            ProjectileFactory.Instance.ReturnPooledArrow(this);
        else
            Destroy(gameObject);
    }
}
