
using UnityEngine;

public class PelletScript : MonoBehaviour
{
 public int points = 10;

// if protected, subclass can access the function
// private means only the class can access it
// virtual can be redefine
 protected virtual void Eat(){
    FindObjectOfType<GameManagerScript>().PelletEaten(this);
 }
 private void OnTriggerEnter2D(Collider2D other){
    if(other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
    {
        Eat();
    }
 }
}
