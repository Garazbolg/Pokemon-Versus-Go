/*

Previous Attempt at using the Built-in gyroscope, But didn't work correctly, there was a significant drift
If you find a way to fix that, we could dump the Goggle VR Lib and maybe export to Windows Phone (BTW thats probably why the app doesn't exist on Windows phone)

*/

using UnityEngine;
using System.Collections;

public class GyroCamera : MonoBehaviour {

    Vector3 euler;

    public GameObject taret;

    float rate;
    bool got;

    // Use this for initialization
    void Start () {
        Input.gyro.enabled = true;
        Input.compensateSensors = false;
        //Input.gyro.updateInterval = 0.06f;
    }
	
	// Update is called once per frame
	void Update () {

        transform.localRotation = new Quaternion(-Input.gyro.attitude.x, -Input.gyro.attitude.y, Input.gyro.attitude.z, Input.gyro.attitude.w);

    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 500, 20), "Attitude : " + Input.gyro.attitude.ToString());
        GUI.Label(new Rect(10, 50, 500, 20), "transfor : " + transform.localRotation.ToString());
        GUI.Label(new Rect(10, 75, 500, 20), "Gtransfo : " + transform.rotation.ToString());
    }
}
