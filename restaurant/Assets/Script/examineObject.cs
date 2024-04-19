using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Cursor = UnityEngine.Cursor;

public class examineObject : MonoBehaviour
{
    public Transform cam;
    public GameObject offset;
    private PlayerInput playerInput;
    GameObject targetObject;
    private float hitRange = 2f;

    public bool isExaminging = false;
    public GameObject canva;
    //public GameObject tableObject;
    public LayerMask examinedLayer;

    private Vector3 lastMousePoisiton;
    private Transform examinedObject;
    public GameObject CubeCanvas;
    public GameObject SphereCanvas;
    public GameObject ClipBoardCanvas;
    public GameObject boardCanvas;
    public GameObject examineObjectA;
    public GameObject examineObjectB;
    public GameObject examineObjectC;
    public GameObject boardTutorial;

    private Dictionary<Transform, Vector3> originalPosition = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> originalRotation = new Dictionary<Transform, Quaternion>();

    bool isShowDetail = false;
    public static int trailNum = 0;
    public mouseLooking mouselooking;


    public static List<GameObject> firstTimeWatched = new List<GameObject>();

    void Start()
    {
        canva.SetActive(false);
        targetObject = GameObject.Find("Player");
        playerInput = targetObject.GetComponent<PlayerInput>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ray cast form the mouse position , which is locked in the center 
            RaycastHit hit;

            if (Physics.Raycast(cam.position, cam.forward, out hit)) //if raycast hit something 
            {
                if (hit.collider.CompareTag("Object")) //and that thing is having a tag named object
                {

                    ToggleExamination(); //turn isExamining to turn

                    if (isExaminging) //store examinedObject position , rotation
                    {
                        examinedObject = hit.transform;
                        originalPosition[examinedObject] = examinedObject.position;
                        originalRotation[examinedObject] = examinedObject.rotation;

                        isShowDetail = false;
                    }
                }
            }
        }

        RaycastHit hit_;

        if (Physics.Raycast(cam.position, cam.forward, out hit_, hitRange, examinedLayer))
        {
            canva.SetActive(true);
        }
        else if (!Physics.Raycast(cam.position, cam.forward, out hit_, hitRange, examinedLayer))
        {
            canva.SetActive(false);
        }




        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            ToggleShowDetail();

            if (Physics.Raycast(ray, out hit))
            {
                if (isExaminging == true)
                {
                    if (hit.collider.gameObject == examineObjectA) //for example cube is useful item 
                    {
                        checkDetailTrail(CubeCanvas);//show detail of the cube
                    }
                    if (hit.collider.gameObject == examineObjectB) //and shpere is just for display
                    {
                        checkDetailTrail(SphereCanvas);//show detail of the cube
                    }
                    if (hit.collider.gameObject == examineObjectC)
                    {
                        checkDetail(ClipBoardCanvas);
                    }
                    if (hit.collider.gameObject == boardTutorial)
                    {
                        checkDetail(boardCanvas);
                    }
                }
            }
        }
        if (isExaminging == false)
        {
            CubeCanvas.SetActive(false);
            SphereCanvas.SetActive(false);
            ClipBoardCanvas.SetActive(false);
            boardCanvas.SetActive(false);
        }


        if (canva.activeSelf == true)
        {

            if (isExaminging) // if player already press e , toggleExamination , isExamine= true
            {
                Debug.Log("true");
                canva.SetActive(false); // turn off the examine ui
                Examine();
                StartExamination();

            }
            else if (!isExaminging)// if player havent press E , havent toggleExamination , is Examine = false
            {
                canva.SetActive(true); // turn on the canva (only if its close enough , like 2f)
                NonExamine();
                StopExamination();
            }
        }

        if (trailNum >= 3)//if trailNum achieve the specific num 
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("GG");
            }
        }
    }

    public void ToggleShowDetail()
    {
        isShowDetail = !isShowDetail;
    }

    public void ToggleExamination()
    {
        isExaminging = !isExaminging;
    }

    void StartExamination()
    {
        mouselooking.enabled = false;
        Time.timeScale = 0f;
        lastMousePoisiton = Input.mousePosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerInput.enabled = false; //not allow player to move


    }

    void StopExamination()
    {
        mouselooking.enabled = true;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInput.enabled = true;

        if (examinedObject != null)
        {
            // Reset the position and rotation of the examined object
            examinedObject.position = originalPosition[examinedObject];
            examinedObject.rotation = originalRotation[examinedObject];
        }
    }

    void Examine()
    {
        if (examinedObject != null)
        {
            examinedObject.position = Vector3.Lerp(examinedObject.position, offset.transform.position, .2f); //moving the table object closer to our cam

            Vector3 deltaMouse = Input.mousePosition - lastMousePoisiton;
            float rotationSpeed = 1.0f;

            if (Input.GetMouseButton(0))
            {
                examinedObject.Rotate(deltaMouse.x * rotationSpeed * Vector3.down, Space.World); //in tutoiral vid , if the vector turns to up , it will go to the other side
                examinedObject.Rotate(deltaMouse.y * rotationSpeed * Vector3.right, Space.World);
                lastMousePoisiton = Input.mousePosition;
            }
        }
        if (examinedObject == null)
        {
            // Reset the position and rotation of the examined object
            examinedObject.position = originalPosition[examinedObject];
            examinedObject.rotation = originalRotation[examinedObject];
        }
    }

    void NonExamine()
    {
        if (examinedObject != null)
        {
            if (originalPosition.ContainsKey(examinedObject))
            {
                examinedObject.position = Vector3.Lerp(examinedObject.position, originalPosition[examinedObject], .2f);
            }
            if (originalRotation.ContainsKey(examinedObject))
            {
                examinedObject.rotation = Quaternion.Slerp(examinedObject.rotation, originalRotation[examinedObject], .2f);
            }
        }
    }



    void checkDetail(GameObject canvasOfObjectExamine) // right click show detail
    {
        if (isShowDetail == true)
        {
            canvasOfObjectExamine.SetActive(true);
        }
        else if (isShowDetail == false)
        {
            canvasOfObjectExamine.SetActive(false);
        }
    }

    void checkDetailTrail(GameObject canvasOfObjectExamine) // right click show detail
    {
        if (isShowDetail == true && !firstTimeWatched.Contains(canvasOfObjectExamine))
        {
            canvasOfObjectExamine.SetActive(true);
            firstTimeWatched.Add(canvasOfObjectExamine);
            trailNum++;

            Debug.Log(trailNum);

        }
        else if (isShowDetail == true && firstTimeWatched.Contains(canvasOfObjectExamine))
        {
            canvasOfObjectExamine.SetActive(true);
        }

        else if (isShowDetail == false)
        {
            canvasOfObjectExamine.SetActive(false);
        }

    }




}
