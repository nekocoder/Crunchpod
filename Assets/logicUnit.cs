using UnityEngine;
using System.Collections;



public class LogicUnit : MonoBehaviour {

	//variables
		//motion and orientation
			bool[] moving = new bool[4];
			public float[] rotations = new float[3];
			public float[] speed= new float[2];
			public Vector3 target= new Vector3();	//Position to move to 
			public bool seeking;
			public float[] limits = new float[4];	//Space player is allowed to move within
		//Health 
			public int[] HP = new int[3];			//present HP, max HP and lives
			public bool hurt;
			public int[] invincibilityFrames=new int[2]; //present frame and max number of frames
		//Abilities
			public int ATK;
	
	// Use this for initialization
	void Awake() 
	{
		//reset or initialize variables
			reset ();
			
		//start AI()
			//this.GetComponent<AIModulePlayer> ().reset ();	
	}

	void reset()
	{
		moving[0] = false;
		rotations [0] = 0;
		rotations [1] = 0;
		rotations [2] = 0;
		speed [0] = (float)1;
		speed [1] = (float)1;		
	}
//Update methods
	// Update is called once per frame	
	void Update () 
	{				
		//check status
			//print("Chomp: "+Input.GetKey ("joystick button 0"));
			//print ("Movement:  [" + Input.GetAxis ("Horizontal")+"/"+ Input.GetAxis ("Vertical")+"]");
			//print ("Movement:  [" + Input.GetAxisRaw ("Horizontal")+"/"+ Input.GetAxisRaw ("Vertical")+"]");
			//print ("Targeting: [" + Input.GetAxis ("Target Horizontal") +"/" + Input.GetAxis ("Target Vertical")+"]");
			//print ("Targeting: [" + Input.GetAxisRaw ("Target Horizontal") +"/" + Input.GetAxisRaw ("Target Vertical")+"]");
			//print ("Rotations: [" + rotations [0] + "/" + rotations [1] + "]");	
	}
	//called before physics calculations
	void FixedUpdate()
	{
		if (GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().getState()=="game") 
		{
			//Test
				//print("-we're a go");
			//update rotations
				UpdateRotation ();
			//check for and fix boundary collisions
				checkBoundaries();
			//update position
				move ();
							
			//Test
				//ViewStatus();
		}
		else
		{
			//print("-hanging tight");
		}
		
		//Test			
			//print("Chomp: "+Input.GetKey ("joystick button 0"));
			//print ("Movement:  [" + Input.GetAxis ("Horizontal")+"/"+ Input.GetAxis ("Vertical")+"]");
			//print ("Movement:  [" + Input.GetAxisRaw ("Horizontal")+"/"+ Input.GetAxisRaw ("Vertical")+"]");
			//print ("Targeting: [" + Input.GetAxis ("Target Horizontal") +"/" + Input.GetAxis ("Target Vertical")+"]");
			//print ("Targeting: [" + Input.GetAxisRaw ("Target Horizontal") +"/" + Input.GetAxisRaw ("Target Vertical")+"]");
			//print ("Rotations: [" + rotations [0] + "/" + rotations [1] + "]");	
		
	}
//calculation methods


//modification methods
	void UpdateRotation()
	{
		//check to see if the PC is even trying to move. 
		//no point
			//bool seeking= this.GetComponent<AIModulePlayer>().seeking;
		
		//Don't bother with calculating and changing rotation if the thumbstick isn't being moved		
			if(seeking)
			{
				//move the object
					this.GetComponent<Rigidbody2D>().MoveRotation(rotations[0]);
				
				//test
					//print("--Rotation "+rotations[0]);
			}		
		
	}
	void move()
	{

		//bool seeking= this.GetComponent<AIModulePlayer>().seeking;

		if (seeking)
		{
			//move the object
				this.GetComponent<Rigidbody2D>().MovePosition(target);
				
			//debug output
				//print(this+" MOVIN'!");
				
		} 
		else 
		{
			
		}

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

		//debug output
			//print("Bounds: "+limits[0]+" "+limits[1]+" "+limits[2]+" "+limits[3]);
			//print("Target: "+target[0]+" "+target[1]);
			//print("Checking Boundaries");
	}
	public void ChangeStatus(char stat, int amt)
	{
		switch(stat)
		{
			case 'h':
				//alter health
					HP[0]+=amt;
				
				//cap health if too high
					if(HP[0]>HP[1])
					{
						//assign max health
							HP[0]=HP[1];
					}
				
				//lover lives if health is depleted
					if(HP[0]<0 && HP[2]>0)
					{
						//reset health
							HP[0]=HP[1];
					
						//lower lives
							HP[2]--;
					}
				break;
			default:
			break;		
		}
			
	}
	
//Info methods
	public void ViewStatus()
	{
		print("************************************");
		print("Rotation: "+this.GetComponent<Rigidbody2D>().rotation);		
		print("Position: "+this.GetComponent<Rigidbody2D>().position);
		print("Speed:    "+speed[0]+"|"+speed[1]);
		print("Target:   "+target[0]+"|"+target[1]+"|"+target[3]);
		print("************************************");
		
	}
	
}
