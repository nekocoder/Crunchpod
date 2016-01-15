using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
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
	
	//Use this for initialization
		void Start()
		{
			status = "inactive";
			DefThrottle=25;
			throttle=0;
			choice = 1;
			
		}
		
	//This toggles the menu's active status
		public bool HitDaSwitches()
		{
			switch(status)
			{
				case "active":
					status="inactive";
					throttle=0;
					choice=1;
					//Debug Output
						//ShowInfo();
					break;
				case "inactive":				
					status="active";
					throttle=DefThrottle;
					//Debug Output
						//ShowInfo();
					break;
				default:
					break;
			}
			
			return status=="active";
		}
		
	//debugging stats
		public void ShowInfo()
		{
			print("Status: "+status);
			print("Throttle: "+throttle);	
		}
	
	//This processes a choice being made in the menu
		void ProcessChoice()
		{
			switch(choice)
			{
				//Main menu
					case 1:
						HitDaSwitches();
						this.GetComponent<Protocol>().addMessage("unload");
						this.GetComponent<Protocol>().addMessage("MainMenu");
						break;
				//quit application
					case 2:
						Application.Quit();
						break;						
				default:
					break;				
			}
			
		}
	
	//Update is called once per frame
		void Update()
		{
				
		}
		
	//FixedUpdate is called once per physics calculation
		void FixedUpdate()
		{
		
		
		}
	
	//This, in conjunction with PrepAndServe, sets up sizing and positioning for UI elements
		void SetGUI()
		{
			float [] screenSize = new float[2]; //this.GetComponent<MainMachine>().Bounds;
			
			screenSize[0]=Screen.width;
			screenSize[1]=Screen.height;		

			//title		
				PrepAndServe (-1, (float)1 / 6, 'h', (float)1 / 6, logo);
			
			//cursor		
				PrepAndServe ((float)3/8, (float)(choice+4)/ 8, 'h', (float)1 / 8, cursor1);
				PrepAndServe ((float)5/8, (float)(choice+4)/ 8, 'h', (float)1 / 8, cursor2);

			//options				
				PrepAndServe (-1, (float)5 / 8, 'h', (float)1 / 8, logo);
				PrepAndServe (-1, (float)6 / 8, 'h', (float)1 / 8, logo);
			
		}
	
	//Does calculations for where to place UI elements
		void PrepAndServe(float h, float v,char axis, float size, Texture2D pic)
		{
			float [] screenSize = new float[2];		
			screenSize[0]=Screen.width;
			screenSize[1]=Screen.height;
			
			float ratio= (float)pic.width/(float)pic.height;

			float horiz=0;
			float vert=0;
			float widt=0;
			float heig=0;
			
			
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
		
	//Displays UI elements each frame
		void OnGUI()
		{
			if (status == "active") 
			{
					//get size
					float [] screenSize = new float[2]; //this.GetComponent<MainMachine>().Bounds;
					
					////screenSize[0]=Screen.width;
					////screenSize[1]=Screen.height;
					
					//print("bounds "+(screenSize[1]-screenSize[0]));
					
					// Make a background box
					//GUI.Box (new Rect ((screenSize[1]-screenSize[0])/2, 0, screenSize[1]-screenSize[0], screenSize[3]-screenSize[2]), "Crunchpod");
					
					//show logo
					//GUI.backgroundColor = Color.white;				
					//GUI.skin.box.normal.background=logo;
					//GUI.Box (new Rect (screenSize[0]/3, screenSize[1]/5,screenSize[0]/3,screenSize[1]/5), logo);
					
					SetGUI();

					if((Input.GetButton("Chomp")  || Input.GetKey(KeyCode.Return)) && throttle==0)
					{
						ProcessChoice();
					}
					if(Input.GetAxis("Vertical")>0 && throttle==0)
					{
						choice++;
						
						throttle=DefThrottle;
						
						if(choice>2)
						{
							choice=1;
						}
						if(choice<1)
						{
							choice=2;
						}					
					}
					if(Input.GetAxis("Vertical")<0 && throttle==0)
					{
						choice--;
						
						throttle=DefThrottle;
						
						if(choice>2)
						{
							choice=1;
						}
						if(choice<1)
						{
							choice=2;
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