using UnityEngine;
using System.Collections;

public class LaunchGravityWeapon : MonoBehaviour {

    public Transform GravityWeaponTransform { set { GravityWeapon = value; } get { return GravityWeapon; } }
    public GavityWeapon GravityWeaponComponent { set { m_gravityWeaponComponent = value; } get { return m_gravityWeaponComponent; } }
    public bool CanLaunch { set { m_canLaunch = value; } get { return m_canLaunch; } }

    [SerializeField]
    private Transform m_endLaser;
    [SerializeField]
    private Transform m_beginLaser;
    [SerializeField]
    private Transform m_laser;
    [SerializeField]
    private float m_distance;
    [SerializeField]
    private Transform GravityWeaponTransparency;
    [SerializeField]
    private Transform GravityWeapon;
    private RaycastHit m_Hit;
    private GavityWeapon m_gravityWeaponComponent;
    private Transform m_trans;
    private bool m_canLaunch;
    private PlayerController m_playerControllerCompoenet;

    void Awake()
    {
        m_trans = transform;
        m_gravityWeaponComponent = GravityWeaponTransform.GetComponent<GavityWeapon>();
        m_canLaunch = false;
        m_playerControllerCompoenet = m_trans.GetComponent<PlayerController>();
    }

    public bool TryLaunchGravityWeapon(Vector3 cameraForward)
    {
        if (CanLaunch)
        {
            if (!GravityWeapon.gameObject.activeSelf)
            {
                m_playerControllerCompoenet.SetBoolWeaponIsActive(true);
                GravityWeapon.gameObject.SetActive(true);
            }

            GravityWeapon.position = new Vector3(m_Hit.point.x, m_Hit.point.y, m_Hit.point.z);
            return true;
        }
        else
            return false;
    }

    public void CheckDistance(Vector3 cameraForward)
    {
         if (Physics.Raycast(m_trans.position, cameraForward, out m_Hit, m_distance))
        {
            Debug.DrawLine(m_trans.position, m_Hit.point, Color.red);
            CanLaunch = true;
            //m_laser.gameObject.SetActive(true);
        }
        else
        {
            CanLaunch = false;
            //m_laser.gameObject.SetActive(false);
        }
    }

    public bool CheckDistanceBetweenPlayerAndWeapon()
    {
        m_beginLaser.position = m_trans.position + new Vector3(0, -0.5f, 0);
        m_endLaser.position = GravityWeapon.position;
        float tmpDist = Vector3.Distance(GravityWeapon.position, m_trans.position);
        if (tmpDist < m_distance)
            return true;
        return false;
    }
}
