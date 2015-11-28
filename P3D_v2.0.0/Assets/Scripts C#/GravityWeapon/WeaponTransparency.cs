using UnityEngine;
using System.Collections;

public class WeaponTransparency : MonoBehaviour {

    private Transform m_trans;
    private MeshRenderer m_rendererComponent;

    void Awake()
    {
        m_trans = transform;
        m_rendererComponent = m_trans.GetComponent<MeshRenderer>();
        m_rendererComponent.material.SetColor("_Color", Color.clear);
    }
}
