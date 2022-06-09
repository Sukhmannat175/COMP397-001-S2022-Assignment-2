/*Tower.cs
 *Created by: Han Bi 301176547
 *base abstract class for all towers
 *Last update: June 8, 2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The time tower will wait before firing again")]
    protected float actionDelay;

    protected abstract void TowerBehaviour();

    private void Update()
    {
        TowerBehaviour();
    }

    public virtual void AddToTargets(GameObject gameObject) { }

    public virtual void RemoveFromTargets(GameObject gameObject) { }



 


}



