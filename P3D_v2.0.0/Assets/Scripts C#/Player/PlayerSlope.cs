using UnityEngine;
using System.Collections;

public class PlayerSlope : MonoBehaviour
{
    [SerializeField]
    private float angleLimit = 15f;

    [SerializeField]
    private float speedDividerSlope = 6;

    private Rigidbody rigidBody;
    private Vector3 normal;
    private Vector3 slopeForward;
    private float groundAngle;

	void Start ()
    {
        groundAngle = 0;
        normal = new Vector3(0, 0, 0);
        slopeForward = new Vector3(0, 0, 0);
	    rigidBody = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        if (groundAngle >= angleLimit)
        {
            Vector3 tmpDirectionSlope = transform.up - normal;
            Vector3 tmpOrientationSlope = Vector3.Cross(slopeForward, normal);

            if(tmpDirectionSlope.x < 0)
                rigidBody.AddForce((-tmpOrientationSlope * groundAngle) / speedDividerSlope, ForceMode.Force);
            else
                rigidBody.AddForce((tmpOrientationSlope * groundAngle) / speedDividerSlope, ForceMode.Force);
        }
	}

    void OnCollisionStay(Collision collision)
    {
        slopeForward = collision.transform.forward;
        normal = collision.contacts[0].normal;

        groundAngle = (Mathf.Acos(Mathf.Clamp(normal.y, -1f, 1f))) * Mathf.Rad2Deg;

        Debug.DrawLine(transform.up, normal);
    }

    void OnCollisionExit(Collision collision)
    {
        groundAngle = 0;
    }
}
