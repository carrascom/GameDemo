using UnityEngine;
using System.Collections;

public class CameraTrackingPerspective : MonoBehaviour {


            //How fast we want the camera to follow player
            //public float smoothX = 1;
            //public float smoothY = 1;
            public float cameraSpeed = 1;
            public float startDistance = -20;
            public float idleView = -5;
            public float moveView = -15;

            //To access position of the player
            private GameObject player;
            private Rigidbody2D player_rb2d;
            private bool moving;
            
            //For smoothing camera
            private Vector3 velocity;

	// Use this for initialization
	void Start () {
	   
                //Get player object and Rigidbody component to access position and speed
                player = GameObject.FindWithTag("Player");
                player_rb2d = player.GetComponent<Rigidbody2D>();

                transform.position = new Vector3( player.transform.position.x, player.transform.position.y, startDistance);
	}
	

            //Happens on a timed-schedule. Best for tracking physics/motion?
            void FixedUpdate () {

                // Check speed
                if( Mathf.Abs(player_rb2d.velocity.x) > 0.1)
                {
                    moving = true;
                }
                else
                {
                    moving = false;
                }

                //Mathf.SmoothDamp( current, target, current velocity, smooth time)
                float posx = Mathf.SmoothDamp( transform.position.x, player.transform.position.x, ref velocity.x, cameraSpeed );
                float posy = Mathf.SmoothDamp( transform.position.y, player.transform.position.y, ref velocity.y, cameraSpeed );
                float posz = Mathf.SmoothDamp( transform.position.z, (moving ? moveView:idleView), ref velocity.z, cameraSpeed ); // Lets us do special zooming stuff while moving

                               
                transform.position = new Vector3(posx, posy, posz);
            }
}
