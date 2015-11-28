using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

    public int Score { set { m_score = value; } get { return m_score; } }

    private Transform m_trans;
    private Transform m_parent;

    [SerializeField]
    private int m_score;

    void Awake ()
    {
        m_trans = transform;
        m_parent = null;
    }

	void Update () 
    {
        if (m_parent)
            UpdatePosition();
	}

    public void SetParentposition(Transform parent)
    {
        m_parent = parent;
    }

    void UpdatePosition()
    {
        m_trans.position = m_parent.position;
    }
}
