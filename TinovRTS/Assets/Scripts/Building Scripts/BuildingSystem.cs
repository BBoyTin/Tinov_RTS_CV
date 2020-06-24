using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    
    public static BuildingSystem instanceOfBuildsingSystem;

    [Header("General")]
    public int money = 1000; 

    public LayerMask mask;

    [Header("UI must stuff")]
    public GameObject textUICanvas;

    public Text moneyTxt;

    public GameObject openShopBttn;


    
    GameObject newUiTextGameObj;
    Text infoText;

    [HideInInspector]
    public BuidlingBluepring buildingBluepring;

    [HideInInspector]
    public GameObject building;
    

    [HideInInspector]
    public GameObject objToMove;

    [HideInInspector]
    public enum CursorState { Building, Rotating }
    [HideInInspector]
    public CursorState state = CursorState.Building;
    [HideInInspector]
    public bool placingObject = false;

    BuildingCollisionDetector buildingColisions;

  

    float LastPosX, LastPosY, LastPosZ;
    Vector3 mousePos;


    private void Awake()
    {
        if (instanceOfBuildsingSystem != null)
        {
            Debug.Log("more than one BuildingSystem in scene");
            return;

        }
        instanceOfBuildsingSystem = this;
    }

    private void Start()
    {
        
        moneyTxt.text = money + " g";
    }


    public void SetBuilding(BuidlingBluepring bp)
    {
        buildingBluepring = bp;
        if(money >= buildingBluepring.cost)
        {
            
            building = buildingBluepring.prefab;


            //Debug.Log("ovdje u SetBilding u BuilidngSystemu se nes dogadja " + buildingBluepring.cost);
           
            placingObject = true;

            money = money - buildingBluepring.cost;
            moneyTxt.text = money+" g" ;

            Debug.Log("ToDo: place value of buildings in shop");

            openShopBttn.SetActive(false);
        }
        else
        {
            Debug.Log("ToDo: not enough money");
        }
        
    }


    //za potencijalnu implementaciju kasnije
    public void ActivateBuildingProcess()
    {
        placingObject = true;
    }

    //fixedUpdate bio prije jer sam mislio da cu neke druge stvari jos ovdje prckati
    void Update()
    {
        if (building == null)
            return;
        if (placingObject == true)
        {
            if (objToMove == null)
            {
                CreateObjToMove();
            }
            if (objToMove != null)
                PlaceObject();
        }
    }

    void CollorObjRed()
    {
        foreach (Renderer r in objToMove.GetComponentsInChildren<Renderer>())
        {
            r.material.color = Color.red;
        }
    }
    void CollorObjGreen()
    {
        foreach (Renderer r in objToMove.GetComponentsInChildren<Renderer>())
        {
            r.material.color = Color.green;
        }
    }


    private void CreateObjToMove()
    {
        objToMove = Instantiate(building, transform.position, Quaternion.identity);
        CollorObjGreen();

        CreateInfoText();


        infoText.text = "press \"space\" to build";
        
        buildingColisions = objToMove.GetComponent<BuildingCollisionDetector>();
        

    }

    private void CreateInfoText()
    {
        newUiTextGameObj = Instantiate(textUICanvas, objToMove.transform);

        infoText = newUiTextGameObj.GetComponentInChildren<Text>();
        if (infoText == null)
            Debug.Log("text element missing here");
    }



    private void PlaceObject()
    {
       
        mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            float PosX = hit.point.x;
            float PosY = hit.point.y;
            float PosZ = hit.point.z;

            if (PosX != LastPosX || PosY != LastPosY || PosZ != LastPosZ)
            {
                LastPosX = PosX;
                LastPosY = PosY;
                LastPosZ = PosZ;
                if(state==CursorState.Building)
                    objToMove.transform.position = new Vector3(LastPosX, LastPosY, LastPosZ);
            }

            //bojanje ako moze ili ne moze se graditi
            if (buildingColisions.isOkeyToPlace)
            {
                CollorObjGreen();
            }
            else
            {
                CollorObjRed();
            }

            if (Input.GetMouseButton(0))
            {

                if (state == CursorState.Rotating)
                {
                    objToMove.transform.LookAt(new Vector3(LastPosX, objToMove.transform.position.y, LastPosZ));
                }
            }

            //umjesto muse up mijenjam sa space

            //if (Input.GetMouseButtonUp(0))
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (state == CursorState.Building)
                {
                    if (buildingColisions.isOkeyToPlace)
                    {
                        state = CursorState.Rotating;
                        infoText.text = "Rotate with mouse";
                        //Debug.Log("now rotate the obj");
                        return;
                    }
                    else
                    {
                        StartCoroutine("TextInfoUpdate");
                        
                        //Debug.Log("cant build here");
                    }
                }
                if (state == CursorState.Rotating)
                {
                    if (buildingColisions.isOkeyToPlace)
                    {

                        //Debug.Log("kreiramo novi objekt");
                        BuildNewObject();
                        state = CursorState.Building;
                    }
                    else
                    {
                        state = CursorState.Building;
                    }
                }


            }


        }
    }


    private void BuildNewObject()
    {
        openShopBttn.SetActive(true);

        GameObject newBuilding = Instantiate(building, objToMove.transform.position, objToMove.transform.rotation);
        Destroy(objToMove);
        placingObject = false;
        Rigidbody rb = newBuilding.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        //rb.isKinematic = true;
        rb.useGravity = false;

        Collider col = newBuilding.GetComponent<Collider>();
        col.isTrigger = false;

        Animator animator = newBuilding.GetComponent<Animator>();

        animator.SetBool("isBuild", true);
        

        //moramo jer inace aganti bjeze kada se gradi
        //VAZNO : prek taga u child objektu trazim da aktiviram navmesh
        foreach(Transform child in newBuilding.transform)
        {
            if (child.tag == "NavMeshObj")
            {
                child.gameObject.SetActive(true);
                //Debug.Log("nasao TAG!!!");
            }
        }

        newBuilding.GetComponent<BuildingInfo>().enabled = true;

        if(newBuilding.GetComponent<BuildingAttack>() != null)
            newBuilding.GetComponent<BuildingAttack>().enabled = true;

        newBuilding.gameObject.tag = "Building";
   
    }

    IEnumerator TextInfoUpdate()
    {
        infoText.text = "Can't build here";
        yield return new WaitForSeconds(1f);
        infoText.text = "press \"space\" to build";
    }


    public void TakeMoney(int amount)
    {
        money = money - amount;
        moneyTxt.text = money + " g";
    }
}
