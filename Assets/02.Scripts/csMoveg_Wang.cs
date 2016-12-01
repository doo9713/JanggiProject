using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class csMoveg_Wang : MonoBehaviour
{
    public Image g_panel;
    public Image r_panel;
    public GameObject point;
    public GameObject pointkill;

    Transform obj;

    float speed = 0.0f;
    int tempA = 10, tempB = 10;
    int destA = 0, destB = 0;
    bool win_check = false;

    void Start()
    {
        g_panel.gameObject.SetActive(false);
        r_panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (csMain.player && win_check)
            g_panel.gameObject.SetActive(true);

        if (csMain.move && csMain.player && (csMain.realmove == GameObject.Find("(" + tempA + "," + tempB + ")").transform.position))
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
                if (destA == 3)
                    win_check = true;
                csMain.g_coordinates[tempA, tempB] = false;
                csMain.g_coordinates[destA, destB] = true;
                csMain.player = false;
                csMain.move = false;
                csMain.realmove = Vector3.zero;
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
                speed = 0.0f;
                tempA = 10;
                tempB = 10;
            }
        }
    }

    void OnMouseDown()
    {
        if (csMain.player)
        {
            csMain.realmove = transform.position;

            /* 생성되었던 포인트 제거 */
            var clones = GameObject.FindGameObjectsWithTag("clone");
            foreach (var clone in clones)
                Destroy(clone);

            /* 게임 오브젝트의 위치 검색 */
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                    if (transform.position == GameObject.Find("(" + i + "," + j + ")").transform.position)
                    {
                        tempA = i;
                        tempB = j;
                    }
            }

            /* 움직일수 있는 좌표 표시 */
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if ((tempA + i > -1 && tempA + i < 4) && (tempB + j > -1 && tempB + j < 3) && !csMain.g_coordinates[tempA + i, tempB + j])
                    {
                        if (!csMain.r_coordinates[tempA + i, tempB + j])
                            Instantiate(point,
                                GameObject.Find("(" + (tempA + i) + "," + (tempB + j) + ")").transform.position + Vector3.forward * 0.26f,
                                GameObject.Find("(" + (tempA + i) + "," + (tempB + j) + ")").transform.rotation);
                        else
                            Instantiate(pointkill,
                                GameObject.Find("(" + (tempA + i) + "," + (tempB + j) + ")").transform.position - Vector3.forward * 0.26f,
                                GameObject.Find("(" + (tempA + i) + "," + (tempB + j) + ")").transform.rotation);
                    }
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

                r_panel.gameObject.SetActive(true);
            }
        }
    }
}
