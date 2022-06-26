/*  Filename:           EnemyProjectile.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 18, 2022
 *  Description:        Enemy Projectile for Tower Destroyer.
 *  Revision History:   June 18, 2022 (Yuk Yee Wong): Initial script.
 */
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyProjectile : MonoBehaviour
{ 
    // Not using sound manager because attack audio should play simultaneously from different enemies instead of being superceded by new shot.
    [SerializeField] AudioSource audioSource;
    [SerializeField] private string targetTag;
    [SerializeField] private int damage;

    public void Init(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(other.tag))
        {
            if (other.CompareTag(targetTag))
            {
                
                Tower tower = other.GetComponent<Tower>(); 
                
                if (tower != null)
                {
                    tower.TakeDamage(damage);
                    audioSource.Play();

                }
            }
        }
    }
}
