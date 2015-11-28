using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformUpdatePosition : MonoBehaviour
{

    [SerializeField]
    private Transform m_platform;
    [SerializeField]
    private bool m_forceUpdatePosition;
    [SerializeField]
    private List<Transform> t_tranform;

    private Transform m_trans;
    private Vector3 m_newPosition;

    void Awake()
    {
        m_trans = transform;
        m_newPosition = Vector3.zero;
        InitPosition();
        m_forceUpdatePosition = false;
    }

    void Update()
    {
        if(m_forceUpdatePosition)
        {
            UpdatePosition();
            m_forceUpdatePosition = false;
        }
    }

    bool NewPosition()
    {
        int tmpCount = t_tranform.Count;
        if (tmpCount > 0)
        {
            m_newPosition = t_tranform[Random.Range(0, tmpCount)].localPosition;
            return true;
        }
        return false;
    }

    public void UpdatePosition()
    {
        UpdatePositionChildren();
        if (!NewPosition())
            return;
        float clipLength = 0.4f;
        AnimationCurve curve1 = null, curve2 = null, curve3 = null;
        AnimationClip clip = null;
        clip = new AnimationClip();

        curve1 = AnimationCurve.Linear(0, m_platform.localPosition.x, clipLength, m_newPosition.x);
        curve2 = AnimationCurve.Linear(0, m_platform.localPosition.y, clipLength, m_newPosition.y);
        curve3 = AnimationCurve.Linear(0, m_platform.localPosition.z, clipLength, m_newPosition.z);

        clip.legacy = true;
        clip.SetCurve("", typeof(Transform), "localPosition.x", curve1);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curve2);
        clip.SetCurve("", typeof(Transform), "localPosition.z", curve3);
        if (!m_platform.gameObject.GetComponent<Animation>())
            m_platform.gameObject.AddComponent<Animation>();
        m_platform.gameObject.GetComponent<Animation>().AddClip(clip, "AnimationDemo");
        foreach (AnimationState state in m_platform.gameObject.GetComponent<Animation>())
            state.speed = 1;
        m_platform.gameObject.GetComponent<Animation>().Play("AnimationDemo");
    }

    void InitPosition()
    {
        RaycastHit tmpHit;
        if (Physics.Raycast(m_trans.position, -m_trans.up, out tmpHit, 15.0f))
            m_trans.up = tmpHit.normal;
    }

    void UpdatePositionChildren()
    {
        foreach (Transform child in m_trans)
        {
            foreach (Transform childOfChild in child.transform)
            {
                PlatformUpdatePosition tmpPlatformUpdatePositionComponent = childOfChild.GetComponent<PlatformUpdatePosition>();
                if (tmpPlatformUpdatePositionComponent)
                    tmpPlatformUpdatePositionComponent.UpdatePosition();
            }
        }
    }
}
