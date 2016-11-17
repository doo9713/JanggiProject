using UnityEngine;
using System.Collections;

public class csMoveg_Sang : MonoBehaviour
{
    public GameObject point;
    public GameObject pointkill;
    public GameObject dead;

    Transform obj;

    float speed = 0.0f;
    int tempA = 0, tempB = 0;
    int destA = 0, destB = 0;

    // Update is called once per frame
    void Update()
    {
        if (csMain.check && csMain.player && csMain.g_Sang)
        {
            if (csMain.eat)
            {
                destA = csPointKill.moveA;
                destB = csPointKill.moveB;
            }
            else
            {
                destA = csPoint.moveA;
                destB = csPoint.moveB;
            }

            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            obj = GameObject.Find("(" + destA + "," + destB + ")").transform;
            speed += Time.deltaTime * 5.0f;
            transform.position = Vector3.Lerp(transform.position, obj.position, speed);

            if (transform.position == obj.position)
            {
                csMain.g_coordinates[tempA, tempB] = false;
                csMain.g_coordinates[destA, destB] = true;
                csMain.player = false;
                csMain.check = false;
                csMain.g_Sang = false;
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
                speed = 0.0f;
            }
        }
    }

    void OnMouseDown()
    {
        if (csMain.player)
        {
            csMain.g_Ja = false;
            csMain.g_Jang = false;
            csMain.g_Sang = true;
            csMain.g_Wang = false;
            csMain.g_Dead = false;

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
                if ((tempA + i > -1 && tempA + i < 4) && (tempB + i > -1 && tempB + i < 3) && !csMain.g_coordinates[tempA + i, tempB + i])
                {
                    if (!csMain.r_coordinates[tempA + i, tempB + i])
                        Instantiate(point,
                            GameObject.Find("(" + (tempA + i) + "," + (tempB + i) + ")").transform.position + Vector3.forward * 0.26f,
                            GameObject.Find("(" + (tempA + i) + "," + (tempB + i) + ")").transform.rotation);
                    else
                        Instantiate(pointkill,
                            GameObject.Find("(" + (tempA + i) + "," + (tempB + i) + ")").transform.position - Vector3.forward * 0.26f,
                            GameObject.Find("(" + (tempA + i) + "," + (tempB + i) + ")").transform.rotation);
                }

                if ((tempA - i > -1 && tempA - i < 4) && (tempB + i > -1 && tempB + i < 3) && !csMain.g_coordinates[tempA - i, tempB + i])
                {
                    if (!csMain.r_coordinates[tempA - i, tempB + i])
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

    void OnTriggerEnter(Collider other)
    {
        if (!gameObject.GetComponent<BoxCollider>().isTrigger)
        {
            int x, y;
            if (csMain.eat)
            {
                x = csPointKill.moveA;
                y = csPointKill.moveB;
            }
            else
            {
                x = csPoint.moveA;
                y = csPoint.moveB;
            }

            if (transform.position == GameObject.Find("(" + x + "," + y + ")").transform.position)
            {
                csMain.g_coordinates[x, y] = false;

                Destroy(gameObject);

                Instantiate(dead,
                    GameObject.Find("(" + (3 - csMain.deadg_Piece) + ",-1)").transform.position,
                    Quaternion.Euler(0, 0, 180));

                csMain.deadg_Piece++;
                if (csMain.deadg_Piece == 3)
                    csMain.deadg_Piece = 0;
            }
        }
    }
}
