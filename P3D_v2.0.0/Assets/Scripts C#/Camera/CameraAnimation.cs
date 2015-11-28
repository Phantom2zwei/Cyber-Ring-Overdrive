using UnityEngine;
using System.Collections;

public class CameraAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform m_player;

    private Transform m_trans;
    private PlayerController m_playerControllerComponent;
    private Animator m_animatorControllerComponent;

    void Awake()
    {
        m_trans = transform;
        m_playerControllerComponent = m_player.GetComponent<PlayerController>();
        m_animatorControllerComponent = m_trans.GetComponent<Animator>();
    }

    void LateUpdate()
    {
        m_animatorControllerComponent.SetBool("walkBool", m_playerControllerComponent.IsWalking);
    }
}
