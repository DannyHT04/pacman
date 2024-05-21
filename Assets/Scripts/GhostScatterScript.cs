
using UnityEngine;

public class GhostScatterScript : GhostBehaviorScript
{

    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }
    private void OnTriggerEnter2D(Collider2D other){
        NodeScript node = other.GetComponent<NodeScript>();
        if(node != null && this.enabled  && !this.ghost.frightened.enabled)
        {

            int index = Random.Range(0, node.availableDirections.Count);

            if(node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1)
            {
                index++;

                if(index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }
            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
