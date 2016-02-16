/* SEE BOTTOM FOR FEATURES TO ADD *///
/////////////////////////////////////

using UnityEngine;
using System.Collections;


//Only works if the player/character/hero has the name "Player"
public class CameraTrackingOrthographic : MonoBehaviour {

/////////////////////////////////////////////
    /////////////* PUBLIC *//////////////
    /////////////////////////////////////
    
    // How fast we want the camera to follow player
    public float cameraSpeed = 1f;
    public float startDistance = -20;

////// Uncomment if independent speed is needed for each axis
//  public float smoothX = 1;
//  public float smoothY = 1;
////////////////

    // How far we want to zoom in/out when moving
    public float zoomIn  =  5;   // when Idle  
    public float zoomOut = 15;   // when Moving

    // When true, it's easier to see player during high jumps
    public bool expandOnY = true;
    public float ySpeedMultiplier = 1;                     // adjusts Camera based on Player's speed


//////////////////////////////////////////////
    /////////////* PRIVATE *//////////////
    //////////////////////////////////////

    //To access position of the player
    private GameObject player;
    private Rigidbody2D player_rb2d;
    private bool moving;
    
    //For smoothing camera
    private Vector3 velocity;
    private Camera playerCamera;



//////////////////////////////////////////////
////////////////* FUNCTIONS *////////////////
////////////////////////////////////////////

	// Use this for initialization
	void Start () {
	   
        // Get player object and Rigidbody component to access position and speed
        player = GameObject.Find("Player");                // change the name to the item you want your Camera to follow
        player_rb2d = player.GetComponent<Rigidbody2D>();

        // Orthographic needs 3D b/c camera still has a 3D position (Unity lies)
        transform.position = new Vector3( player.transform.position.x, player.transform.position.y, startDistance);
        playerCamera = gameObject.GetComponent<Camera>();
	}
	

    //Happens on a timed-schedule. Best for tracking physics/motion?
    void FixedUpdate () {

        // Check walking speed to set Camera zoom
        if( Mathf.Abs(player_rb2d.velocity.x) > 0.1)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        // Makes the camera movement smooth, from current camera position to player's position.
        //--> Mathf.SmoothDamp(current, target, current velocity, smooth time) <--
        float posx = Mathf.SmoothDamp( transform.position.x, player.transform.position.x, ref velocity.x, cameraSpeed );
        float posy = Mathf.SmoothDamp( transform.position.y, player.transform.position.y, ref velocity.y, cameraSpeed );
        float posCamera;                                   // Lets us do special zooming stuff while moving


        // TRUE  = Zoom out faster when player is flying through air (e.g. falling)
        // FALSE = Zoom does not consider Y-axis (up/down) movement
        if( expandOnY )
        {
            // Camera speeds up as the Player falls faster

            // -- Don't divide by 0:
            //      If player is in the air, set a faster/slower speed for the camera
            float validSpeed = (player_rb2d.velocity.y == 0) ? 1:(cameraSpeed / player_rb2d.velocity.y);


            float ySpeed = Mathf.Clamp( Mathf.Abs(validSpeed), 0.1f, cameraSpeed);        // Clamp(currentVal, lowestNum, highestNum)
            posCamera = Mathf.SmoothDamp( playerCamera.orthographicSize, (moving ? zoomOut:zoomIn), ref velocity.z, ySpeed); 

        }
        else
        {
            posCamera = Mathf.SmoothDamp( playerCamera.orthographicSize, (moving ? zoomOut:zoomIn), ref velocity.z, cameraSpeed );
        }


        transform.position = new Vector3(posx, posy, transform.position.z);
        playerCamera.orthographicSize = posCamera;
    }
}


//////////////////////////////////////////////////
//////////// FUNCTIONALITY TO ADD ///////////////
////////////////////////////////////////////////

/*
 * Pause before the camera zooms back in
   -- A tad annoying to be constantly zooming
 * 
*/
