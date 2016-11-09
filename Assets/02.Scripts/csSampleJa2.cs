using UnityEngine;
using System.Collections;

public class csSampleJa2 : MonoBehaviour
{
    public GameObject point;

    Transform obj;

    float speed = 0.0f;
    int tempA = 0, tempB = 0;

    // Update is called once per frame
    void Update()
    {
        /* 물체의 움직임 연습 */
        if (csMain.check && !csMain.player && csMain.r_Ja)
        {
            obj = GameObject.Find("(" + csPointSample.moveA + "," + csPointSample.moveB + ")").transform;
            speed += Time.deltaTime * 5.0f;
            transform.position = Vector3.Lerp(transform.position, obj.position, speed);

            if (transform.position == obj.position)
            {
                csMain.coordialtes[tempA, tempB] = false;
                csMain.coordialtes[csPointSample.moveA, csPointSample.moveB] = true;
                csMain.player = true;
                csMain.check = false;
                csPointSample.moveA = 0;
                csPointSample.moveB = 0;
                speed = 0.0f;
                csMain.r_Ja = false;
            }
        }
    }

    void OnMouseDown()
    {
        if (!csMain.player)
        {
            csMain.r_Sang = false;
            csMain.r_Jang = false;
            csMain.r_Ja = true;
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

            if (tempA - 1 > -1  && !csMain.coordialtes[tempA-1, tempB])
            {
                Instantiate(point,
                       GameObject.Find("(" + (tempA-1) + "," + tempB + ")").transform.position - Vector3.forward * (-0.26f),
                       GameObject.Find("(" + (tempA-1) + "," + tempB + ")").transform.rotation);
            }
        }
    }
}
