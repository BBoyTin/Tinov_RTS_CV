using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.youtube.com/watch?v=vsdIhyLKgjc

public class BoxSelector : MonoBehaviour
{
    public RectTransform selectBoxImage;
    Vector3 startPos;
    Vector3 endPos;
    
    void Start()
    {
        selectBoxImage.gameObject.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100))
            {
                startPos = hit.point;

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectBoxImage.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            
            if (!selectBoxImage.gameObject.activeInHierarchy)
            {
                selectBoxImage.gameObject.SetActive(true);
            }

            endPos = Input.mousePosition;

            Vector3 boxStart = Camera.main.WorldToScreenPoint(startPos);
            //mozda kasnije ide u Y ako trebam mijenjati
            boxStart.z = 0f;

            Vector3 centreOfBox = (boxStart + endPos) / 2f;

            selectBoxImage.position = centreOfBox;

            float sizeX = Mathf.Abs(boxStart.x - endPos.x);
            float sizeY = Mathf.Abs(boxStart.y - endPos.y);

            selectBoxImage.sizeDelta = new Vector2(sizeX, sizeY);
        }

    }
}
