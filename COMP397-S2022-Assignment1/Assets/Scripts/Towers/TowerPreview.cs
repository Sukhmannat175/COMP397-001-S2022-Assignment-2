/*  Filename:           TowerPreview.cs
 *  Author:             Han Bi (301176547)
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        June 26, 2022
 *  Description:        Previewing the tower
 *  Revision History:   June 26, 2022 (Han Bi): Initial script.
 *                      August 1, 2022 (Yuk Yee Wong): Reorganised the code.
 *                      
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPreview : MonoBehaviour
{
    [SerializeField] GameObject TowerRange;
    [SerializeField] LayerMask invalidObjects;
    [SerializeField] Collider[] hitColliders;
    [SerializeField] bool isValidPosition = false;

    private void FixedUpdate()
    {
        isValidPosition = isValid();
    }

    public void ChangeRangeColor(Color colour)
    {
        TowerRange.GetComponentInChildren<SpriteRenderer>().color = colour;

    }

    public bool isValid()
    {
        Collider[] hitColliders = Physics.OverlapBox(
            new Vector3(transform.position.x, transform.position.y + 1.835f,
            transform.position.z + 0.03f), new Vector3(3.75f / 8, 14.75f / 8, 3.75f / 8),
            Quaternion.identity,
            invalidObjects);

        if (hitColliders.Length > 0)
        {
            
            return false;
        }
        else
        {
            return true;
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
    //    //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
    //    Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y+1.835f, transform.position.z + 0.03f), new Vector3(3.75f/4, 14.71f/4, 3.75f/4));
    //}


    public bool GetIsValidPosition()
    {
        return isValidPosition;
    }

}
