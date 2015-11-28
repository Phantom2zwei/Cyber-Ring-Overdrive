using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform m_collectibleManager;
    [SerializeField]
    private Transform m_platformsManager;
    [SerializeField]
    private bool m_forceUpdateLevel;
    [SerializeField]
    private Transform m_spawnPlayer;
    [SerializeField]
    private Transform m_player;
    [SerializeField]
    private Transform m_ring;

    private PlatformsManager m_platformsManagerComponent;
    private CollectibleManager m_collectibleManagerComponent;
    private RingManager m_ringManagerComponent;

    void Awake()
    {
        m_collectibleManagerComponent = m_collectibleManager.GetComponent<CollectibleManager>();
        m_platformsManagerComponent = m_platformsManager.GetComponent<PlatformsManager>();
        m_ringManagerComponent = m_ring.GetComponent<RingManager>();
        m_forceUpdateLevel = false;
    }

    void Start()
    {
        UpdateLevel();
        SpawnPlayer();
    }

    void Update()
    {
        if (m_forceUpdateLevel)
        {
            UpdateLevel();
            m_forceUpdateLevel = false;
        }
    }

    public void UpdateLevel()
    {
        m_platformsManagerComponent.UpdatePlatformsManager();
        m_collectibleManagerComponent.UpdatePosition();
        m_ringManagerComponent.UpdateRing();
    }

    public void SpawnPlayer()
    {
        m_player.position = m_spawnPlayer.position;
    }
}