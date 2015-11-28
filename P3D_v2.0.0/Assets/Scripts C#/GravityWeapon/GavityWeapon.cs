using UnityEngine;
using System.Collections;

public class GavityWeapon : MonoBehaviour
{

    public float AttractiveForce { set { m_attractiveForce = value; } get { return m_attractiveForce; } }

    public ParticleSystem m_particuleSystemComponent;
    private Transform m_trans;

    [SerializeField]
    private float m_attractiveForce;
    [SerializeField]
    private GameObject m_particule;

    public void SetActiveParticule(bool active)
    {
        m_particuleSystemComponent.enableEmission = active;
    }
}
