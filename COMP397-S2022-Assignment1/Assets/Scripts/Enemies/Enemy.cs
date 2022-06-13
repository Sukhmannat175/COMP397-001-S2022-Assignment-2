/*  Filename:           Enemy.cs
 *  Author:             Han Bi (301176547)
 *  Last Update:        June 7, 2022
 *  Description:        Abstract Enemy Class for all enemies.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script which currently has mechanics to allow proper tower targeting.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private float speed;
    [SerializeField] private float distanceTravelled;
    // Start is called before the first frame update
    void Start()
    {       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDistanceTravelled()
    {
        distanceTravelled += speed * Time.deltaTime;
    }

    public float GetDistanceTravelled()
    {
        return distanceTravelled;
    }

    protected void SetSpeed(float speed)
    {
        this.speed = speed;
    }

}
