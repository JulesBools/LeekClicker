using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBuildingScreen : MonoBehaviour
{

    public Transform main;
    public Transform down;
    public float speed;
    public int screen;


    // Update is called once per frame
    void Update()
    {

        screen = GameObject.Find("GameManager").GetComponent<ClickScript>().activeScreen;

        if (screen == 1)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, main.position, step);
        }

        if (screen == 0)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, down.position, step);
        }
    }
}
