using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlowPan : MonoBehaviour {
    //Move camera smoothly between 2 points (no rotation)
    //More points breaks code (fix later if more points needed)

    public Camera cam;
    public Vector3[] positions;
    public float speed = 1;
    public float easeAmount;

    Transform pos;
    float distance;
    float percent;
    int fromIndex;
    int toIndex;

    void Start () {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>(); //Grab Camera
        //pos1 = cam.transform.position; //Not neccessary
        pos = cam.transform;

 	}
	
	void Update () {
        panCam();
		
	}

    public void panCam() {

        fromIndex %= positions.Length;
        toIndex = (fromIndex + 1);
        distance = Vector3.Distance(positions[fromIndex], positions[toIndex]);
        percent += Time.deltaTime * speed / distance;
        percent = Mathf.Clamp01(percent);
        float easedpercent = Ease(percent);

        pos.transform.position = Vector3.Lerp(positions[0], positions[1], easedpercent); //Move Camera
        //pos.transform.position = Vector3.Lerp(positions[0], positions[1], Time.deltaTime * speed); //Bad way to Move Camera
        cam.transform.position = pos.position; //Actually Move Camera

        if(percent >=1) //Flip direction when at end of position array
        {
            percent = 0;
            Debug.Log("Swapping Camera Direction");
            fromIndex = 0;
            System.Array.Reverse(positions);
        }


    }

    float Ease(float x) //Advanced Easing Math
    {
        float a = easeAmount + 1;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }
}
