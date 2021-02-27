using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //https://www.youtube.com/watch?v=HhIy23NImdA
    //https://www.youtube.com/watch?v=cOy9JZXlGQ0
    
    public float moveSpeed = 3.0f;
    public float lastSpeedUpgrade = 1.5f;
    RectTransform rectCanvas;
    public GameObject portal;
    public GameObject canvas;
    Vector2 canvasSize;
    public GameObject player;
    public Sprite sp1;
    public Sprite rest;
    public Sprite sp2;
    public Sprite currentSprite;
    //public Rigidbody2D rb;
    Vector2 movement;
    public InputAction wasd;
    public InputAction mouse;
    Vector2 playerStill = new Vector2(0f,0f);
    bool playerIsResting = true;
    public CharacterController controller;
    public static bool spawnedPortal = false;
    public static bool isPaused=false;
    public static bool upgradeOpen = false;
    public GameObject pauseMenu;
    void Start(){
        upgradeOpen=false;
        isPaused=false;
       // SpawnPortal();
        spawnedPortal = true;
        rectCanvas = canvas.GetComponent<RectTransform>();
        canvasSize = new Vector2(rectCanvas.rect.width, rectCanvas.rect.height);
        
        controller = this.GetComponent<CharacterController>();
        currentSprite =rest;
        
        
    }
    public void UpgradeSpeedValue(){
        moveSpeed+=(lastSpeedUpgrade*0.6f);
        lastSpeedUpgrade*=0.6f;
        Debug.Log(moveSpeed);
    }
    public void SpawnPortal(GameObject portal){

            //GameObject a = Instantiate(portal) as GameObject;
            Vector2 randPos = new Vector2(0,0);
            
            float xPos = Random.Range((-canvasSize.x/2)+20,(canvasSize.x/2)-20);
            float yPos = Random.Range((-canvasSize.y/2)+20,(canvasSize.y/2)-20);
            randPos = new Vector2(xPos,yPos);
            Debug.Log(randPos);
            portal.GetComponent<RectTransform>().position = randPos;
            //portal.transform.SetParent(canvas.transform, false);
            spawnedPortal=true;

    }
    void OnEnable(){
        wasd.Enable();
        mouse.Enable();
    }
    void OnDisable(){
        wasd.Disable();
        mouse.Disable();
    }
    void TurnSprite(){
        if(Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame|| Keyboard.current.sKey.wasPressedThisFrame){
            if(Keyboard.current.aKey.wasPressedThisFrame){
                player.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,90);
            }
            else if(Keyboard.current.dKey.wasPressedThisFrame){
            player.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,-90);
            }
            else if(Keyboard.current.sKey.wasPressedThisFrame){
                player.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,-180);

            }
            else{
               player.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,0); 
            }
        
            
        }
        

    }

    void ClampBoundaries(){
        Vector2 viewPos = Camera.main.ScreenToWorldPoint(this.transform.position);
        
        viewPos.x = Mathf.Clamp(viewPos.x, (-canvasSize.x/2)/100, (canvasSize.x/2)/100);//if it is not between the two clamp it to min/max
        viewPos.y = Mathf.Clamp(viewPos.y, (-canvasSize.y/2)/100, (canvasSize.y/2)/100);

        
        
        this.transform.position = viewPos;
        Debug.Log(this.transform.position.x+ " " + this.transform.position.y);
    }
    public void CloseUpgrade(){
        upgradeOpen=false;
    }
    public void UnPause(){
        Time.timeScale=1;
        isPaused=false;
    }
    public void Pause(){
        if(Keyboard.current.escapeKey.wasPressedThisFrame && !isPaused && !upgradeOpen){
            Debug.Log(isPaused);
            Time.timeScale = 0;
            isPaused=true;
            pauseMenu.SetActive(true);

        }else if(Keyboard.current.escapeKey.wasPressedThisFrame && isPaused && !upgradeOpen){
            Time.timeScale=1;
            isPaused=false;
            pauseMenu.SetActive(false);
        } 
        

    }
    public void UpgradesUnPause(){
        Time.timeScale=1;
    }
    void Update(){
        if(Mouse.current.leftButton.wasPressedThisFrame){
            Vector2 vec2 = mouse.ReadValue<Vector2>();
            Vector3 pos = new Vector3(vec2.x, vec2.y, gameObject.transform.position.z);
            Debug.Log(pos);
            this.gameObject.transform.position = pos;
            Debug.Log(gameObject.transform.position);

        }
        if(!spawnedPortal){
            //SpawnPortal();
        }
        
        Pause();

        TurnSprite();
        Vector2 inputVector = wasd.ReadValue<Vector2>();
        
        //Debug.Log(wasd.ReadValue<Vector2>().ToString());
        if(wasd.ReadValue<Vector2>() == playerStill){
            playerIsResting=true;
            
        }else{
            playerIsResting=false;
            StartCoroutine(AnimateFlying());
        }
        controller.Move(inputVector *Time.deltaTime *moveSpeed);
        }

    
    IEnumerator AnimateFlying(){
       
        while(!playerIsResting){
            
            float animSpeed=0.5f/moveSpeed;
            if(currentSprite==sp1){
                player.GetComponent<SpriteRenderer>().sprite = rest;
                yield return new WaitForSeconds(animSpeed);
                player.GetComponent<SpriteRenderer>().sprite = sp2;
                currentSprite=sp2;
                yield return new WaitForSeconds(animSpeed);
            } else{
                player.GetComponent<SpriteRenderer>().sprite = rest;
                yield return new WaitForSeconds(animSpeed);
                player.GetComponent<SpriteRenderer>().sprite = sp1;
                currentSprite=sp1;
                yield return new WaitForSeconds(animSpeed);
                
            }
        }
        
        
        player.GetComponent<SpriteRenderer>().sprite = rest;
    }
    
}
