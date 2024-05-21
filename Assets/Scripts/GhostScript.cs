using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
   public MovementScript movement { get; private set;}
   public GhostHomeScript home { get; private set;}
   public GhostScatterScript scatter { get; private set;}
   public GhostChaseScript chase { get; private set;}
   public GhostFrightenedScript frightened { get; private set;}
   public GhostBehaviorScript initialBehavior;
   public Transform target;
   public int points = 200;

   private void Awake(){
      this.movement = GetComponent<MovementScript>();
      this.home = GetComponent<GhostHomeScript>();
      this.scatter = GetComponent<GhostScatterScript>();
      this.chase = GetComponent<GhostChaseScript>();
      this.frightened = GetComponent<GhostFrightenedScript>();
   }
   private void Start()
   {
      ResetState();
   }

   public void ResetState(){
      this.movement.ResetState();
      this.gameObject.SetActive(true);
      this.frightened.Disable();
      this.chase.Disable();
      this.scatter.Enable();
      this.home.Disable();

      if(this.home != this.initialBehavior){
         this.home.Disable();
      }
      if(this.initialBehavior != null)
      {
         this.initialBehavior.Enable();
      }
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
      if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman")){
         if(this.frightened.enabled)
         {
            FindObjectOfType<GameManagerScript>().GhostEaten(this);
         }else{
            FindObjectOfType<GameManagerScript>().PacManEaten();
         }
      }
   }
}
