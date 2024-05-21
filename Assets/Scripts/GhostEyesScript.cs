
using UnityEngine;

public class GhostEyesScript : MonoBehaviour
{
   public Sprite up;
   public Sprite down;
   public Sprite right;
   public Sprite left;

   public SpriteRenderer spriteRenderer { get; private set;}
   public MovementScript movement { get; private set;}

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.movement = GetComponentInParent<MovementScript>();
    }
   private void Update()
   {
        if(this.movement.direction == Vector2.up)
        {
            this.spriteRenderer.sprite = this.up;
        }else if(this.movement.direction == Vector2.down)
        {
             this.spriteRenderer.sprite = this.down;
        
        }else if(this.movement.direction == Vector2.right)
        {
             this.spriteRenderer.sprite = this.right;
        
        }else if(this.movement.direction == Vector2.left)
        {
             this.spriteRenderer.sprite = this.left;
        }
   }
}
