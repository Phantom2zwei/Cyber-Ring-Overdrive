using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class ManagerButtonMainMenu : MonoBehaviour
{
    [SerializeField]
    private Leaderboard leaderboard;

    public List<GameObject> Menu;
    public List<GameObject> SelectedOption;
    public List<GameObject> Audio;

    private GameObject MainCamera;

    public void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Audio[0].GetComponent<AudioSource>().Play();
        PlayerPrefs.SetString("Music", SelectedOption[0].GetComponent<Text>().text);
    }

    public void Play()
    {
        PlayerPrefs.SetString("Music", SelectedOption[0].GetComponent<Text>().text);
        if (PlayerPrefs.GetString("Music") == "On")
            Audio[1].GetComponent<AudioSource>().Play();
        Menu[0].SetActive(false);
        Menu[3].SetActive(true);
        MainCamera.transform.Translate(100,100,100);
    }

    public void StartTheGame(GameObject text)
    {
        text.GetComponent<Text>().text = "Loading";
        Application.LoadLevel(1);
    }

    public void Options()
    {
        if (PlayerPrefs.GetString("Music") == "On")
            Audio[1].GetComponent<AudioSource>().Play();
        MainCamera.transform.Rotate(0, 90, 0);
        Menu[0].SetActive(false);
        Menu[1].SetActive(true);
    }

    public void PlayMusic()
    {
        if (SelectedOption[0].GetComponent<Text>().text == "On")
        {
            for (int i = 0; i < Audio.Capacity; i++)
                Audio[i].SetActive(true);
            Audio[0].GetComponent<AudioSource>().Play();
        }
        else
        {
            Audio[0].GetComponent<AudioSource>().Stop();
            for (int i = 0; i < Audio.Capacity; i++)
                Audio[i].SetActive(false);
        }
        PlayerPrefs.SetString("Music", SelectedOption[0].GetComponent<Text>().text);
    }

    public void ReturnOptions()
    {
        if (PlayerPrefs.GetString("Music") == "On")
            Audio[2].GetComponent<AudioSource>().Play();
        MainCamera.transform.Rotate(0, -90, 0);
        Menu[0].SetActive(true);
        Menu[1].SetActive(false);
    }

    public void Rank()
    {
        if (PlayerPrefs.GetString("Music") == "On")
            Audio[1].GetComponent<AudioSource>().Play();
        MainCamera.transform.Rotate(0, 90, 0);
        Menu[0].SetActive(false);
        Menu[2].SetActive(true);
        leaderboard.transform.gameObject.SetActive(true);
    }

    public void ReturnRank()
    {
        if (PlayerPrefs.GetString("Music") == "On")
            Audio[2].GetComponent<AudioSource>().Play();
        MainCamera.transform.Rotate(0, -90, 0);
        Menu[0].SetActive(true);
        Menu[2].SetActive(false);
        leaderboard.transform.gameObject.SetActive(false);
    }

    public void change(GameObject text)
    {
        if (text.GetComponent<Text>().text == "On")
            text.GetComponent<Text>().text = "Off";
        else
            text.GetComponent<Text>().text = "On";
    }

    public void ReturnToMainMenu()
    {
        Application.LoadLevel(0);
    }

    public void Quit()
    {
        if (PlayerPrefs.GetString("Music") == "On")
            Audio[2].GetComponent<AudioSource>().Play();
        Application.Quit();
    }
}