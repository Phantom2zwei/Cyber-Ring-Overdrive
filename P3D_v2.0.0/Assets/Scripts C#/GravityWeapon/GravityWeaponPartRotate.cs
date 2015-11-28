using UnityEngine;
using System.Collections;

public class GravityWeaponPartRotate : MonoBehaviour {

    [SerializeField]
    private Vector3 m_rotationAxe;
    [SerializeField]
    private float m_rotationSpeed;

    private Transform m_trans;

    void Awake()
    {
        m_trans = transform;
    }

    void Update()
    {
            m_trans.Rotate(m_rotationAxe * m_rotationSpeed * Time.deltaTime);
    }
}
