using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csTurnText : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (csMain.player)
            text.text = "Player1";
        else
            text.text = "Player2";
    }
}
