using UnityEngine;
using System.Collections;

public class AIModuleEnemy1 : MonoBehaviour
{
	//variable
		

	// Use this for initialization
	void Start () 
	{
		reset ();
	}

	public void reset()
	{
		this.GetComponent<LogicUnit>().speed [0] = (float)1;
		this.GetComponent<LogicUnit>().speed [1] = (float)1;
		
		this.GetComponent<LogicUnit>().rotations[0] = 0;
		this.GetComponent<LogicUnit>().rotations[1] = 1;
		this.GetComponent<LogicUnit>().rotations[2] = 0;
		
		this.GetComponent<LogicUnit>().invincibilityFrames[0]=0;
		this.GetComponent<LogicUnit>().invincibilityFrames[1]=10;
		
		this.GetComponent<LogicUnit>().HP[0]=100;
		this.GetComponent<LogicUnit>().HP[1]=100;		
		
		this.GetComponent<LogicUnit>().ATK=20;

		this.GetComponent<LogicUnit>().target = new Vector3 (this.rigidbody2D.transform.position.x ,
		                      this.rigidbody2D.transform.position.y,
							  this.rigidbody2D.transform.position.z);
							  
		this.GetComponent<LogicUnit>().limits = GameObject.FindGameObjectWithTag ("GroundControl").GetComponent<Protocol> ().Bounds;		
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