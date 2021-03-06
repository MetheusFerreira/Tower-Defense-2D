using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    //list of towers (prefabs) that will instantiate
    public List<GameObject> towersPrefabs;
    //Transform of the spawning towers (Root Object)
    public Transform spawnTowerRoot;
    //list of towers (UI)
    public List<Image> towersUI;
    //id of tower to spawn
    int spawnID=-1;
    //SpawnPoints Tilemap
    public Tilemap spawnTilemap;
    
    void Update()
    {
        if(CanSpawn())
            DetectSpawnPoint();
    }

    bool CanSpawn()
    {
        if (spawnID == -1)
            return false;
        else
            return true;
    }

    void DetectSpawnPoint()
    {
        //Detect when mouse is clicked (first touch clicked)
        if(Input.touchCount> 0)
        {
            //get the world space postion of the mouse
            var touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            //get the position of the cell in the tilemap
            var cellPosDefault = spawnTilemap.WorldToCell(touchPos);
            //get the center position of the cell
            var cellPosCentered = spawnTilemap.GetCellCenterWorld(cellPosDefault);
            //check if we can spawn in that cell (collider)
            if(spawnTilemap.GetColliderType(cellPosDefault)== Tile.ColliderType.Sprite)
            {
                int towerCost = TowerCost(spawnID);
                //Check if currency is enough to spawn
                if(GameManager.instance.currency.EnoughCurrency(towerCost))
                {
                    //Use the amount of cost from the currency available
                    GameManager.instance.currency.Use(towerCost);
                    //Spawn the tower
                    SpawnTower(cellPosCentered);
                    //Disable the collider
                    spawnTilemap.SetColliderType(cellPosDefault, Tile.ColliderType.None);
                }
                else
                {
                    Debug.Log("Not Enough Currency");
                }                               
            }                                  
        }
    }

    public int TowerCost(int id)
    {
        switch(id)
        {
            case 0: return towersPrefabs[id].GetComponent<Tower_Pink>().cost;
            case 1: return towersPrefabs[id].GetComponent<Tower_Mask>().cost;
            case 2: return towersPrefabs[id].GetComponent<Tower_Ninja>().cost;  
            default:return -1;
        }
    }

    void SpawnTower(Vector3 position)
    {
        GameObject tower = Instantiate(towersPrefabs[spawnID],spawnTowerRoot);
        tower.transform.position = position;

        DeselectTowers();
    }

    public void SelectTower(int id)
    {
        DeselectTowers();
        //Set the spawnID
        spawnID = id;
        //Highlight the tower
        towersUI[spawnID].color = Color.white;        
    }

    public void DeselectTowers()
    {
        spawnID = -1;
        foreach(var t in towersUI)
        {
            t.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }    


    
}
