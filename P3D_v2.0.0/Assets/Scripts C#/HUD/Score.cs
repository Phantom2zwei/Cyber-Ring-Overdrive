using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    [SerializeField]
    private int score = 0;

    public bool NewRecord
    { get { return newRecord; } set { newRecord = value; } }

    public int ActualScore
    { get { return score; } set { score = value; }}

    public Leaderboard LeaderboardLose
    { get { return leaderboard; } private set { leaderboard = value; } }

    private bool newRecord = false;
    private NumberToNumberTexture numberConverter;
    private SaveLoadFromFile FileMgr;
    Leaderboard leaderboard;
    private int index = -1;

    void Start()
    {
        numberConverter = GetComponent<NumberToNumberTexture>();

        leaderboard = GetComponent<Leaderboard>();
        leaderboard.enabled = false;

        FileMgr = GetComponent<SaveLoadFromFile>();
        LoadRecords();
    }

    void OnGUI()
    {
        numberConverter.Draw(new Rect(15 * (Screen.width / 100), 45 * (Screen.height / 50), Screen.width / 11, Screen.height / 7), score.ToString("0000000"), 100);
    }

    public void EnterNewRecord(string name)
    {
        if (index == -1)
            return;

        List<string> newRankingName = new List<string>();
        List<string> newRaningScore = new List<string>();

        int indexRank = 0;
        for (int i = 0; indexRank < leaderboard.RankingName.Count; i++)
        {
            if (i == index)
            {
                newRankingName.Add(name);
                newRaningScore.Add(score.ToString());
            }
            else
            {
                newRankingName.Add(leaderboard.RankingName[indexRank]);
                newRaningScore.Add(leaderboard.RankingScore[indexRank]);
                indexRank++;
            }
        }
        leaderboard.RankingName = newRankingName;
        leaderboard.RankingScore = newRaningScore;
        FileMgr.Write("Leaderboard.txt", leaderboard.RankingName, leaderboard.RankingScore);
        index = -1;
        score = 0;
    }

    public bool IsNewRecord()
    {
        if (leaderboard.RankingScore.Count == 0)
            return true;

        int indexMax;
        if (leaderboard.RankingName.Count > leaderboard.MaxScoreInBoard)
            indexMax = leaderboard.MaxScoreInBoard;
        else
            indexMax = leaderboard.RankingName.Count;

        for (int i = 0; i < indexMax; i++)
        {
            if (score > int.Parse(leaderboard.RankingScore[i]))
            {
                index = i;
                return true;
            }
        }
        return false;
    }

    public void LoadRecords()
    {
        FileMgr.Load("Leaderboard.txt", leaderboard.RankingName, leaderboard.RankingScore);
    }
}
