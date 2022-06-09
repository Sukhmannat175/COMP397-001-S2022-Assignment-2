/*Projectile.cs
 *Created by: Han Bi 301176547
 *Abstract class for different projectiles
 *Last update: June 7, 2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected GameObject target;
    protected virtual void Shoot(GameObject target) { }

    public void SetTarget(GameObject gameObject)
    {
        target = gameObject;
    }
}
