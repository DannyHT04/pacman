using UnityEngine;

public class PowerPelletScript : PelletScript
{
    protected override void Eat(){
    FindObjectOfType<GameManagerScript>().PowerPelletEaten(this);
 }
    public float duration = 8.0f;
}
