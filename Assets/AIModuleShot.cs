using UnityEngine;
using System.Collections;

public class AIModuleShot : MonoBehaviour {

	//variables
		//Motion and Orientation
			
	// Use this for initialization
		void Start () 
		{
			reset();
		}
		
		public void reset()
		{
			this.GetComponent<LogicUnit>().rotations[0] = this.GetComponent<Rigidbody2D>().rotation;
			
			this.GetComponent<LogicUnit>().speed [0] = (float).35 * Mathf.Acos(Mathf.Deg2Rad * this.GetComponent<LogicUnit>().rotations[0]);
			
			this.GetComponent<LogicUnit>().speed [1] = (float).35 * Mathf.Asin(Mathf.Deg2Rad * this.GetComponent<LogicUnit>().rotations[0]);
			
			//CalcNextStep();
			
			this.GetComponent<LogicUnit>().seeking=false;//true;
						
			this.GetComponent<LogicUnit>().invincibilityFrames[0]=0;
			this.GetComponent<LogicUnit>().invincibilityFrames[1]=10;
			
			this.GetComponent<LogicUnit>().HP[0]=100;
			this.GetComponent<LogicUnit>().HP[1]=100;		
			
			this.GetComponent<LogicUnit>().ATK=20;
			
			this.GetComponent<LogicUnit>().target=this.GetComponent<Transform>().position;
			
			CalcNextStep();
		}

	// Update is called once per frame
		void Update () 
		{
			CalcNextStep();
			
			//test
				this.GetComponent<LogicUnit>().ViewStatus();
		}
		
	//calculate next position to move to
		void CalcNextStep()
		{
			//float rot=this.GetComponent<Rigidbody2D>().rotation;
			
			this.GetComponent<LogicUnit>().target[0] = this.GetComponent<Rigidbody2D>().transform.position.x + this.GetComponent<LogicUnit>().speed [0];
			
			this.GetComponent<LogicUnit>().target[1]=
			this.GetComponent<Rigidbody2D>().transform.position.y + this.GetComponent<LogicUnit>().speed [1];
			
			this.GetComponent<LogicUnit>().target[2]=0;
			
			//print("Rotation: "+rot);
			
		}
		
}
