using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    [SerializeField]
    private Transform m_gameManager;
    [SerializeField]
    private Transform m_HudPlayer;

    [SerializeField]
    private Lose m_loseScript;

    private GameManager m_gameManagerComponent;
    public GameObject SoundCollectible;

    void Awake()
    {
        PlayerPrefs.SetInt("Titem", 0);
        m_gameManagerComponent = m_gameManager.GetComponent<GameManager>();
        m_loseScript = m_HudPlayer.GetComponent<Lose>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Collectible")
        {
            PlayerPrefs.SetInt("Titem", PlayerPrefs.GetInt("Titem")+1);
            m_gameManagerComponent.UpdateLevel();
            if (SoundCollectible != null)
                SoundCollectible.GetComponent<AudioSource>().Play();
        }
        else if (collision.tag == "Limits")
            m_loseScript.YouLose = true;
    }
}
