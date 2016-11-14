using UnityEngine;
using System.Collections;

public class csDeadg_Piece : MonoBehaviour
{
    public GameObject point;

    void OnMouseDown()
    {
        if (csMain.player)
        {
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
