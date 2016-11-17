using UnityEngine;
using System.Collections;

public class csDeadg_Piece : MonoBehaviour
{
    public GameObject point;
    public GameObject use;

    int destA = 0, destB = 0;

    void Update()
    {
        if (csMain.check && csMain.player && csMain.g_Dead)
        {
            destA = csPoint.moveA;
            destB = csPoint.moveB;

            Instantiate(use,
                GameObject.Find("(" + destA + "," + destB + ")").transform.position,
                Quaternion.Euler(0, 0, 0));

            Destroy(gameObject);

            csMain.g_coordinates[destA, destB] = true;
            csMain.player = false;
            csMain.check = false;
            csMain.g_Dead = false;
        }
    }

    void OnMouseDown()
    {
        if (csMain.player)
        {
            csMain.g_Ja = false;
            csMain.g_Jang = false;
            csMain.g_Sang = false;
            csMain.g_Wang = false;
            csMain.g_Dead = true;

            var clones = GameObject.FindGameObjectsWithTag("clone");
            foreach (var clone in clones)
                Destroy(clone);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    if (!csMain.g_coordinates[i,j] && !csMain.r_coordinates[i,j])
                    {
                        Instantiate(point,
                           GameObject.Find("(" + i + "," + j + ")").transform.position + Vector3.forward * 0.26f,
                           GameObject.Find("(" + i + "," + j + ")").transform.rotation);
                    }
            }
        }
    }
}
