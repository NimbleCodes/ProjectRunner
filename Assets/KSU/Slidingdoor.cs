using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slidingdoor : MonoBehaviour
{
    bool flag;
    public GameObject door;

    void Start()
    {
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == true)
        {
            if (door.transform.position.x >= -1f)
            {
                door.transform.Translate(-0.05f, 0, 0);
            }
        }
        if (flag == false)
        {
            if (door.transform.position.x < -2f)
            {
                door.transform.Translate(0.05f, 0, 0);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        flag = true;
    }
    private void OnTriggerExit(Collider other)
    {
        flag = false;

    }
}
