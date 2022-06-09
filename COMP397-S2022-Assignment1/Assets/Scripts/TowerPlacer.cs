using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{

    [SerializeField] GameObject crossbowTower;
    [SerializeField] GameObject crossbowTowerPreview;


    GameObject towerPreview;
    [SerializeField] bool isPreview = false;

    public Vector3 screenPos;
    public Vector3 worldPos;

    public LayerMask ground = 1<<7;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isPreview)
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
                    PlaceTower();
                    Destroy(towerPreview);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Destroy(towerPreview.gameObject);
                isPreview = false;
            }
        }
    }

    public void PreviewTower()
    {
        if (!isPreview)
        {
            isPreview = true;
            towerPreview = Instantiate(crossbowTowerPreview);
        }
    }

    public void PlaceTower()
    {

        isPreview = false;
        GameObject tower = Instantiate(crossbowTower, worldPos, Quaternion.identity);
    }


}
