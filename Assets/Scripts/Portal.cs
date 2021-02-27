using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Sprite sea;
    public Sprite dirt;
    public Sprite grass;
    public GameObject collectableContainer;
    GameObject panel;
    public GameObject canvas;
    public GameObject decorContainer;
    public GameObject decorObj;
    public GameObject player;
    public GameObject upgrades;
    public GameObject portalContainer;
    public Vector2 canvasSize;
    RectTransform rectCanvas;
    void GenerateDecor(GameObject decor){
        canvas = GameObject.Find("Canvas");
        RectTransform rectCanvas = canvas.GetComponent<RectTransform>();
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
    void SelectNextWorld(){
        GameObject panel = GameObject.Find("Panel");
        int chance = Random.Range(1, 3);
        switch (chance)
      {
          case 1:
              panel.GetComponent<SpriteRenderer>().sprite = dirt;
              //spawn dirt objs
              break;
          case 2:
              panel.GetComponent<SpriteRenderer>().sprite = grass;
              //spawn grass objs
              break;
          case 3:
              panel.GetComponent<SpriteRenderer>().sprite = sea;
              //spawn sea objs
              break;
          default:
              break;
      }

    }
    public void MakeUpgradeWindowActive(){
        upgrades.SetActive(true);
        Time.timeScale = 0;
        PlayerMovement.upgradeOpen = true;
    }
    void Start(){
        rectCanvas = canvas.GetComponent<RectTransform>();
        canvasSize = new Vector2(rectCanvas.rect.width, rectCanvas.rect.height);
        Debug.Log(canvasSize.x+ " 8 " + canvasSize.y);
    }
    public void SpawnPortal(){
            rectCanvas = canvas.GetComponent<RectTransform>();
            int xCord = (int)canvasSize.x/2;
            int yCord = (int)canvasSize.y/2;
            int xPos = Random.Range(-xCord+30, xCord-30);
            int yPos = Random.Range(-yCord+30, yCord-30);
            Vector2 randPos = new Vector2(xPos,yPos);
            
            this.transform.SetParent(null,false);
            gameObject.transform.position = randPos;
            Debug.Log("new pos:"+randPos);
            this.transform.SetParent(portalContainer.transform,false);

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Player p = collider.GetComponent<Player>();
        if(p!=null){
            MakeUpgradeWindowActive();
            PlayerMovement.spawnedPortal=false;
            GameObject collect = GameObject.Find("CollectableContainer");
            //GameObject decor = GameObject.Find("DecorContainer");
            canvas = GameObject.Find("Canvas");
            
            //Destroy(decor);
            Destroy(collect);
            SelectNextWorld();
            collect = Instantiate(collectableContainer, new Vector2(0, 0), Quaternion.identity);
            collect.name = "CollectableContainer";
            collect.transform.SetParent(canvas.transform, false);
            //decor = Instantiate(decorContainer, new Vector2(0, 0), Quaternion.identity);
            //descor.name = "DecorContainer";
           // decor.transform.SetParent(canvas.transform, false);
            //decorContainer = decor;
           // GenerateDecor(decorObj);
            //player.GetComponent<PlayerMovement>().SpawnPortal(this.gameObject);
            //Destroy(this.gameObject);
            SpawnPortal();
        }else{
            Destroy(collider);
        }
        

    }

    
}
