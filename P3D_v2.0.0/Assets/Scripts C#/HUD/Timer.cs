using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float timeLeft = 60.0f;

    public float TimeLeft
    { get { return timeLeft; } set { timeLeft = value; } }

    private GUIStyle style;
    private int minutes;
    private int seconds;
    private int current;
    private Lose loseScript;
    private NumberToNumberTexture numberConverter;

	void Start ()
    {
        style = new GUIStyle();
        style.fontSize = Screen.height * 2 / 50;
        style.normal.textColor = Color.black;
        loseScript = GetComponent<Lose>();
        numberConverter = GetComponent<NumberToNumberTexture>();
	}
	
	void Update ()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0)
            loseScript.YouLose = true;
	}

    void OnGUI()
    {
        numberConverter.Draw(new Rect(-5 * (Screen.width / 90), 24 * (Screen.height / 30), Screen.width / 7, Screen.height / 4), ConvertTimeLeft(), 60);
    }

    string ConvertTimeLeft()
    {
        minutes = 0;
        seconds = 0;
        current = 0;

        for (int i = 0; i < timeLeft; i++)
        {
            current++;
            seconds = current;
            if (seconds >= 60)
            {
                minutes++;
                seconds = 0;
                current -= 60;
            }
        }
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}