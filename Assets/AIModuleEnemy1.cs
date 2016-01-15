using UnityEngine;
using System.Collections;

public class AIModuleEnemy1 : MonoBehaviour
{
	//variable
		Vector3 goal; 	//pos to move to
		int[] cannon= new int[2];	//affects fire rate [frame counter,frames between firing]
		public Transform shot; //bullet
		
		
	// Use this for initialization
	void Start () 
	{
		reset ();
	}
	
	public void reset()
	{
		this.GetComponent<LogicUnit>().speed [0] = (float).25;
		this.GetComponent<LogicUnit>().speed [1] = (float).25;
		
		this.GetComponent<LogicUnit>().seeking=true;
		
		this.GetComponent<LogicUnit>().rotations[0] = 0;
		this.GetComponent<LogicUnit>().rotations[1] = 1;
		this.GetComponent<LogicUnit>().rotations[2] = 0;
		
		this.GetComponent<LogicUnit>().invincibilityFrames[0]=0;
		this.GetComponent<LogicUnit>().invincibilityFrames[1]=10;
		
		this.GetComponent<LogicUnit>().HP[0]=100;
		this.GetComponent<LogicUnit>().HP[1]=100;		
		
		this.GetComponent<LogicUnit>().ATK=20;

		this.GetComponent<LogicUnit>().target = new Vector3 (this.GetComponent<Rigidbody2D>().transform.position.x ,
		                      this.GetComponent<Rigidbody2D>().transform.position.y,
							  this.GetComponent<Rigidbody2D>().transform.position.z);
							  
		this.GetComponent<LogicUnit>().limits = GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().Bounds;
		
		PrepGoal();
		
		//Cannon will fire once every X frames
			cannon[0]=0;
			cannon[1]=100;
		
		//goal =new Vector3( 100,100,1);
		//check that goal is within game area
				CheckGoalIntegrity();
		
		//goal = new Vector3 (		Random.Range(this.GetComponent<LogicUnit>().limits[0],this.GetComponent<LogicUnit>().limits[1]),Random.Range(this.GetComponent<LogicUnit>().limits[2],this.GetComponent<LogicUnit>().limits[3]),this.rigidbody2D.transform.position.z);
	}
	public void Update()
	{
		//print("Pos: "+this.GetComponent<Transform>().position);
	}
	public void FixedUpdate()
	{
		if (GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().getState()=="game") 
		{
			//Random Target
				Behavour1();
			
			//Rotation
					
			//test
				//print("ENEMY IS DOING STUFF (or should be)");
		}
	}
	
