using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

            public float speed = 10;
            public float maxSpeed = 3;
            public float jumpPower = 5f;
            public int maxJumps = 3;

            //True when player is touching the ground
            public bool grounded;
            

            private int jumpCount = 0;
            private Rigidbody2D rb2d;
            private Animator anim;
	// Use this for initialization
	void Start () {
                rb2d = gameObject.GetComponent<Rigidbody2D>();
                anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
            // -- Best for User Input
	void Update () {

                //Triggers the "Animator" states in the editor
                anim.SetBool("Grounded", grounded);
                anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x) );

                bool jumpPushed = false;

                // While jump is held down, we want the player to crouch
                if( Input.GetButton("Jump") )
                {
                    jumpPushed = true;
                    anim.Play("Player_crouch",0);
                }

                anim.SetBool("Jump Button Down", jumpPushed);

                if (Input.GetButtonUp("Jump") )
                {
                    //For jump animation
                    anim.SetTrigger("Jump Now");

                    //First Jump
                    if( grounded )
                    {
                        rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                        jumpCount = 1;

                        //In the air!
                        grounded = !grounded;
                    }

                    //Other Jumps
                    else
                    {
                        if( jumpCount < maxJumps )
                        {
                            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                            jumpCount++;
                        }

                        else
                        {
                            //Reset jumps once you run out
                            jumpCount = 0;
                        }
                    }

                }

	
	}

            // -- Best for Physics updates
            void FixedUpdate () {

                // This takes input from joystick or arrow keys. Can range from -1..1
                float h = Input.GetAxis("Horizontal");

                //Apply the force to our player
                rb2d.AddForce( (Vector2.right * speed) * h );
                                        // X-axis * speed * how hard & direction joystick/key is pressed

                
                // Movement
                if( rb2d.velocity.x > 0.1 )
                {
                    //Our character faces right
                    transform.localScale = new Vector2(1, 1);
                    if( rb2d.velocity.x > maxSpeed)
                    {
                        rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
                    }
                }
                
                if ( rb2d.velocity.x < -0.1 )
                {
                    //Our character faces left
                    transform.localScale = new Vector2(-1, 1);
                    if( rb2d.velocity.x < -maxSpeed)
                    {
                        rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
                    }
                }

            }
}
