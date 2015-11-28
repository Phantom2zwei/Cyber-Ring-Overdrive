using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public bool IsWalking { set { m_isWalking = value; } get { return m_isWalking; } }
    public bool IsUsingAttractiveForce { set { m_isUsingAttractiveForce = value; } get { return m_isUsingAttractiveForce; } }

    [SerializeField]
    private Transform m_ring;
    [SerializeField]
    private Transform m_laser;
    [SerializeField]
    private Transform m_cameraX;
    [SerializeField]
    private Transform m_cameraY;
    [SerializeField]
    private float m_forwardSpeed;
    [SerializeField]
    private float m_backwardSpeed;
    [SerializeField]
    private float m_strafeSpeed;
    [SerializeField]
    private float m_jumpVelocity;
    [SerializeField]
    private float m_forwardAirSpeed;
    [SerializeField]
    private float m_backwardAirSpeed;
    [SerializeField]
    private float m_strafeAirSpeed;
    [SerializeField]
    private GavityWeapon m_gravityWeaponComponent;

    private Transform m_trans;
    private Vector3 m_moveDirection;
    private Rigidbody m_rigidbodyComponent;
    private SphereCollider m_sphereColliderComponent;
    private Vector2 m_input;
    private Vector2 m_speedMove;
    private RaycastHit m_hitInfo;
    private bool m_isGrounded;
    private bool m_jump;
    private bool m_jumping;
    private LaunchGravityWeapon m_launchGravityWeaponomponent;
    private bool m_weaponIsActive;
    private Vector3 m_cameraForward;
    private bool m_MouseRightDown;
    private bool m_isWalking;
    private bool m_isUsingAttractiveForce;

    void Awake()
    {
        m_trans = transform;
        m_moveDirection = m_trans.forward;
        m_rigidbodyComponent = GetComponent<Rigidbody>();
        m_sphereColliderComponent = GetComponent<SphereCollider>();
        m_launchGravityWeaponomponent = GetComponent<LaunchGravityWeapon>();
        m_input = Vector2.zero;
        m_speedMove = Vector2.zero;
        m_isGrounded = false;
        m_jump = false;
        m_jumping = false;
        m_weaponIsActive = false;
        m_cameraForward = Vector3.zero;
        m_MouseRightDown = false;
        m_isWalking = false;
        m_isUsingAttractiveForce = false;
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move();
        AttractiveForce();
    }

    public void SetBoolWeaponIsActive(bool isActive)
    {
        m_weaponIsActive = isActive;
    }

    void AttractiveForce()
    {
        if (m_MouseRightDown && m_weaponIsActive && m_launchGravityWeaponomponent.CheckDistanceBetweenPlayerAndWeapon())
        {
            Vector3 tmpDirection = m_launchGravityWeaponomponent.GravityWeaponTransform.position - m_trans.position;
            m_rigidbodyComponent.velocity = tmpDirection.normalized * m_launchGravityWeaponomponent.GravityWeaponComponent.AttractiveForce;
            m_isUsingAttractiveForce = true;
            m_gravityWeaponComponent.SetActiveParticule(true);
            //m_laser.gameObject.SetActive(true);
        }
        else
        {
            m_gravityWeaponComponent.SetActiveParticule(false);
            m_isUsingAttractiveForce = false;
            //m_laser.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Pause") == 0)
        GetInput();
        m_launchGravityWeaponomponent.CheckDistance(m_cameraForward);
        GroundWalk();
        if(!m_isGrounded)
            m_trans.SetParent(null);
    }

    void GetInput()
    {
        m_input.x = Input.GetAxis("Horizontal");
        m_input.y = Input.GetAxis("Vertical");

        if (m_input.x > 0 || m_input.x < 0)
        {
            if (m_isGrounded)
                m_speedMove.x = m_input.x * m_strafeSpeed;
            else
                m_speedMove.x = m_input.x * m_strafeAirSpeed;
        }
        else
            m_speedMove.x = 0;
        if (m_input.y > 0)
        {
            if (m_isGrounded)
                m_speedMove.y = m_input.y * m_forwardSpeed;
            else
                m_speedMove.y = m_input.y * m_forwardAirSpeed;
        }
        else if (m_input.y < 0)
        {
            if (m_isGrounded)
                m_speedMove.y = m_input.y * m_backwardSpeed;
            else
                m_speedMove.y = m_input.y * m_backwardAirSpeed;
        }
        else
            m_speedMove.y = 0;
        if (Input.GetButtonDown("Jump"))
            m_jump = true;
        if (Input.GetMouseButtonDown(0))
            if (m_launchGravityWeaponomponent.TryLaunchGravityWeapon(m_cameraForward))
                m_weaponIsActive = true;
        if (Input.GetMouseButtonDown(1))
            m_MouseRightDown = true;
        else if (Input.GetMouseButtonUp(1))
            m_MouseRightDown = false;
    }

    void Move()
    {
        m_cameraForward = m_cameraX.forward.normalized;
        m_moveDirection = m_cameraForward * m_speedMove.y + m_cameraY.right * m_speedMove.x;
        m_moveDirection = Vector3.ProjectOnPlane(m_moveDirection, Vector3.up);
        m_moveDirection.y = m_rigidbodyComponent.velocity.y;
        Jump();
        m_rigidbodyComponent.velocity = m_moveDirection;
    }

    void Jump()
    {
        if (m_jump && !m_jumping)
        {
            m_jumping = true;
            m_rigidbodyComponent.AddForce(new Vector3(m_moveDirection.x, m_jumpVelocity, m_moveDirection.z), ForceMode.Impulse);
        }
        m_jump = false;
    }

    private void GroundCheck()
    {
        Debug.DrawLine(m_trans.position + new Vector3(m_sphereColliderComponent.radius, 0, 0),
            m_trans.position + new Vector3(m_sphereColliderComponent.radius, -m_sphereColliderComponent.radius, 0));
        Debug.DrawLine(m_trans.position + new Vector3(-m_sphereColliderComponent.radius, 0, 0),
            m_trans.position + new Vector3(-m_sphereColliderComponent.radius, -m_sphereColliderComponent.radius, 0));
        if (/*Physics.SphereCast(m_trans.position, m_sphereColliderComponent.radius + 0.01f, -m_trans.up, out m_hitInfo) ||*/
            Physics.Raycast(m_trans.position + new Vector3(m_sphereColliderComponent.radius, 0, 0), -m_trans.up, m_sphereColliderComponent.radius + 0.01f) ||
            Physics.Raycast(m_trans.position + new Vector3(-m_sphereColliderComponent.radius, 0, 0), -m_trans.up, m_sphereColliderComponent.radius + 0.01f))
            m_isGrounded = true;
        else
            m_isGrounded = false;
        /*if (!m_previouslyGrounded && m_isGrounded && m_jumping)
            m_jumping = false;
        m_previouslyGrounded = m_isGrounded;*/
    }

    void GroundWalk()
    {
        if (!m_MouseRightDown && !m_jump && m_moveDirection != Vector3.zero && m_input != Vector2.zero)
            m_isWalking = true;
        else
            m_isWalking = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].point != Vector3.zero)
        {
            m_isGrounded = true;
            m_trans.SetParent(m_ring); 
            m_jumping = false;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.contacts[0].point != Vector3.zero)
        {
            m_isGrounded = true;
            m_trans.SetParent(m_ring); 
            m_jumping = false;
        }
    }

    void OnCollisionExit()
    {
        m_jumping = true;
    }
}
