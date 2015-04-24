using UnityEngine;
using System.Collections;

public class AIModulePlayer : MonoBehaviour 
{
	//variable
		public float[] speed=new float[2];
		public bool seeking;
		public Vector3 target= new Vector3();	
		public float[] limits = new float[4];

	// Use this for initialization
	void Start () 
	{
		reset ();
	}

	public void reset()
	{
		speed [0] = (float)1;
		speed [1] = (float)1;

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
		checkBoundaries ();
	
	}

	void checkInput()
	{
		if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) 
		{
			seeking=true;
		}
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
