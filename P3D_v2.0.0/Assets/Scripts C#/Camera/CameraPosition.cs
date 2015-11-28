using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

    [SerializeField]
    private Transform Player;

    private Transform m_trans;

    void Awake()
    {
        m_trans = transform;
        m_trans.position = Player.position;
    }

    void LateUpdate()
    {
        m_trans.position = Player.position;
        m_trans.position += new Vector3(0, 0, 0);
    }
}
