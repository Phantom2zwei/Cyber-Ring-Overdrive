using UnityEngine;
using System.Collections;

public class Lose : MonoBehaviour
{
    [SerializeField]
    private Transform ImageLose;
    [SerializeField]
    private Transform EnterNewRecord;

    public bool YouLose
    { get { return lose; } set { lose = value; } }

    private bool lose = false;
    private Score scoreScript;
    public GameObject ManagerGUI;

	void Start ()
    {
        EnterNewRecord.gameObject.SetActive(false);
        scoreScript = GetComponent<Score>();
        ImageLose.gameObject.SetActive(false);
	}
	
	void Update ()
    {
        if (lose)
        {
            ManagerGUI.SetActive(true);
            ImageLose.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PlayerPrefs.SetInt("Pause",1);
            Time.timeScale = 0;
        }
	}

    void OnGUI()
    {
        if (!lose)
            return;

        if (scoreScript.IsNewRecord())
            EnterNewRecord.gameObject.SetActive(true);
        else
            scoreScript.LeaderboardLose.enabled = true;
    }
}