﻿using UnityEngine;
using System.Collections;

public class csPoint : MonoBehaviour
{
    public static int moveA;
    public static int moveB;

    void OnMouseDown()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
                if ((transform.position - Vector3.forward * 0.26f) == GameObject.Find("(" + i + "," + j + ")").transform.position)
                {
                    moveA = i;
                    moveB = j;
                }
        }

        csMain.move = true;
        csMain.eat = false;

        var clones = GameObject.FindGameObjectsWithTag("clone");
        foreach (var clone in clones)
            Destroy(clone);
    }
}
