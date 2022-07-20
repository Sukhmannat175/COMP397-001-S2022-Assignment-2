/*  Filename:           Projectile.cs
 *  Author:             Han Bi (301176547)
 *  Last Update:        June 26, 2022
 *  Description:        Abstract class for different projectiles.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script.
 *                      June 26, 2022 (Yuk Yee Wong): Added damage float and SetDamage function.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected int damage;
    protected GameObject target;
    protected virtual void Shoot(GameObject target) { }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetTarget(GameObject gameObject)
    {
        target = gameObject;
    }
}
