using UnityEngine;
using System.Collections;

public class RingManager : MonoBehaviour
{

    [SerializeField]
    private bool m_rotation;
    [SerializeField]
    private float m_rotationSpeed;
    [SerializeField]
    private float m_speedMinRotation;
    [SerializeField]
    private float m_speedMaxRotation;

    private Vector3 m_rotationAxe;
    private Transform m_trans;

    void Awake()
    {
        m_trans = transform;
        m_rotationAxe = m_trans.forward;
        m_rotationSpeed = Random.Range(m_speedMinRotation, m_speedMaxRotation);
    }

    void Update()
    {
        if (m_rotation)
            m_trans.Rotate(m_rotationAxe * m_rotationSpeed * Time.deltaTime);
    }

    public void UpdateRing()
    {
        ChangeSpeed();
        ChangeRotation();
    }

    void ChangeSpeed()
    {
        Random.Range(m_speedMinRotation, m_speedMaxRotation + 1);
    }

    void ChangeRotation()
    {
        float tmpvalue = Random.Range(0, 2);
        if (tmpvalue == 0)
            m_rotationAxe *= -1;
        else
            m_rotationAxe *= 1;
    }
}
