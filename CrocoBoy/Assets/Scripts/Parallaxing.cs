using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {


	public Transform[] backgrounds;					//Only objects we want to parallax
	private float[] parallaxScales;					//The proportion of the camera's movement to move the BG by
	public float smoothing = 1f;					//How smooth the parallax is going to be. Make sure to set this above 0

	private Transform cam;							//Reference to de main cameras transfrom
	private Vector3 previousCamPosition;			//The position of the camera in the previous frame

	//It's called before Start(). Great for references.
	void Awake () {
		//Set up the camera reference
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		//Store the previous frame (It had the current frame's camera position)
		previousCamPosition = cam.position;

		//Asigning coresponding parallaxScales
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales [i] = backgrounds [i].position.z*-1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//For each background
		for (int i = 0;i < backgrounds.Length; i++) {
			//The parallax is the opposite of the camera movement because the previous frame multiplied by the scale
			float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];
		
			//Set a target x position which is the current position plus the parallax
			float backgroundTargetPositionX = backgrounds[i].position.x + parallax;
		
			//Create a target position which is the backgrounds current position ehich ir's target x position
			Vector3 backgroundTargetPosition = new Vector3 (backgroundTargetPositionX, backgrounds[i].position.y, backgrounds[i].position.z);
		
			//Fade between current position and the target position using lerp
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPosition, smoothing * Time.deltaTime);
		}

		//Set the previousCamPos to the camera's position at the end of the frame
		previousCamPosition = cam.position;
	}
}
