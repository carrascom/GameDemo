using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

            private Player player;
	// Use this for initialization
	void Start () {
	
                //Would return all components matching "Player", but we know we only have 1 instance attached.
                player = gameObject.GetComponentInParent<Player>();
	}
	
            //Called when the trigger enters another collider
	void OnTriggerEnter2D(Collider2D col)
            {
                player.grounded = true;
            }

            //Called every frame a trigger is in the collider
            void OnTriggerStay2D(Collider2D col)
            {
                player.grounded = true;
            }

            //Called once the trigger exits collider
            void OnTriggerExit2D(Collider2D col)
            {
                player.grounded = false;
            }

}
