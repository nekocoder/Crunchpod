using UnityEngine;
using System.Collections;

public class logicUnit : MonoBehaviour {

	//variables
	bool[] moving = new bool[4];
	float[] rotations = new float[2];
	float[] speed= new float[2];

	// Use this for initialization
	void Start () 
	{
		//start AI()
			this.GetComponent<AIModulePlayer> ().reset ();	

		//reset or initialize variables
			reset ();
	}

	void reset()
	{
		moving[0] = false;
		rotations [0] = 0;
		rotations [1] = 0;
		//speed [0] = (float)1;
		//speed [1] = (float)1;
		speed = this.GetComponent<AIModulePlayer> ().speed;
	}
//Update methods
	// Update is called once per frame
	void Update () 
	{
		if (GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().getState()=="game") 
		{
			//print("-we're a go");
			
			//update variables

			//update rotations
				calcRotation ();
			//update position
				move ();
			//check for and fix boundary collisions
				//boundaryCheck ();

			//check status
				//print("Chomp: "+Input.GetKey ("joystick button 0"));
				//print ("Movement:  [" + Input.GetAxis ("Horizontal")+"/"+ Input.GetAxis ("Vertical")+"]");
				//print ("Movement:  [" + Input.GetAxisRaw ("Horizontal")+"/"+ Input.GetAxisRaw ("Vertical")+"]");
				//print ("Targeting: [" + Input.GetAxis ("Target Horizontal") +"/" + Input.GetAxis ("Target Vertical")+"]");
				//print ("Targeting: [" + Input.GetAxisRaw ("Target Horizontal") +"/" + Input.GetAxisRaw ("Target Vertical")+"]");
				//print ("Rotations: [" + rotations [0] + "/" + rotations [1] + "]");
		}
		else
		{
			//print("-hanging tight");
		}
	}
	//called before physics calculations
	void FixedUpdate()
	{

	}
//calculation methonds
	void calcRotation()
	{
		float[] jsp = new float[4]; //joyStickPositions
		jsp [0] = Input.GetAxis ("Horizontal");
		jsp [1] = Input.GetAxis ("Vertical");
		jsp [2] = Input.GetAxis ("Target Horizontal");
		jsp [3] = Input.GetAxis ("Target Vertical");

		for(int i=0;i<jsp.Length;i++)
		{
			if (jsp[i] <= 0)
				jsp[i] *= -1;
			else
				jsp[i]+=1;
		}

		rotations[0]= (jsp[0]*90) + (jsp[1]*90);
		rotations[1]= (jsp[2]*90) + (jsp[3]*90);

		//print ("distiled: "+jsp[0]+"/"+jsp[1]+"/"+jsp[2]+"/"+jsp[3]);
	}

//modification methods
		void move()
	{

		bool seeking= this.GetComponent<AIModulePlayer>().seeking;

		if (seeking)
		{
			//helper variables
				Vector3 target =this.GetComponent<AIModulePlayer>().target;	

			//move the object
				this.rigidbody2D.MovePosition(target);

		} else 
		{
			//this.rigidbody2D.AddForce()
		}

	}
	}
