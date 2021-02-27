using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjects : MonoBehaviour
{
    public GameObject colliderContainer;
    public GameObject spawnerPrefab;
    public float respawnTime = 0.8f;
    public GameObject canvas;
    public GameObject decorContainer;
    public GameObject decor;
    RectTransform rectCanvas;
    Vector2 canvasWidthHeight;
    public Collider2D[] colliders;
    public float radius;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        rectCanvas = canvas.GetComponent<RectTransform>();
        canvasWidthHeight = new Vector2(rectCanvas.rect.width, rectCanvas.rect.height);
        SpawnDecor();
       // Debug.Log(canvasWidthHeight.x + " : " + canvasWidthHeight.y);
        for(int i=0;i<=10;i++){
            SpawnPrefab();

        }
        //Debug.Log(canvasWidthHeight.x + " : " + canvasWidthHeight.y);
        
        StartCoroutine(SpawnerWave());
    }

    void SetSpawnerType(GameObject spawner){
        int chance = Random.Range(1, 5);
        switch (chance)
      {
          case 1:
              spawner.GetComponent<Image>().color = new Color32(168,255,225,225);
              spawner.name = "blue";
              break;
          case 2:
              spawner.GetComponent<Image>().color = new Color32(90,255,90,225);
              spawner.name = "green";
              break;
          case 3:
              spawner.GetComponent<Image>().color = new Color32(225,90,225,225);
              spawner.name = "pink";
              break;
          case 4:
              spawner.GetComponent<Image>().color = new Color32(225,225,0,225);
              spawner.name = "yellow";
              break;
          default:
              break;
      }
        
    }
    void SpawnDecor(){
        int xCord = (int)rectCanvas.rect.width/2;
        int yCord = (int)rectCanvas.rect.height/2;
        int spacing = 25;
        for(int i = -xCord; i<=xCord; i+=spacing){
            for(int j = -yCord; j<=yCord; j+=spacing){
                int temp = Random.Range(0,5);
                if(temp==1){
                    GameObject a = Instantiate(decor) as GameObject;
                    Vector2 tempPos = new Vector2((float)i,(float)j);
                    a.transform.position = tempPos;
                    a.transform.SetParent(decorContainer.transform, false);
                }

            }

        }
    }
    private void SpawnPrefab(){
        bool canSpawnHere = false;
        int safetyNet = 0;
        GameObject a = Instantiate(spawnerPrefab) as GameObject;
        SetSpawnerType(a);
        canvasWidthHeight = new Vector2(rectCanvas.rect.width, rectCanvas.rect.height);
        Vector2 randPos = new Vector2(0,0);
        //Debug.Log(xPos + " x " + yPos);
        
        while(!canSpawnHere){
            //Debug.Log("respawn");
            float xPos = Random.Range((-canvasWidthHeight.x/2)+20,(canvasWidthHeight.x/2)-20);
            float yPos = Random.Range((-canvasWidthHeight.y/2)+20,(canvasWidthHeight.y/2)-20);
            randPos = new Vector2(xPos,yPos);
            canSpawnHere = PreventSpawnOverlap(randPos);
            if(canSpawnHere){
                break;
            }
            safetyNet++;
            if(safetyNet>50){
                //Debug.Log("Too many attempts");
                break;
                
            }
        }
        colliderContainer = GameObject.Find("CollectableContainer");
        a.transform.position = randPos;
       // Debug.Log(a.transform.position.x + " x " + a.transform.position.y);
        a.transform.SetParent(colliderContainer.transform, false);
        
    }
    bool PreventSpawnOverlap(Vector2 spawnPos){
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        for(int i=0;i<colliders.Length; i++){
            Vector2 centerPoint = colliders[i].bounds.center;
            float width = colliders[i].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;

            float leftExtent = centerPoint.x - width;
            float rightExtent = centerPoint.y +width;
            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;
            if(spawnPos.x >= leftExtent && spawnPos.x <=rightExtent){
                if(spawnPos.y >= lowerExtent && spawnPos.y <= upperExtent){
                    return false;

                }

            }
           

        }
         return true;
    }
    IEnumerator SpawnerWave(){
        while(true){
            yield return new WaitForSeconds(respawnTime);
            SpawnPrefab();
        }
    }
}
