using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector2 Limit=new Vector2(100,100);



    public float MinZoom = 20f;
    public float MaxZoom = 60f;

    public float OgranicenjeDolje=2f, OgranicenjeGore=35f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("momentalno scroll " + pos.y);

        }
        pos.z += Input.GetAxis("Vertical") * .3f;
        pos.x += Input.GetAxis("Horizontal") * .3f;

        if (OgranicenjeDolje <= pos.y && OgranicenjeGore >= pos.y)
        {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        pos.y += -scroll * 5f;

            //ovu koristim ako zelim da se i kamera zarotira prema objektu (20, 60)

            
            Camera.main.fieldOfView -= scroll * 20;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, MinZoom, MaxZoom);
            

            //ovu koristim ako zelim samo lagano zmirati (2,7)
            //pos.y = Mathf.Clamp(pos.y, MinZoom, MaxZoom);
        }

        


        //limit mi pretstavlja koliko mogu ici lijevo desno na sceni
        pos.x = Mathf.Clamp(pos.x, -Limit.x, Limit.x);
        pos.z = Mathf.Clamp(pos.z, -Limit.y, Limit.y);
        transform.position = pos;
    }
}