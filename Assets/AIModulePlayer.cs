using UnityEngine;
using System.Collections;

/////Class Purpose
//This class is Varick. He thinks and says "ZHU LI, DO THE THING!"
//
public class AIModulePlayer : MonoBehaviour 
{
	//variable
		

	// Use this for initialization
	void Start () 
	{
		reset ();
	}

	public void reset()
	{
		this.GetComponent<LogicUnit>().speed [0] = (float).30;
		this.GetComponent<LogicUnit>().speed [1] = (float).30;
		
		this.GetComponent<LogicUnit>().invincibilityFrames[0]=0;
		this.GetComponent<LogicUnit>().invincibilityFrames[1]=100;
		
		this.GetComponent<LogicUnit>().HP[0]=100;
		this.GetComponent<LogicUnit>().HP[1]=100;		
		
		this.GetComponent<LogicUnit>().ATK=20;
		
		this.GetComponent<LogicUnit>().rotations[0] = 0;
		this.GetComponent<LogicUnit>().rotations[1] = 1;
		this.GetComponent<LogicUnit>().rotations[2] = 0;

		this.GetComponent<LogicUnit>().target = new Vector3 (this.GetComponent<Rigidbody2D>().transform.position.x ,
		                      this.GetComponent<Rigidbody2D>().transform.position.y,
							  this.GetComponent<Rigidbody2D>().transform.position.z);
							  
		this.GetComponent<LogicUnit>().limits = GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().Bounds;
		
	}

	// Update is called once per frame
	void Update () 
	{
		if (GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().getState()=="game") 
		{
			checkInput ();
			//createCoordinates ();
			//createForwardVector();
			//CheckStatus();

			//update counters
				cooldown();
		}
	}
	
	//FixedUpdate is called in conjunction with time
	void FixedUpdate()
	{
		if (GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().getState()=="game") 
		{
			//checkInput ();
			//createCoordinates ();
			//createForwardVector();
			//CheckStatus();

			//update counters
				//cooldown();
		}
		//debug output statements
			//print("AIModulePlayer update");
	}

	void checkInput()
	{
		//set seeking to true if the thumbstick is not at defualt position. Set false otherwise
			float horiz=Input.GetAxis ("Horizontal");
			float vert=Input.GetAxis ("Vertical");
			float tHoriz=Input.GetAxis ("Target Horizontal");
			float tVert= Input.GetAxis ("Target Vertical");
			
			bool test= (horiz!= 0 ||  vert!= 0);
			
			if (test)//Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) 
			{
				this.GetComponent<LogicUnit>().seeking=true;
				
			}
			else
			{
				this.GetComponent<LogicUnit>().seeking=false;
			}
						
			createCoordinates(horiz, vert);
			createForwardVector(horiz, vert, tHoriz, tVert);
						
		//debug output statements			
			//print("Input:  H: "+Input.GetAxis ("Horizontal")+" V: "+Input.GetAxis ("Vertical")+" Seeking: "+test);
	}
	
	void CheckStatus()
	{
		if(GetComponent<LogicUnit>().hurt)
		{
			//lower health
				this.GetComponent<LogicUnit>().ChangeStatus('h',-20);
				
			//reset flag
				this.GetComponent<LogicUnit>().hurt=false;
		}
		if(this.GetComponent<LogicUnit>().HP[0]<0 && this.GetComponent<LogicUnit>().HP[2]<1)
		{
			//Player is dead
				print("player is dead");
				
			//send game over message
			GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().addMessage("GameOver");
		}
	}
	
	void createForwardVector(float horiz,float vert,float tHoriz,float tVert)
	{
		//Grab animator object
			Animator animator= GetComponent<Animator>();
		
		//Don't bother with calculating and changing rotation if the thumbstick isn't being moved		
			if(this.GetComponent<LogicUnit>().seeking)
			{
				float[] jsp = new float[4]; //joyStickPositions
				jsp [0] = horiz; //Input.GetAxis ("Horizontal");
				jsp [1] = vert; //Input.GetAxis ("Vertical");
				jsp [2] = tHoriz; //Input.GetAxis ("Target Horizontal");
				jsp [3] = tVert; //Input.GetAxis ("Target Vertical");
				
				//flip vertical reading*
				//*range is 1 < y < -1; should be -1 < y < 1
					jsp[1] *= -1;
				
				//get theta sub 
					//float theta=Mathf.Atan(jsp[1]/jsp[0]) * Mathf.Rad2Deg;
					float theta=Mathf.Atan2(jsp[1],jsp[0]) * Mathf.Rad2Deg;
					
				//adjust accordingly
					if(theta<0)
					{
						theta+=360;
					}
				
				//document rotation and slope
					this.GetComponent<LogicUnit>().rotations[0]= theta;
					this.GetComponent<LogicUnit>().rotations[1]= jsp[0];
					this.GetComponent<LogicUnit>().rotations[2]= jsp[1];
				
				//Debug Output Statements	
					//print ("distiled: "+jsp[0]+"/"+jsp[1]+"/"+jsp[2]+"/"+jsp[3]);
					//print ("rotations: "+rotations[0]+"..."+rotations[1]);
					//print ("rotations: " + (Mathf.Rad2Deg * Mathf.Acos (jsp [0]))+"...."+(Mathf.Rad2Deg* Mathf.Asin(jsp[1])));
					//print("Quad: "+quad);
					//print("-Theta: "+theta);				
			}
			
			//Change booster flag in animator, but only if the game is active (not during menus)
				bool game=(GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().getState()=="game");
					
				animator.SetBool("Boosters",this.GetComponent<LogicUnit>().seeking && game);			
		
	}
	
