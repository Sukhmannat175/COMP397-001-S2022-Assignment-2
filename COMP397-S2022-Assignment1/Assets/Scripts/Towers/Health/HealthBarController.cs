using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{   /*  Filename:           Tower.cs
 *  Author:             Han Bi (301176547)
 *  Last Update:        June 26, 2022
 *  Description:        Managing all Healthbars
 *  Revision History:   June 26, 2022 (Han Bi): Initial script.
 *                      
 */


    [SerializeField]
    private HealthBar healthBarPrefab;

    [SerializeField] Transform healthbarsParent;

    public Dictionary<Health, HealthBar> healthBars = new Dictionary<Health, HealthBar>();

    private void Awake()
    {

        //registers the methods which will be called for the static actions provided

        Health.OnHealthAdded += AddHealthBar;
        Health.OnHealthRemoved += RemoveHealthBar;
    }


    private void AddHealthBar(Health health)
    {
        
        if (!healthBars.ContainsKey(health))
        {
            var healthBar = Instantiate(healthBarPrefab, transform);
            healthBar.transform.SetParent(healthbarsParent);
            healthBars.Add(health, healthBar);
            healthBar.SetHealth(health);
        }
    }

    private void RemoveHealthBar(Health health)
    {
        if (healthBars.ContainsKey(health))
        {
            if (healthBars[health] != null)
            {
                Destroy(healthBars[health].gameObject);
                healthBars.Remove(health);
            }
            
        }
    }

    private void OnDestroy()
    {
        Health.OnHealthAdded -= AddHealthBar;
        Health.OnHealthRemoved -= RemoveHealthBar;
    }

}
