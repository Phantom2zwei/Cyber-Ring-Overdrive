using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NumberToNumberTexture : MonoBehaviour
{
    private List<Texture2D> listTextureNumber;

    void Start()
    {
        listTextureNumber = new List<Texture2D>();

        string path = "Number/";
        Texture2D tex = new Texture2D(200, 200);

        for (int i = 0; i < 10; i++ )
        {
            tex = Resources.Load(path + i) as Texture2D;
            listTextureNumber.Add(tex);
        }
        tex = Resources.Load(path + "deuxpetitspoints") as Texture2D;
        listTextureNumber.Add(tex);
    }

    public void Draw(Rect position, string text, int spacing = 60)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] != ':')
                GUI.DrawTexture(position, listTextureNumber[int.Parse(text[i].ToString())]);
            else
                GUI.DrawTexture(position, listTextureNumber[10]);

            position.x += (Screen.width / spacing);
        }
    }
}