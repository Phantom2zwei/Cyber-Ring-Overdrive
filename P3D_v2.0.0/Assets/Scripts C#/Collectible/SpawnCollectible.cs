using UnityEngine;
using System.Collections;

public class SpawnCollectible : MonoBehaviour {

    [SerializeField]
    private Transform m_collectibleManager;

    private Transform m_trans;
    private CollectibleManager m_collectibleManagerComponent;

    void Awake()
    {
        m_trans = transform;
    }

	void Start () {
        m_collectibleManagerComponent = m_collectibleManager.GetComponent<CollectibleManager>();
        m_collectibleManagerComponent.AddSpawnPosition(m_trans);
	}
}
