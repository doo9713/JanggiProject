using UnityEngine;
using System.Collections;

public class csDeadr_Piece : MonoBehaviour
{
    public GameObject point;
    public GameObject use;

    int destA = 0, destB = 0;

    void Update()
    {
        if (csMain.check && !csMain.player && csMain.r_Dead)
        {
            destA = csPoint.moveA;
            destB = csPoint.moveB;

            Instantiate(use,
                GameObject.Find("(" + destA + "," + destB + ")").transform.position,   
                Quaternion.Euler(0, 0, 180));

            Destroy(gameObject);

            csMain.r_coordinates[destA, destB] = true;
            csMain.player = true;
            csMain.check = false;
            csMain.r_Dead = false;
        }
    }

    void OnMouseDown()
    {
        if (!csMain.player)
        {
            csMain.r_Ja = false;
            csMain.r_Jang = false;
            csMain.r_Sang = false;
            csMain.r_Wang = false;
            csMain.r_Dead = true;

            var clones = GameObject.FindGameObjectsWithTag("clone");
            foreach (var clone in clones)
                Destroy(clone);

            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                    if (!csMain.g_coordinates[i, j] && !csMain.r_coordinates[i, j])
                    {
                        Instantiate(point,
                           GameObject.Find("(" + i + "," + j + ")").transform.position + Vector3.forward * 0.26f,
                           GameObject.Find("(" + i + "," + j + ")").transform.rotation);
                    }
            }
        }
    }
}
