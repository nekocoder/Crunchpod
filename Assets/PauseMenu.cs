using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {


	//status
	public string status;
	public int DefThrottle;	//how many frames to wait before accepting input
	public int throttle;	//frame counter. used with DefThrottle

	//internal variables
	int choice;

	//media
	public Texture2D logo;
	public Texture2D cursor1;
	public Texture2D cursor2;



	// Use this for initialization
	void Start () 
	{
		status = "inactive";
		DefThrottle=25;
		throttle=0;
		choice = 1;
	}


	
	public bool HitDaSwitches()
	{
		switch(status)
		{
			case "active":
				status="inactive";
				throttle=0;
				choice=1;
				ShowInfo();
				break;
			case "inactive":				
				status="active";
				throttle=DefThrottle;
				ShowInfo();
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
	
	void processChoice()
	{
		switch(choice)
		{
		//start game
			case 1: 
				HitDaSwitches();
				this.GetComponent<Protocol>().addMessage("fresh");				
				break;
		//save/load
			case 2:
				break;
		//options
			case 3:
				break;
		//quit game
			case 4:
				HitDaSwitches();
				this.GetComponent<Protocol>().addMessage("unload");
				this.GetComponent<Protocol>().addMessage("MainMenu");
				break;
		//quit application
			case 5:
				Application.Quit();
				break;
			default:
				break;
		}
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

		//title		
		prepAndServe (-1, (float)1 / 6, 'h', (float)1 / 6, logo);
		
		//cursor		
		prepAndServe ((float)3/8, (float)(choice+2)/ 8, 'h', (float)1 / 8, cursor1);
		prepAndServe ((float)5/8, (float)(choice+2)/ 8, 'h', (float)1 / 8, cursor2);

		//options
		prepAndServe (-1, (float)3 / 8, 'h', (float)1 / 8, logo);
		prepAndServe (-1, (float)4 / 8, 'h', (float)1 / 8, logo);
		prepAndServe (-1, (float)5 / 8, 'h', (float)1 / 8, logo);
		prepAndServe (-1, (float)6 / 8, 'h', (float)1 / 8, logo);
		prepAndServe (-1, (float)7 / 8, 'h', (float)1 / 8, logo);
	
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
				//get size
				float [] screenSize = new float[2]; //this.GetComponent<Protocol>().Bounds;
				
				screenSize[0]=Screen.width;
				screenSize[1]=Screen.height;
				
				//print("bounds "+(screenSize[1]-screenSize[0]));
				
				// Make a background box
				//GUI.Box (new Rect ((screenSize[1]-screenSize[0])/2, 0, screenSize[1]-screenSize[0], screenSize[3]-screenSize[2]), "Crunchpod");
				
				//show logo
				//GUI.backgroundColor = Color.white;				
				//GUI.skin.box.normal.background=logo;
				//GUI.Box (new Rect (screenSize[0]/3, screenSize[1]/5,screenSize[0]/3,screenSize[1]/5), logo);
				setGUI();

				if((Input.GetButton("Chomp")  || Input.GetKey(KeyCode.Return)) && throttle==0)
				{
					/*
					HitDaSwitches();
					
					this.GetComponent<Protocol>().addMessage("fresh");
					print("killing menu");*/
					processChoice();
				}
				if(Input.GetAxis("Vertical")>0 && throttle==0)
				{
					choice++;
					
					throttle=DefThrottle;
					
					if(choice>5)
					{
						choice=1;
					}
					if(choice<1)
					{
						choice=5;
					}					
				}
				if(Input.GetAxis("Vertical")<0 && throttle==0)
				{
					choice--;
					
					throttle=DefThrottle;
					
					if(choice>5)
					{
						choice=1;
					}
					if(choice<1)
					{
						choice=5;
					}				
				}
				else
				{
					if(throttle>0)
					{
						throttle--;
						//print(this+" frame lock: "+throttle+" frames");
					}
				}
				
		}
	}

}
