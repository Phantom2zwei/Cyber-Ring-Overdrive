using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

    public Transform m_beginLaser;
    public Transform m_endLaser;

    private Transform m_trans;
    private LineRenderer m_lineRenderer;

    void Awake()
    {
        m_trans = transform;
        m_lineRenderer = m_trans.GetComponent<LineRenderer>();
    }

	void Update () {

        m_lineRenderer.SetPosition(0, m_beginLaser.position);
        m_lineRenderer.SetPosition(1, m_endLaser.position);
	}
}
