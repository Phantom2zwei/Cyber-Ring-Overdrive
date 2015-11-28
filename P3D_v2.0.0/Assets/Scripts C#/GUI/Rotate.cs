using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public float X = 0;
    public float Y = 0;
    public float Z = 0;
	
	void Update () 
    {
        this.transform.Rotate(X * Time.deltaTime, Y * Time.deltaTime, Z * Time.deltaTime);
	}
}