	void createCoordinates(float horiz, float vert)
	{
		//set new target 
		/*	
			this.GetComponent<LogicUnit>().target = new Vector3 (
							this.GetComponent<Rigidbody2D>().transform.position.x + 
							(this.GetComponent<LogicUnit>().speed [0]*Input.GetAxis("Horizontal")),
		                    this.GetComponent<Rigidbody2D>().transform.position.y - 
							(this.GetComponent<LogicUnit>().speed [1]*Input.GetAxis("Vertical")),
		                    this.GetComponent<Rigidbody2D>().transform.position.z);
		*/
		//set new target 
			
			this.GetComponent<LogicUnit>().target = new Vector3 (
							this.GetComponent<Rigidbody2D>().transform.position.x + 
							(this.GetComponent<LogicUnit>().speed [0]*horiz),
		                    this.GetComponent<Rigidbody2D>().transform.position.y - 
							(this.GetComponent<LogicUnit>().speed [1]*vert),
		                    this.GetComponent<Rigidbody2D>().transform.position.z);
							
		//set new target 
			/*
			this.GetComponent<LogicUnit>().target = new Vector3 (
							this.GetComponent<Rigidbody2D>().transform.position.x + 
							(horiz),
		                    this.GetComponent<Rigidbody2D>().transform.position.y - 
							(vert),
		                    this.GetComponent<Rigidbody2D>().transform.position.z);
			*/
		//debug output
			//print("NEW TARGET");
	}	
	
	
	
	//States	
	
	////Collision Detection
		void OnTriggerEnter2D(Collider2D col)
		{
			//check if invincibility frames are still running
				if(this.GetComponent<LogicUnit>().invincibilityFrames[0]==0&& GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().getState()=="game")
				{
					//Grab animator object
						Animator animator= GetComponent<Animator>();
						
					//set hurt flag
						animator.SetBool("Hurt",true);
						this.GetComponent<LogicUnit>().hurt=true;
						
					//reset invincibilityFrames
						this.GetComponent<LogicUnit>().invincibilityFrames[0]=this.GetComponent<LogicUnit>().invincibilityFrames[1];
					
					//Debug output
						//print("FIRST HIT!");
				}
				else
				{
					//Debug output
						//print("INVINCIBILITY FRAME: ["+this.GetComponent<LogicUnit>().invincibilityFrames[0]+"/"+this.GetComponent<LogicUnit>().invincibilityFrames[1]+"]");
				}
				
			//debug output
				//print("-FIRST TRIGGER ");
		}
		void OnTriggerStay2D(Collider2D col)
		{
			//check if invincibility frames are still running
				if(this.GetComponent<LogicUnit>().invincibilityFrames[0]==0&&GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().getState()=="game")
				{
					//Grab animator object
						Animator animator= GetComponent<Animator>();
						
					//set hurt flag
						animator.SetBool("Hurt",true);
						this.GetComponent<LogicUnit>().hurt=true;
						
					//reset invincibilityFrames
						this.GetComponent<LogicUnit>().invincibilityFrames[0]=this.GetComponent<LogicUnit>().invincibilityFrames[1];
						
					//debug output
						//print("MORE HITS!");
				}
				else
				{
					//Debug output
						//print("INVINCIBILITY FRAME: ["+this.GetComponent<LogicUnit>().invincibilityFrames[0]+"/"+this.GetComponent<LogicUnit>().invincibilityFrames[1]+"]");
				}
			
			//debug output
				//print("-CONTINUAL TRIGGER");
		}
		void OnTriggerExit2D(Collider2D col)
		{
			//debug output
				//print("-EXIT TRIGGER");
		}	
	
	////State logic
		void Hurt()
		{
			
		}
		
	
	//Cooldown
	void cooldown()
	{
		//invincibility frames
			if(this.GetComponent<LogicUnit>().invincibilityFrames[0]>0)
			{
				this.GetComponent<LogicUnit>().invincibilityFrames[0]--;
			}
			else
			{
				
			}
				
	}
}
