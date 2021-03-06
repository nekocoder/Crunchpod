﻿using UnityEngine;

using System.Collections;

public class Protocol : MonoBehaviour {

	//variables
	public float[] Bounds = new float[4];
	ArrayList actionStack= new ArrayList();
	public int throttle;	//how many frames to wait before accepting input
	string state;

	//game bits
	public Transform PC;
	public Transform Enemy;

	//SAMPLE FROM UNITY DOCUMENTATION: VIEWPOINT TO SCREEN COORDINATES
	public Texture2D bottomPanel;
	void VPToScreenPtExample() 
	{
		Vector3 origin = Camera.main.ViewportToScreenPoint(new Vector3(0.25F, 0.1F, 0));
		Vector3 extent = Camera.main.ViewportToScreenPoint(new Vector3(0.5F, 0.2F, 0));
		GUI.DrawTexture(new Rect(origin.x, origin.y, extent.x, extent.y), bottomPanel);
	}
	//--//

	// Use this for initialization
	void Start () 
	{
		//Sets bounds at camera viewable area
			Bounds [0] = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).x;
			Bounds [1] = Camera.main.ViewportToWorldPoint(new Vector3(1,-1,0)).x;
			Bounds [2] = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).y;
			Bounds [3] = Camera.main.ViewportToWorldPoint(new Vector3 (-1, 1, 0)).y;

		actionStack.Add("TitleScreen");		
		throttle=100;
		//print (actionStack [0]);
		//print (actionStack [1]);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton ("Menu") && state == "game" && throttle==0) 
		{
			//print("---------PAWS");
			//print("\t Throttle: "+throttle);
			//print("\t State: "+state);
			//state = "menu";
			addMessage("PauseMenu");
			//this.GetComponent<TitleScreen>().framePause=100;
		}

		CheckStack ();
		//CheckStack (1);
		//CheckStack (2);

		//VPToScreenPtExample ();
			//GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().ResetAspect ();
	}

	void OnGUI()
	{
		//VPToScreenPtExample ();
	}

	public void addMessage(string msg)
	{
		actionStack.Add (msg);
	}

	void CheckStack()
	{
		if (actionStack.Count > 0) 
		{

			switch (actionStack [0].ToString ()) 
			{
				case "TitleScreen":
					///print("---------START Title Screen");
					this.GetComponent<TitleScreen> ().HitDaSwitches();	
					state="menu";
					break;
				case "MainMenu":
					//print("---------START Main Menu");
					this.GetComponent<MainMenu> ().HitDaSwitches();
					state="menu";
					break;
				case "PauseMenu":
					this.GetComponent<PauseMenu> ().HitDaSwitches();
					state="menu";
					break;
				case "GameOver":
					//print("---------START game over");
					this.GetComponent<GameOver> ().HitDaSwitches();
					state="menu";
					break;
				case "fresh":
					state="freshGame";
					break;
				case "load":
					//print ("loading assets");
					LoadAssets();
					break;
				case "unload":
					//print ("unloading assets");
					UnloadAssets();					
					break;
				default:
					break;
			}
			actionStack.RemoveAt (0);	
		}
		
		switch(state)
		{
			case "freshGame":
					throttle=25;
					state="game";
					break;
			case "game" :
				if(throttle>0)
				{
					throttle--;
				}
				break;
			case "menu" :
				break;
			default:
				//state="game";
				break;
		}
	}

	void LoadAssets()
	{
		//Instantiate(brick, Vector3 (x, y, 0), Quaternion.identity);
			Instantiate (PC, new Vector3 (0, 0, 0), Quaternion.identity);
			Instantiate (Enemy, new Vector3 (0, 0, 0), Quaternion.identity);
			//Instantiate (Enemy.GetComponent<AIModuleEnemy1>().shot,new Vector3 (25, 0, 0), Quaternion.identity);
		
		//add a message asking for a fresh game
			addMessage("fresh");
		
	}
	
	void UnloadAssets()
	{
		//remove player character
			Destroy (GameObject.FindWithTag ("Player"));
			Destroy (GameObject.FindWithTag ("Enemy"));
	}
	
	public string getState()
	{
		return state;
	}
	
	public void ViewStatus()
	{
		print("*************GLOBAL STATS***********");
		print("Bounds: "+Bounds[0]+"|"+Bounds[1]+"|"+Bounds[2]+"|"+Bounds[3]);		
		print("************************************");
		
	}
}
