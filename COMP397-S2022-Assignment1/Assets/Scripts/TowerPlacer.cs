using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{

    [SerializeField] GameObject crossbowTower;
    [SerializeField] GameObject towerPrefab;


    GameObject towerPreview;
    [SerializeField] bool isShowing = false;

    public Vector3 screenPos;
    public Vector3 worldPos;

    public LayerMask ground;
    public LayerMask invalidObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isShowing)
        {
            screenPos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out RaycastHit hit, 100, ground))
            {
                worldPos = hit.transform.position;
                worldPos.y += 1;
            }

            towerPreview.transform.position = worldPos;

            if (Input.GetMouseButtonDown(0))
            {
                if (towerPreview.GetComponent<TowerPreview>().GetIsValidPosition())
                {
                    
                    BuyTower();
                    Destroy(towerPreview);
                }

            }

            if (Input.GetMouseButtonDown(1))
            {
                Destroy(towerPreview.gameObject);
                isShowing = false;
            }
        }
    }

    public void BuyTower()
    {

        isShowing = false;
        GameObject tower = Instantiate(crossbowTower, worldPos, Quaternion.identity);


    }

    public void PlaceTower()
    {
        if (!isShowing)
        {
            isShowing = true;
            towerPreview = Instantiate(towerPrefab);
        }

    }


}
