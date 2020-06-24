using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraUI : MonoBehaviour
{
    Camera cam;

    public bool constantUpdate = true;
    public float updateTimer = 0.5f;


    private void Start()
    {
        cam = Camera.main;
        if (!constantUpdate)
            StartCoroutine("LookAt");
        
    }
    private void Update()
    {
        if (constantUpdate)
        {
            transform.LookAt(cam.transform);
        }
    }

    IEnumerator LookAt()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateTimer);
            transform.LookAt(cam.transform);
            //Debug.Log("updejtan look at");
        }
        
    }
}
