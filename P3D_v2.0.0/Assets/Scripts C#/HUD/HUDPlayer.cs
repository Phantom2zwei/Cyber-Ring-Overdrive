using UnityEngine;
using System.Collections;

public class HUDPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform m_arrowForwardObjectif;
    [SerializeField]
    private Transform m_player;
    [SerializeField]
    private Transform m_collectible;
    [SerializeField]
    private Transform m_crosshair;
    [SerializeField]
    private float m_SpeedRotationCrosshair;

    public Transform PlayerTrans
    { get { return m_player; } private set { m_player = value; }  }

    private Rect m_position;
    private LaunchGravityWeapon m_launchGravityWeaponComponent;
    private PlayerController m_playerControllerComponent;
    private Material m_crosshairMaterial;

    void Start()
    {
        m_launchGravityWeaponComponent = m_player.GetComponent<LaunchGravityWeapon>();
        m_playerControllerComponent = m_player.GetComponent<PlayerController>();
        m_crosshairMaterial = m_crosshair.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        UpdateOrientationArrowForwardObjectif();
        UpdateColorCrosshair();
        UpdateRotationCrosshair();
    }

    void UpdateOrientationArrowForwardObjectif()
    {
        //Vector3 tmpDirection = m_collectible.position - m_player.position;
        m_arrowForwardObjectif.LookAt(m_collectible.position);
        //m_arrowForwardObjectif.forward = tmpDirection.normalized;
    }

    void UpdateColorCrosshair()
    {
        if (m_launchGravityWeaponComponent.CanLaunch)
            m_crosshairMaterial.SetColor("_Color", Color.green);
        else
            m_crosshairMaterial.SetColor("_Color", Color.red);
    }

    void UpdateRotationCrosshair()
    {
        if (m_playerControllerComponent.IsUsingAttractiveForce)
            m_crosshair.Rotate(new Vector3(0, 1, 0), m_SpeedRotationCrosshair);
    }
}

