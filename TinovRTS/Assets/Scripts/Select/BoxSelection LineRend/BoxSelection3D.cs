using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelection3D : MonoBehaviour
{
    
    Vector3 startPos;
    Vector3 endPos;


    //kod za line randerer
    //https://www.youtube.com/watch?v=FC3wmwlVKcs
    public GameObject lineGeneratorPrefab;

    public LayerMask mask;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, mask))
                {
                    startPos = hit.point;
                    //startPos.y = 1f;


                }
            }
            if (Input.GetMouseButtonUp(0))
            {

                ClearAllLines();

                GameObject newLineGen;
                LineRenderer lineRend;

                newLineGen = Instantiate(lineGeneratorPrefab);
                lineRend = newLineGen.GetComponent<LineRenderer>();


                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, mask))
                {
                    endPos = hit.point;
                    //endPos.y = 1f;

                    //pocetak
                    lineRend.SetPosition(0, startPos);

                    lineRend.SetPosition(1, new Vector3(startPos.x, endPos.y, endPos.z));

                    //nasuprot pocetku
                    lineRend.SetPosition(2, endPos);

                    lineRend.SetPosition(3, new Vector3(endPos.x, endPos.y, startPos.z));

                    

                    //nakon 2 sekunde unistimo
                    Destroy(lineRend, 2);

                }
            }
            if (Input.GetMouseButton(0))
            {
                ClearAllLines();

                GameObject newLineGen;
                LineRenderer lineRend;
            
                newLineGen = Instantiate(lineGeneratorPrefab);
                lineRend = newLineGen.GetComponent<LineRenderer>();


                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, mask))
                {
                    endPos = hit.point;
                    //endPos.y = 1f;

                    //pocetak
                    lineRend.SetPosition(0, startPos);

                    lineRend.SetPosition(1, new Vector3(startPos.x, endPos.y, endPos.z));

                    //nasuprot pocetku
                    lineRend.SetPosition(2, endPos);

                    lineRend.SetPosition(3, new Vector3(endPos.x, endPos.y, startPos.z));

                    //odma unistimo
                    //Destroy(lineRend, 2);
                }



            }

        }
    }

    void ClearAllLines()
    {
        GameObject[] allLines = GameObject.FindGameObjectsWithTag("LineRendTag");
        foreach(GameObject obj in allLines)
        {
            Destroy(obj);
        }
    }
}
