using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectibleManager : MonoBehaviour
{

    [SerializeField]
    private Transform m_collectible;
    [SerializeField]
    private float m_distMinSpaw;
    [SerializeField]
    private float m_timeBonus;
    [SerializeField]
    private Transform m_hudPlayer;

    private List<Transform> t_transformSpawn;
    private Collectible CollectibleComponent;
    private float m_distSpawn;
    private Transform m_currentSpawn;
    private Score m_scoreComponent;
    private Timer m_timerComponent;

    void Awake()
    {
        t_transformSpawn = new List<Transform>();
        CollectibleComponent = m_collectible.GetComponent<Collectible>();
        m_currentSpawn = null;
        m_scoreComponent = m_hudPlayer.GetComponent<Score>();
        m_timerComponent = m_hudPlayer.GetComponent<Timer>();
    }

    public void AddSpawnPosition(Transform transform)
    {
        t_transformSpawn.Add(transform);
    }

    public void UpdatePosition()
    {
        Transform tmpSpawn = null;
        int tmpCount = t_transformSpawn.Count;
        if (tmpCount > 0)
        {
            if (m_currentSpawn)
            {
                UpdateScoreTimer(CollectibleComponent.GetComponent<Collectible>().Score);
                tmpSpawn = t_transformSpawn[Random.Range(0, tmpCount)];
                m_distSpawn = Vector3.Distance(m_currentSpawn.position, tmpSpawn.position);
                while (m_distSpawn < m_distMinSpaw)
                {
                    tmpSpawn = t_transformSpawn[Random.Range(0, tmpCount)];
                    m_distSpawn = Vector3.Distance(m_currentSpawn.position, tmpSpawn.position);
                }
                m_currentSpawn = tmpSpawn;
                CollectibleComponent.SetParentposition(tmpSpawn);
            }
            else
            {
                m_currentSpawn = t_transformSpawn[Random.Range(0, tmpCount)];
                CollectibleComponent.SetParentposition(m_currentSpawn);
            }
        }

    }

    void UpdateScoreTimer(int point)
    {
        if (PlayerPrefs.GetInt("Titem") == 15)
        {
            m_timeBonus -= 0.5f;
            PlayerPrefs.SetInt("Titem", 0);
        }
        m_scoreComponent.ActualScore+= point;
        m_timerComponent.TimeLeft += m_timeBonus; 
    }
}
