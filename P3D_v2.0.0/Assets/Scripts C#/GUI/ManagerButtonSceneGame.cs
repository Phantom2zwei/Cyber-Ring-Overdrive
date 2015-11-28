using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class ManagerButtonSceneGame : MonoBehaviour
{
    [SerializeField]
    private Score scoreScript;

    public List<GameObject> Menu;
    public List<GameObject> AudioAmbient;
    public List<GameObject> AudioOther;

    private GameObject MainCamera;
    private int getSound = 0;

    public void Start()
    {
        if (AudioAmbient.Count != 0 && PlayerPrefs.GetString("Music") == "On")
        {
            getSound = Random.Range(0, AudioAmbient.Count-1);
            AudioAmbient[getSound].GetComponent<AudioSource>().Play();
        }
        Continue();
    }

    public void Update()
    {
        InGame();
        if (AudioAmbient.Count != 0 && PlayerPrefs.GetString("Music") == "On")
            if (!AudioAmbient[getSound].GetComponent<AudioSource>().isPlaying)
            {
                if (AudioAmbient.Count - 1 > 1)
                    rand(AudioAmbient.Count-1);
                AudioAmbient[getSound].GetComponent<AudioSource>().Play();
            }
    }

    private void InGame()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (Menu[0].activeSelf)
                Continue();
            else
            {
                Time.timeScale = 0;
                PlayerPrefs.SetInt("Pause", 1);
                Menu[0].SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        if (PlayerPrefs.GetInt("Pause") == 0 && PlayerPrefs.GetString("Music") == "On")
        {
            if (Input.GetMouseButtonDown(0))
                AudioOther[0].GetComponent<AudioSource>().Play();
            if (Input.GetMouseButtonDown(1))
                AudioOther[1].GetComponent<AudioSource>().Play();
            if (Input.GetMouseButtonUp(1))
                AudioOther[1].GetComponent<AudioSource>().Stop();
        }
    }

    public void GetString(Transform textField)
    {
        string nameEntered = textField.GetComponent<Text>().text;
        if (nameEntered.Length < 3)
        {
            for (int i = nameEntered.Length; i < 3; i++)
            {
                nameEntered += " ";
            }
        }

        scoreScript.EnterNewRecord(nameEntered);
        scoreScript.LoadRecords();
        scoreScript.NewRecord = false;
        textField.parent.parent.gameObject.SetActive(false);
    }

    private void rand(int max)
    {
        int temp = Random.Range(0, max);
        while (temp == getSound)
            temp = Random.Range(0, max);
        getSound = temp;
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("Pause", 0);
        Time.timeScale = 1;
        Menu[0].SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void Quit()
    {
        Application.LoadLevel(0);
    }
}