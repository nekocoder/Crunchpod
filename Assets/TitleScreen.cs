using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	//status
	public string status;
	public int DefThrottle; //how many frames to wait before accepting input
	public int throttle;	//frame counter. used with DefThrottle
	
	//media
	public Texture2D logo;


	// Use this for initialization
	void Start () 
	{
		status = "inactive";
		throttle=0;
	}
	
	public bool HitDaSwitches()
	{
		switch(status)
		{
			case "active":
				status="inactive";
				throttle=0;
				
				//Debug Output
					//ShowInfo();
				break;
			case "inactive":				
				status="active";
				throttle=25;
				
				//Debug Output
					//ShowInfo();
				break;
			default:
				break;
		}
		
		return status=="active";
	}
	
	public void ShowInfo()
	{
		print("Status: "+status);
		print("Throttle: "+throttle);		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void setGUI()
	{
		float [] screenSize = new float[2]; //this.GetComponent<Protocol>().Bounds;
		
		screenSize[0]=Screen.width;
		screenSize[1]=Screen.height;

		//gui style
		GUI.skin.box.padding.bottom=0;
		GUI.skin.box.padding.top=0;
		GUI.skin.box.padding.left=0;
		GUI.skin.box.padding.right=0;


		//GUI.Box (new Rect (screenSize[0]/3, screenSize[1]/5,screenSize[0]/3,screenSize[1]/5), logo);
		//GUI.Box (new Rect (screenSize[0]/4, screenSize[1]/3,screenSize[0]/2,screenSize[1]/5), logo);
		prepAndServe ((float)1 / 4, -1, 'w', (float)1 / 2,logo);
	
	}
	void prepAndServe(float h, float v,char axis, float size, Texture2D pic)
	{
		float [] screenSize = new float[2];		
		screenSize[0]=Screen.width;
		screenSize[1]=Screen.height;
		
		float ratio= (float)pic.width/(float)pic.height;

		float horiz=0;
		float vert=0;
		float widt=0;
		float heig=0;
			
		//test
		/*print ("screen: "+screenSize[0]+" x "+screenSize[1]);
		print ("texture: "+(ratio*size*screenSize[0])+"x"+(size*screenSize[0]));
		print ("ratio: "+ratio);
		print ("X: "+(screenSize[0]*h));
		print ("Y: "+(screenSize[1]*v));
		print ("W: "+((ratio*size*screenSize[0])));
		print ("H: "+((size*screenSize[0])));
		print ("---------------------------------");*/
		
		switch(axis)
		{
			//test
						
			case 'w': case 'W':								
				widt=size*screenSize[0];
				heig=((1/ratio)*size*screenSize[0]);
				if(h==-1)
				{
					horiz=(screenSize[0]-widt)/2;
				}
				else
				{
					horiz=screenSize[0]*h;
				}
				if(v==-1)
				{
					vert=(screenSize[1]-heig)/2;
				}
				else
				{
					vert=screenSize[1]*v;//vert=((1/ratio)*size*screenSize[0]);
				}
				//GUI.Box (new Rect (screenSize[0]*h,screenSize[1]*v,(size*screenSize[0]),((1/ratio)*size*screenSize[0])),pic);
				break;
			case 'h': case 'H':
				widt=ratio*size*screenSize[1];
				heig=(size*screenSize[1]);
				if(h==-1)
				{
					horiz=(screenSize[0]-widt)/2;
				}
				else
				{
					horiz=screenSize[0]*h;
				}
				if(v==-1)
				{
					vert=(screenSize[1]-heig)/2;
				}
				else
				{
					vert=screenSize[1]*v;//vert=((1/ratio)*size*screenSize[0]);				
				}
				//pic.Resize((int)(ratio*size*screenSize[1]),(int)(size*screenSize[1]));//,TextureFormat.ARGB32,true);
				//pic.Apply();
				break;
			default:
				break;
		}
		
		//print(widt+" X "+heig+" box at "+horiz+","+vert);
		
		//GUI.Box (new Rect (horiz,vert,widt,heig),pic);
		GUI.DrawTexture (new Rect (horiz, vert, widt, heig), pic, ScaleMode.StretchToFill);
		//GUI.DrawTexture (new Rect (horiz, vert, (float)widt, (float)heig), pic, ScaleMode.StretchToFill);
		
		
	}

	void OnGUI()
	{
		if (status == "active") 
		{
				setGUI();

				if((Input.GetButton("Menu") || Input.GetKey(KeyCode.Return)) && throttle==0)
				{
					HitDaSwitches();
					
					this.GetComponent<Protocol>().addMessage("MainMenu");
					
					//Debug Output
						//print("---------END title menu ");
				}
				else
				{
					if(throttle>0)
					{
						throttle--;

						//Debug Output
							//print(this+" frame lock: "+throttle+" frames");
					}
				}

				/*
				// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
				if (GUI.Button (new Rect (20, 40, 80, 20), "Level 1")) {
						Application.LoadLevel (1);
				}

				// Make the second button.
				if (GUI.Button (new Rect (20, 70, 80, 20), "Level 2")) {
						Application.LoadLevel (2);
				}*/
		}
	}
}
