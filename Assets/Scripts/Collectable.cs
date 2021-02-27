using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    string collisionName;
    bool isTouching = false;
    Animator animator;
    void Start(){
        animator = this.GetComponent<Animator>();
        
    }

    IEnumerator CollectItem(){
        
        yield return new WaitForSeconds(0.2f);
        
        if(isTouching && collisionName==this.name){
            if(!GetComponent<AudioSource>().isPlaying){
            this.GetComponent<AudioSource>().Play();
            }
            UpdateScore.score++;
            animator.Play("Destroy");
            yield return new WaitForSeconds(1f);
            Destroy(this.gameObject);
            isTouching=false;
            collisionName=""; 
        }

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Player p = collider.GetComponent<Player>();
        if(p!=null){
        isTouching=true;
        collisionName=collider.gameObject.name;
        StartCoroutine(CollectItem());
        }
        

    }
    void OnTriggerExit2D(Collider2D collider)
    {
        isTouching=false;
        collisionName="";
        

    }
}
