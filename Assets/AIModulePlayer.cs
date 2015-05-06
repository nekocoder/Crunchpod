using UnityEngine;
using System.Collections;

public class AIModulePlayer : MonoBehaviour 
{
	//variable
		public float[] speed=new float[2];		//movement speed
		public bool seeking;					//Should the player be moving?
		public Vector3 target= new Vector3();	//Position to move to 
		public float[] limits = new float[4];	//Space player is allowed to move within
		public float[] rotation = new float[3];	//angle rotated, x component of forward vector, y component of forward vector

	// Use this for initialization
	void Start () 
	{
		reset ();
	}

	public void reset()
	{
		speed [0] = (float)1;
		speed [1] = (float)1;
		
		rotation[0] = 0;
		rotation[1] = 1;
		rotation[2] = 0;

		seeking = false;

		target = new Vector3 (this.rigidbody2D.transform.position.x ,
		                      this.rigidbody2D.transform.position.y,
		                      this.rigidbody2D.transform.position.z);


		/*
		limits [0] = -1 * GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().rect.width * GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().orthographicSize;
		limits [1] = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().rect.width * GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().orthographicSize;
		limits [2] = -1 * GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().rect.height * GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().orthographicSize;
		limits [3] = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().rect.height * GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().orthographicSize;	
		*/

		limits = GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().Bounds;

		//Screen: from [0,0] to [Screen.width, Screen.height]
		//Viewport: from [0,0] to [1,1]

		//print ("Camera: "+GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().rect.height+".."+GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().rect.width+".."+GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().orthographicSize);
		//print ("Screen: " + Screen.height + "......" + Screen.height);
		//print ("limits: " + limits[0]+"...."+ limits[1]+"...."+ limits[2]+"...."+ limits[3]);
		//print (GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().ViewportToScreenPoint (target));
		//print ("Far clipping plane: "+GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().
	}

	// Update is called once per frame
	void Update () 
	{
		checkInput ();
		createCoordinates ();
		createForwardVector();
		checkBoundaries ();
	
		//debug output statements
			//print("AIModulePlayer update");
	}

	void checkInput()
	{
		//set seeking to true if the thumbstick is not at defualt position. Set false otherwise
			if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) 
			{
				seeking=true;
			}
			else
			{
				seeking=false;
			}
			
		//debug output statements
			//print("Intput:  H: "+Input.GetAxis ("Horizontal")+" V: "+Input.GetAxis ("Vertical")+" Seeking: "+seeking);
	}
	
	void createForwardVector()
	{
		//Grab animator object
			Animator animator= GetComponent<Animator>();
		
		//Don't bother with calculating and changing rotation if the thumbstick isn't being moved		
			if(seeking)
			{
				float[] jsp = new float[4]; //joyStickPositions
				jsp [0] = Input.GetAxis ("Horizontal");
				jsp [1] = Input.GetAxis ("Vertical");
				jsp [2] = Input.GetAxis ("Target Horizontal");
				jsp [3] = Input.GetAxis ("Target Vertical");
				
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
					rotation[0]= theta;
					rotation[1]= jsp[0];
					rotation[2]= jsp[1];
					
				//Debug Output Statements	
					//print ("distiled: "+jsp[0]+"/"+jsp[1]+"/"+jsp[2]+"/"+jsp[3]);
					//print ("rotations: "+rotations[0]+"..."+rotations[1]);
					//print ("rotations: " + (Mathf.Rad2Deg * Mathf.Acos (jsp [0]))+"...."+(Mathf.Rad2Deg* Mathf.Asin(jsp[1])));
					//print("Quad: "+quad);
					//print("Rotation: "+theta);				
			}
			
			//Change booster flag in animator
				animator.SetBool("Boosters",seeking);			
		
	}
	
	void createCoordinates()
	{
		target = new Vector3 (this.rigidbody2D.transform.position.x + (speed [0]*Input.GetAxis("Horizontal")),
		                      this.rigidbody2D.transform.position.y - (speed [1]*Input.GetAxis("Vertical")),
		                      this.rigidbody2D.transform.position.z);
	}	
	void checkBoundaries()
	{
		//check to make sure we're not out of bounds
		if (target[0] < limits [0]) 
		{
			//print ("negative X");
			target.Set (limits [0], target.y, target.z);
			//change = true;
		}
		if (target[0] > limits [1]) 
		{
			//print ("positive X");
			target.Set (limits [1], target.y, target.z);
			//change=true;
		}
		if (target[1] < limits [2]) 
		{
			//print ("negative Y");
			target.Set (target.x, limits[2], target.z);
			//change=true;
		}
		if (target[1] > limits [3]) 
		{
			//print ("Positive Y");
			target.Set (target.x, limits [3], target.z);
			//change=true;
		}		
	}
}
