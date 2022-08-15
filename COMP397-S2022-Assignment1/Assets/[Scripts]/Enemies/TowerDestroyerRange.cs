/*  Filename:           TowerDestroyerRange.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 18, 2022
 *  Description:        Detect the attack range.
 *  Revision History:   June 18, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class TowerDestroyerRange : MonoBehaviour
{
    [SerializeField] private LayerMask towerMask;
    [SerializeField] private float radius;

    [Header("Debug")]
    [SerializeField] private TowerDestroyerController towerDestroyer;

    private void Start()
    {
        towerDestroyer = GetComponentInParent<TowerDestroyerController>();
    }

    void FixedUpdate()
    {
        int maxColliders = 10;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, hitColliders, towerMask);

        if (numColliders > 0)
        {
            towerDestroyer.SetTargetTower(hitColliders[0].GetComponent<Tower>());
        }
        else
        {
            towerDestroyer.SetTargetTower(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
