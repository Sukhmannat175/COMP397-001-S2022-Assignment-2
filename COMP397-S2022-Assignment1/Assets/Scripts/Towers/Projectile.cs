/*  Filename:           Projectile.cs
 *  Author:             Han Bi (301176547) *  
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        June 26, 2022
 *  Description:        Abstract class for different projectiles.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script.
 *                      June 26, 2022 (Yuk Yee Wong): Added damage float and SetDamage function.
 *                      August 1, 2022 (Yuk Yee Wong): Reorganised the code and adapted object pooling.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected int damage;
    protected GameObject target;
    protected abstract void OnHit();
    protected abstract void ReturnToPool();

    public virtual void SetDamage(int damage) { this.damage = damage; }

    public virtual void SetTarget(GameObject target) { this.target = target; }

}
