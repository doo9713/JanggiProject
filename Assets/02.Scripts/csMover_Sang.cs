using UnityEngine;
using System.Collections;

public class csMover_Sang : MonoBehaviour
{
    public GameObject point;
    public GameObject pointkill;

    Transform obj;

    float speed = 0.0f;
    int tempA = 0, tempB = 0;
    int destA = 0, destB = 0;

    // Update is called once per frame
    void Update()
    {
        if (csMain.check && !csMain.player && csMain.r_Sang)
        {
            if (csMain.eat)
            {
                destA = csPointKillSample.moveA;
                destB = csPointKillSample.moveB;
            }
            else
            {
                destA = csPointSample.moveA;
                destB = csPointSample.moveB;
            }
            obj = GameObject.Find("(" + destA + "," + destB + ")").transform;
            speed += Time.deltaTime * 5.0f;
            transform.position = Vector3.Lerp(transform.position, obj.position, speed);

            if (transform.position == obj.position)
            {
                csMain.r_coordinates[tempA, tempB] = false;
                csMain.r_coordinates[destA, destB] = true;
                csMain.player = true;
                csMain.check = false;
                destA = 0;
                destB = 0;
                speed = 0.0f;
                csMain.r_Sang = false;
            }
        }
    }

    void OnMouseDown()
    {
        if (!csMain.player)
        {
            csMain.r_Ja = false;
            csMain.r_Jang = false;
            csMain.r_Sang = true;
            csMain.r_Wang = false;

            var clones = GameObject.FindGameObjectsWithTag("clone");
            foreach (var clone in clones)
                Destroy(clone);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                    if (transform.position == GameObject.Find("(" + i + "," + j + ")").transform.position)
                    {
                        tempA = i;
                        tempB = j;
                    }
            }

            for (int i = -1; i < 2; i += 2)
            {
                if ((tempA + i > -1 && tempA + i < 4) && (tempB + i > -1 && tempB + i < 3) && !csMain.r_coordinates[tempA + i, tempB + i])
                {
                    if (!csMain.g_coordinates[tempA + i, tempB + i])
                        Instantiate(point,
                            GameObject.Find("(" + (tempA + i) + "," + (tempB + i) + ")").transform.position + Vector3.forward * 0.26f,
                            GameObject.Find("(" + (tempA + i) + "," + (tempB + i) + ")").transform.rotation);
                    else
                        Instantiate(pointkill,
                            GameObject.Find("(" + (tempA + i) + "," + (tempB + i) + ")").transform.position - Vector3.forward * 0.26f,
                            GameObject.Find("(" + (tempA + i) + "," + (tempB + i) + ")").transform.rotation);
                }

                if ((tempA - i > -1 && tempA - i < 4) && (tempB + i > -1 && tempB + i < 3) && !csMain.r_coordinates[tempA - i, tempB + i])
                {
                    if (!csMain.g_coordinates[tempA - i, tempB + i])
                        Instantiate(point,
                            GameObject.Find("(" + (tempA - i) + "," + (tempB + i) + ")").transform.position + Vector3.forward * 0.26f,
                            GameObject.Find("(" + (tempA - i) + "," + (tempB + i) + ")").transform.rotation);
                    else
                        Instantiate(pointkill,
                            GameObject.Find("(" + (tempA - i) + "," + (tempB + i) + ")").transform.position - Vector3.forward * 0.26f,
                            GameObject.Find("(" + (tempA - i) + "," + (tempB + i) + ")").transform.rotation);
                }
            }
        }
    }
}