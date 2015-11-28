using UnityEngine;
using System.Collections;

public class PlatformsManager : MonoBehaviour
{
    [SerializeField]
    private bool m_forceUpdatePlatformsManager;

    private Transform m_trans;

    void Awake()
    {
        m_trans = transform;
        m_forceUpdatePlatformsManager = false;
    }

    void Update()
    {
        if (m_forceUpdatePlatformsManager)
        {
            UpdatePlatformsManager();
            m_forceUpdatePlatformsManager = false;
        }
    }

    public void UpdatePlatformsManager()
    {
        foreach (Transform child in m_trans)
        {
            PlatformUpdatePosition tmpPlatformUpdatePositionComponent = child.GetComponent<PlatformUpdatePosition>();
            if(tmpPlatformUpdatePositionComponent)
                tmpPlatformUpdatePositionComponent.UpdatePosition();
        }
    }
}
