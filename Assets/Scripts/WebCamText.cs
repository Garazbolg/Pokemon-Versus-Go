/*

	This script is used to Get the image from the Camera and display it behind the content of the scene
	
	by Garazbolg

*/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class WebCamText : MonoBehaviour {

    public WebCamTexture mCamera = null;
    public Renderer ren;

    // Use this for initialization
    void Start()
    {
        ren = GetComponent<Renderer>();

        mCamera = new WebCamTexture();
		
		/*Should probably find another way to do that
			This works fine for my phone "One Plus X"
			Change it for yours*/
        mCamera.requestedWidth = 854;
        mCamera.requestedHeight = 480;
        ren.material.mainTexture = mCamera;
        mCamera.Play();

    }

    // Update is called once per frame
    void Update()
    {
		/*This allow to only update the inGame camera angle when we received a frame from the device Camera
			So that we don't have a 60 fps image over a 30 fps background
			
			that plus the Motion blur on the in game camera should allow for more consistency between the two*/
        if (mCamera.didUpdateThisFrame)
            GvrHead.shouldUpdateHead = true;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10,10,100,20),""+mCamera.width);
        GUI.Label(new Rect(10, 30, 100, 30), "" + mCamera.height);
    }
}
