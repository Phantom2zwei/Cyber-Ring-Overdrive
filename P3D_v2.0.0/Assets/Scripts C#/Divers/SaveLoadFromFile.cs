using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;  

public class SaveLoadFromFile : MonoBehaviour
{
    public void Write(string fileName, List<string> containerName, List<string> containerScore)
    {
        if (!File.Exists(fileName))
            File.Create(fileName);
        StreamWriter writer = new StreamWriter(fileName);
        for (int i = 0; i < containerName.Count; i++)
        {
            writer.WriteLine(containerName[i] + containerScore[i]);
        }
        writer.Close();
    }

    public void Load(string fileName, List<string> containerName, List<string> containerScore)
    {
        if (!File.Exists(fileName))
            File.Create(fileName);
        containerName.Clear();
        containerScore.Clear();
        string currentLine;
        string namePlayer = "";
        string scorePlayer = "";

        StreamReader theReader = new StreamReader(fileName, Encoding.Default);
        using (theReader)
        {
                do
                {
                    currentLine = theReader.ReadLine();
                    if (currentLine != null)
                    {
                        for (int i = 0; i < currentLine.Length; i++)
                        {
                            if (i < 3)
                                namePlayer += currentLine[i];
                            else
                                scorePlayer += currentLine[i];
                        }
                        containerName.Add(namePlayer);
                        containerScore.Add(scorePlayer);
                        scorePlayer = "";
                        namePlayer = "";
                    }
                }
                while (currentLine != null);
                theReader.Close();
        }
    }
}