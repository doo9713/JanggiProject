using UnityEngine;
using System.Collections;

public class csDeadr_Piece : MonoBehaviour
{
    public GameObject point;
    public GameObject use;
    public AudioSource _audio;

    int tempA = 10, tempB = 10;
    int destA = 0, destB = 0;

    public void Start()
    {
        _audio = Instantiate(_audio);
    }

    void Update()
    {
        if (csMain.move && !csMain.player && (csMain.realmove == GameObject.Find("(" + tempA + "," + tempB + ")").transform.position))
        {
            destA = csPoint.moveA;
            destB = csPoint.moveB;

            _audio.Play();

            Instantiate(use,
                GameObject.Find("(" + destA + "," + destB + ")").transform.position,   
                Quaternion.Euler(0, 0, 180));

            Destroy(gameObject);

            csMain.r_coordinates[destA, destB] = true;
            csMain.player = true;
            csMain.move = false;
            csMain.realmove = Vector3.zero;
            tempA = 10;
            tempB = 10;
        }
    }

    void OnMouseDown()
    {
        if (!csMain.player)
        {
            csMain.realmove = transform.position;

            /* 생성되었던 포인트 제거 */
            var clones = GameObject.FindGameObjectsWithTag("clone");
            foreach (var clone in clones)
                Destroy(clone);

            /* 게임 오브젝트의 위치 검색 */
            for (int i = 1; i < 4; i++)
            {
                if (transform.position == GameObject.Find("(" + i + ",-1)").transform.position)
                {
                    tempA = i;
                    tempB = -1;
                }
                    
            }

            /* 움직일수 있는 좌표 표시 */
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
