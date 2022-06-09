/*Enemy.cs
 * Created by: Han Bi 301176547
 * Abstract Enemy Class for all enemies
 * Currently just added mechanics to allow proper tower targeting
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;
    private float distanceTravelled;
    // Start is called before the first frame update
    void Start()
    {
        //For targeting to work, WE NEED TO SET SPEED TO WHATEVER THE ENEMY'S CURRENT SPEED IS
        speed = 3.5f;
    }

    // Update is called once per frame
    void Update()
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
