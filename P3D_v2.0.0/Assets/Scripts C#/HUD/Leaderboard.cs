using UnityEngine;
using System;
using System.Collections.Generic;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private int maxScoreInBoard = 10;

    [SerializeField]
    private Font fontLeaderboard;

    public int MaxScoreInBoard
    { get { return maxScoreInBoard; } set { maxScoreInBoard = value; } }

    public bool OneTime
    { get { return oneTime; } set { oneTime = value; } }

    public List<string> RankingName
    { get { return rankingName; } set { rankingName = value; } }

    public List<string> RankingScore
    { get { return rankingScore; } set { rankingScore = value; } }

    private List<string> rankingName = new List<string>();
    private List<string> rankingScore = new List<string>();
    private SaveLoadFromFile FileMgr;
    private bool oneTime;
    private GUIStyle style;
    private int indexMax;

	void Start ()
    {
        oneTime = false;
        FileMgr = GetComponent<SaveLoadFromFile>();

        style = new GUIStyle();
        style.fontSize = Screen.height * 2 / 35;
        style.normal.textColor = Color.white;
        style.font = fontLeaderboard;
	}
	
	void Update ()
    {
        if (!oneTime)
            LoadAllRecords();
	}

    void OnGUI()
    {
        if (!oneTime)
            return;

        Rect position = new Rect(10 * (Screen.width / 25), 4 * (Screen.height / 20), 100, 100);
        int i = 1;
        for (int index = 0; index < indexMax; index++)
        {
            if (i < 10)
                GUI.Label(position, " " + i + ")" + rankingName[index] + "   " + rankingScore[index], style);
            else if (i == 10)
                GUI.Label(position, i + ")" + rankingName[index] + "   " + rankingScore[index], style);

            position.y += Screen.height / 19;
            i++;
        }
    }

    public void LoadAllRecords()
    {
        oneTime = true;
        FileMgr.Load("Leaderboard.txt", rankingName, rankingScore);

        if (rankingName.Count > maxScoreInBoard)
            indexMax = maxScoreInBoard;
        else
            indexMax = rankingName.Count;
    }
}
