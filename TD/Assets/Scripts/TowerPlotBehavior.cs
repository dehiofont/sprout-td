using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlotBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tpHighlight;
    public GameObject tpTower;
    public GameObject tpTowerPrefab;
    public GameObject tpSpawnPoint;
    public LevelLogic mainGameLogic;

    public int towerValue = 100;

    void Start()
    {
        mainGameLogic = GameObject.Find("GameLogic").GetComponent<LevelLogic>();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseOver()
    {
        tpHighlight.SetActive(true);

        if(Input.GetMouseButtonDown(0) && !tpTower && mainGameLogic.currentGold >= 100)
        {
            //Debug.Log(mainGameLogic.currentGold);
            mainGameLogic.modifyGold(-100);
            tpTower = Instantiate(tpTowerPrefab, tpSpawnPoint.transform.position, tpSpawnPoint.transform.rotation);
        }

        if (Input.GetMouseButtonDown(1) && tpTower)
        {
            mainGameLogic.modifyGold(towerValue);
            Destroy(tpTower);
        }
    }

    private void OnMouseExit()
    {
        tpHighlight.SetActive(false);
    }

    
}
