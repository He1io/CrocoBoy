using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

    public Texture2D fadedOutTexture;
    public float fadeSpeed = 0.8f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    void OnGUI()
    {
        //Fade out/in alpha value using a direction, speed and Time.deltatime to convert the operation to seconds
        alpha += fadeDir * fadeSpeed * Time.deltaTime;

        //Force (clamp) the number between 0-1 
        alpha = Mathf.Clamp01(alpha);

        //Set color of our GUI
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadedOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }

    //OnLevelWasLoaded is called when a level is loaded. It takes loaded level index
    void OnLevelWasLoaded()
    {
        BeginFade(-1);
    }
}
