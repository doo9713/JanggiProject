using UnityEngine;
using System.Collections;

public class csSample : MonoBehaviour
{
    public GameObject point;

    Transform obj;

    float speed = 0.0f;
    int tempA = 0, tempB = 0;

    // Update is called once per frame
    void Update()
    {
        /* 물체의 움직임 연습 */
        if (csMain.check && csMain.player)
        {
            obj = GameObject.Find("(" + csPointSample.moveA + "," + csPointSample.moveB + ")").transform;
            speed += Time.deltaTime * 5.0f;
            transform.position = Vector3.Lerp(transform.position, obj.position, speed);

            if (transform.position == obj.position)
            {
                csMain.coordialtes[tempA, tempB] = false;
                csMain.coordialtes[csPointSample.moveA, csPointSample.moveB] = true;
                csMain.player = false;
                csMain.check = false;
                csPointSample.moveA = 0;
                csPointSample.moveB = 0;
                speed = 0.0f;
            }
        }
    }

    void OnMouseDown()
    {
        if (csMain.player)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                    if (transform.position == GameObject.Find("(" + i + "," + j + ")").transform.position)
                    {
                        tempA = i;
                        tempB = j;
                    }
            }

            for (int i = -1; i < 2; i++)
            {
                if (tempA + i > -1 && tempA + i < 4 && !csMain.coordialtes[tempA + i, tempB])
                {
                    Instantiate(point,
                        GameObject.Find("(" + (tempA + i) + "," + tempB + ")").transform.position - Vector3.forward * (-0.26f),
                        GameObject.Find("(" + (tempA + i) + "," + tempB + ")").transform.rotation);
                }

                if (tempB + i > -1 && tempB + i < 3 && !csMain.coordialtes[tempA, tempB + i])
                {
                    Instantiate(point,
                        GameObject.Find("(" + tempA + "," + (tempB + i) + ")").transform.position - Vector3.forward * (-0.26f),
                        GameObject.Find("(" + tempA + "," + (tempB + i) + ")").transform.rotation);
                }
            }
        }

    }
}