	void Behavour1()	
	{
		//debug output
			//print("Arrived?: "+(this.rigidbody2D.transform.position==this.GetComponent<LogicUnit>().target));
			//print("--running Mook1 behaviour");
			
		/*
		Tier 1 mook
		-------------
		Move to random points
		rotate constantly
		fire once a second
		*/
		
		//Check proximity to goal
			CheckGoalStatus();
		//Calculate intermediate step position
			CalcNextStep();
		//Update Rotation	
			CalcRotation();
		//Check cannon and fire if necessary
			CheckCannon();
		//cooldowns
			Cooldown();
		
	}
	void CheckGoalStatus()	
	{
		//print("---Checking progression towards goal");
		
		/*
		Check if character is at goal
		[y]assign new goal
		[y]find distance to goal
		[y]normalize difference
		[y]assign normal vector components as speed		
		*/
		
		//Have we reached the goal
		if(this.GetComponent<Rigidbody2D>().transform.position==goal&& GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().getState()=="game")
		{
			//Will generate a new goal and set speed accordingly
				PrepGoal();
		}
		else
		{
			//Debug Output 
				//print("----------------------------");
				//print("NO NEW TARGET");
				//print(">>>Position: "+this.rigidbody2D.transform.position);
				//print(">>>Target:   "+this.GetComponent<LogicUnit>().MainMachine0]+"-"+this.GetComponent<LogicUnit>().MainMachine1]+"-"+this.GetComponent<LogicUnit>().MainMachine2]);
				//print(">>>Goal:    "+goal[0]+"-"+goal[1]);
				//print(">>>Speed:   "+this.GetComponent<LogicUnit>().speed[0]+"-"+this.GetComponent<LogicUnit>().speed[1]);
		}
	}
	void PrepGoal()
	{
		/*
			assign new goal
			find distance to goal
			normalize difference
			assign normal vector components as speed		
		*/
		
		//assign new goal
			goal= new Vector3 (	Random.Range(this.GetComponent<LogicUnit>().limits[0],this.GetComponent<LogicUnit>().limits[1]),Random.Range(this.GetComponent<LogicUnit>().limits[2],this.GetComponent<LogicUnit>().limits[3]),this.GetComponent<Rigidbody2D>().transform.position.z);
			
		//check that goal is within game area
			CheckGoalIntegrity();
			
		//Set Goal
			//this.GetComponent<LogicUnit>().target=goal;
		
		//Debug Output
			//print("---ACQUIRING TARGET: "+this.GetComponent<LogicUnit>().target);
			
	}
	void CalcNextStep()
	{
		//find distance
			Vector3 distance=goal-this.GetComponent<LogicUnit>().transform.position;
			
		//normalize distance
			if(distance.magnitude>1)
			{
				distance.Normalize();
			}
			
		//reduce
			distance=distance*0.25F;
			
		//assign components as speed
			this.GetComponent<LogicUnit>().speed[0]=distance[0];
			this.GetComponent<LogicUnit>().speed[1]=distance[1];		
		
			this.GetComponent<LogicUnit>().target = new Vector3(
			this.GetComponent<Rigidbody2D>().transform.position.x + this.GetComponent<LogicUnit>().speed [0],
			this.GetComponent<Rigidbody2D>().transform.position.y + this.GetComponent<LogicUnit>().speed [1], 
			this.GetComponent<Rigidbody2D>().transform.position.z);
			
		//Debug Output
			//print("---GOING THE DISTANCE"+distance[0]+"-"+distance[1]);
	}
	void CalcRotation()
	{
		this.GetComponent<LogicUnit>().rotations[0]=this.GetComponent<Rigidbody2D>().rotation+1;
		
		//print("---");
	}
	void CheckCannon()
	{
		if(cannon[0]==cannon[1])
		{
			//spawn shot
				 Instantiate (shot, this.GetComponent<Rigidbody2D>().position, this.GetComponent<Transform>().rotation);
				
				//bullet.GetComponent<LogicUnit>().rotations=this.GetComponent<LogicUnit>().rotations;
				
				//Instantiate (shot, new Vector3(0,0,0), Quaternion.identity);
				
				//bullet.GetComponent<AIModuleShot>().target=this.GetComponent<Transform>().position;
				
				//Instantiate (shot, this.GetComponent<LogicUnit>().target, Quaternion.identity);
				
			//reset counter
				cannon[0]=0;
		}
		
	}
	void Cooldown()
	{
		if(cannon[0]<cannon[1])
		{
			cannon[0]++;
		}
	}
	
	//Make sure goal isn't outside the bounds of the game area
	void CheckGoalIntegrity()
	{
		//check to make sure we're not out of bounds
		if (goal[0] < this.GetComponent<LogicUnit>().limits [0]) 
		{
			//print ("negative X");
			goal.Set (this.GetComponent<LogicUnit>().limits [0], goal.y, goal.z);
			//change = true;
		}
		if (goal[0] > this.GetComponent<LogicUnit>().limits [1]) 
		{
			//print ("positive X");
			goal.Set (this.GetComponent<LogicUnit>().limits [1], goal.y, goal.z);
			//change=true;
		}
		if (goal[1] < this.GetComponent<LogicUnit>().limits [2]) 
		{
			//print ("negative Y");
			goal.Set (goal.x, this.GetComponent<LogicUnit>().limits[2], goal.z);
			//change=true;
		}
		if (goal[1] > this.GetComponent<LogicUnit>().limits [3]) 
		{
			//print ("Positive Y");
			goal.Set (goal.x, this.GetComponent<LogicUnit>().limits [3], goal.z);
			//change=true;
		}

		//debug output
			//print("Bounds: "+this.GetComponent<LogicUnit>().limits[0]+" "+this.GetComponent<LogicUnit>().limits[1]+" "+this.GetComponent<LogicUnit>().limits[2]+" "+this.GetComponent<LogicUnit>().limits[3]);
			//print("Target: "+this.GetComponent<LogicUnit>().MainMachine0]+" "+this.GetComponent<LogicUnit>().MainMachine1]);
			//print("Checking Boundaries");
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		//print("-FIRST CONTACT ");
	}
	void OnTriggerStay2D(Collider2D col)
	{
		//print("-CONTINUAL COLLISION");
	}
	void OnTriggerExit2D(Collider2D col)
	{
		//print("-EXIT COLLISION");
	}

}