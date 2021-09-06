using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClickScreen : MonoBehaviour
{
    public Transform main;
    public Transform up;
    public float speed;
    public int screen;

    // Update is called once per frame
    void Update()
    {

        screen = GameObject.Find("GameManager").GetComponent<ClickScript>().activeScreen;

        if (screen == 0)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, main.position, step);
        }
        else if (screen == 1)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, up.position, step);
        }

    }
}
