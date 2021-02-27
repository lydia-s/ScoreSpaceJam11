using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject collectItem;
    bool isTouching = false;
    public float damageValue = 0.1f;
    public float lastUpgradeValue=0.05f;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = new Color32(168,255,225,225);
        this.name = "blue";
        StartCoroutine(ChangeType());
    }

    public void UpgradeDamageValue(){
        float multiplier = 0.4f;
        damageValue-=(lastUpgradeValue*multiplier);
        lastUpgradeValue*=multiplier;
    }
    IEnumerator ChangeType(){
        while(true){
        yield return new WaitForSeconds(5f);
        int chance = Random.Range(1, 5);
        switch (chance)
      {
          case 1:
              this.GetComponent<SpriteRenderer>().color = new Color32(168,255,225,225);
              this.name = "blue";
              break;
          case 2:
              this.GetComponent<SpriteRenderer>().color = new Color32(90,255,90,225);
              this.name = "green";
              break;
          case 3:
              this.GetComponent<SpriteRenderer>().color = new Color32(225,90,225,225);
              this.name = "pink";
              break;
          case 4:
              this.GetComponent<SpriteRenderer>().color = new Color32(225,225,0,225);
              this.name = "yellow";
              break;
          default:
              break;
      }
        }
      
     
    }
    IEnumerator Damage(){
        int i =0;
        Color tmp = this.GetComponent<SpriteRenderer>().color;
        while(i<4){
            i++;
            yield return new WaitForSeconds(0.1f);
            tmp.a = 0.2f;
            this.GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForSeconds(0.1f);
            tmp.a = 1f;
            this.GetComponent<SpriteRenderer>().color = tmp;
        }
        
    }
    IEnumerator IsTouchingBadItem(){
        yield return new WaitForSeconds(0.4f);
        if(isTouching &&  collectItem.name!=this.name && collectItem.name!="portal"){
            if(!GetComponent<AudioSource>().isPlaying){
            this.GetComponent<AudioSource>().Play();
            }
            Destroy(collectItem);
            HealthBar.health -= damageValue;
           // Debug.Log(damageValue);
            StartCoroutine(Damage());
            isTouching=false;
            collectItem=null;

        }

    }
    void OnTriggerEnter2D(Collider2D collider)
    {

        isTouching=true;
        collectItem=collider.gameObject;
        
        StartCoroutine(IsTouchingBadItem());
        

    }
    void OnTriggerExit2D(Collider2D collider)
    {
        isTouching=false;
        collectItem=null;
        

    }
}
