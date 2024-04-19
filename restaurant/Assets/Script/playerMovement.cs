using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class playerMovement : MonoBehaviour
{

    public string text = "hello";
    [SerializeField] private GameObject closeEnoughInteractUI; //for those ui that when player walk close enough


    /*----------Get Script----------*/
    [SerializeField] private GameObject getMovementScript;
    [SerializeField] private GameObject getMouseLookingScript;


    /*-----------Movement----------
    public CharacterController controller;
    Vector3 velocity;
    float speed = 3f;
    float gravity = -9.81f;-*/


    /*-------------pause-------------*/
    public GameObject pauseMenuUI;
    public bool gamePaused = false;


    /*---------pick up object---------*/
    [SerializeField] private LayerMask pickLayerMask;
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject pickUI;
    [SerializeField] private Transform parent;
    [SerializeField] public GameObject pickedObject;
    private float hitRange = 3;
    private RaycastHit hit;


    /*-------------spawn-------------*/
    [SerializeField] private Transform parentOfSpawn;
    private bool amountOfObj;
    public GameObject spawnObj;


    /*------------customer------------*/
    [SerializeField] private LayerMask customerLayerMask;
    [SerializeField] private GameObject giveUI;
    [SerializeField] private GameObject giveUIMama;
    [SerializeField] private GameObject ask4FoodUI;
    [SerializeField] private GameObject giveThemFoodUI;
    public GameObject customer;
    private float hitRange4Cusomter = 3;
    private RaycastHit hit4Customer;
    public static bool talked = false; //make , if we havent talk to customer , we cant do something
    public bool shown = false;
    [SerializeField] private GameObject UI1;


    /*-----------cooked--------------*/
    [SerializeField] private LayerMask cookingLayerMask;
    [SerializeField] private GameObject cookUI;
    [SerializeField] private GameObject cookDoneUI;
    [SerializeField] private GameObject cookNotDoneUI;
    private float hitRange4cook = 3;
    private RaycastHit hit4cook;
    private bool cooked = false;
    public GameObject cookedFoodObject;
    public GameObject wrongFoodObject;
    [SerializeField] private Transform spawnCookedFood;
    public string correctFoodTag;


    /*--------foodInsideFridge----------*/
    [SerializeField] private LayerMask foodA;
    [SerializeField] private LayerMask foodB;
    [SerializeField] private LayerMask LowerFridge;
    [SerializeField] private GameObject be4talk;
    [SerializeField] private LayerMask Fridge;
    private float hitRange4Fridge = 3;
    private RaycastHit hit4Fridge;
    public GameObject foodAFromFridge;
    public GameObject foodBFromFridge;
    public Transform pickFoodFridge;
    public Animator anim;
    public bool animPlay = false;
    public bool lookingAtHandle = false;


    /*-------------blackWindow----------------*/
    [SerializeField] private LayerMask blackWindowLayer;
    private bool doneWindowTalk = false;


    /*------------whiteBoard--------------*/
    [SerializeField] private LayerMask whiteBoardLayer;


    void Start()
    {

    }



    void Update()
    {
        /*--------------some rules before and after customer interaction-------*/



        /*------Movement-------
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);-*/


        /*-------pause----------*/
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                closeEnoughInteractUI.SetActive(true);
                resume();
            } else
            {
                closeEnoughInteractUI.SetActive(false);
                pause();
            }

        }


        /*-----pick up object---*/
        

        if (Physics.Raycast(cam.position, cam.forward, out hit, hitRange, pickLayerMask))
        {
            pickUI.SetActive(true);
        }
        else
        {
            pickUI.SetActive(false);
        }

        if (Physics.Raycast(cam.position, cam.forward, out hit, hitRange, pickLayerMask) && Input.GetKeyDown(KeyCode.E))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            //if (hit.transform.TryGetComponent(out Highlight highlight)){}
                pickedObject = hit.collider.gameObject;
                pickedObject.transform.localPosition = new Vector3(0, 0, 0);
                pickedObject.transform.rotation = Quaternion.identity;
                pickedObject.transform.SetParent(parent.transform, false);
                if (rb != null)
                {
                    rb.isKinematic = true;
                    pickUI.SetActive(false);
                }
                return;

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (pickedObject != null)
            {
                pickedObject.transform.SetParent(null);
                pickedObject = null;
            }
        }


        /*-----------cooked----------------*/
        if (Physics.Raycast(cam.position, cam.forward, out hit4cook, hitRange4cook, cookingLayerMask) && anim4Oven.isOpen == true)
        {
            cookUI.SetActive(true);
            if (cooked == false)
            {
                cookNotDoneUI.SetActive(true);
                cookDoneUI.SetActive(false);
            } else
            {
                cookNotDoneUI.SetActive(false);
                cookDoneUI.SetActive(true);
            }
        }
        else
        {
            cookUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && anim4Oven.isOpen == true)
        {
            if (Physics.Raycast(cam.position, cam.forward, out hit4cook, hitRange4cook, cookingLayerMask))
            {
                if ((cooked == false) && (pickedObject != null))//if the object is not cooked 
                {
                    //new WaitForSeconds(10f);

                    if (pickedObject.CompareTag(correctFoodTag)) {
                        cookedFoodObject = Instantiate(cookedFoodObject, Vector3.zero, Quaternion.identity);
                        Debug.Log("Correct");
                    }

                    else if (!pickedObject.CompareTag(correctFoodTag))
                    {
                        cookedFoodObject = Instantiate(wrongFoodObject, Vector3.zero, Quaternion.identity);
                        Debug.Log("Wrong");
                    }
                    Destroy(pickedObject);
                    cookedFoodObject.transform.SetParent(spawnCookedFood.transform, false);
                    cookedFoodObject.transform.rotation = Quaternion.identity;
                    Debug.Log("cooking");
                    cooked = true;
                }
                else if ((cooked == true) && (pickedObject != null))
                {
                    Debug.Log("done cook already");
                    //giveThemFoodUI.SetActive(true);
                }
            }

        }


        /*--------------customer----------------*/

        if (Physics.Raycast(cam.position, cam.forward, out hit4Customer, hitRange4Cusomter, customerLayerMask))
        {
            giveUI.SetActive(true);
        } else
        {
            giveUI.SetActive(false);
        }


        /*--------------customer----------------*/
        if (Physics.Raycast(cam.position, cam.forward, out hit))
        {
            /*-
            if (pickedObject == null && cooked == false)
            {
                if (UI1 != null)
                { 
                    UI1.SetActive(true);
                    talked = true;
                    Time.timeScale = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    shown = true;
                }

            }
            if (pickedObject == null && cooked == false && talked == true)
            {
                Debug.Log("I am looking forward to try the food");
            }
            if (cooked != true && pickedObject != null)
            {
                Debug.Log("emm , i dont think this is even cooked....");
                //giveThemFoodUI.SetActive(true);
            } if (cooked == true && pickedObject == null)
            {
                Debug.Log("Where is my food????");
                //UI of customer talking
            } if (cooked == true && pickedObject != null && pickedObject != cookedFoodObject)
            {
                Debug.Log("What the ... this is NOT what i order!!");
                //UI of customer talking
            }-*/

            if (Input.GetKeyDown(KeyCode.E)&&hit.collider.CompareTag("cus"))
            {
                if (cooked == true && pickedObject != null && pickedObject == cookedFoodObject && pickedObject.CompareTag(correctFoodTag))
                {
                    Debug.Log("ThankYOUUUU");
                    examineObject.trailNum++;
                    Destroy(pickedObject);
                    Destroy(customer); // if customer destroy , spawn another trail 
                    //UI of customer talking
                }
                if (cooked == true && pickedObject != null && pickedObject == cookedFoodObject && !pickedObject.CompareTag(correctFoodTag))
                {
                    Debug.Log("What the ... this is NOT what i order!!");
                    //UI of customer talking
                    Destroy(customer);
                    Destroy(pickedObject);
                }
            }
        }

       

        if (shown == true)
        {
            giveUI.SetActive(false);
            diactive(UI1);
        }
        

        /*--------foodInsideFridge--------*/
        
        if (Physics.Raycast(cam.position, cam.forward, out hit4Fridge, hitRange4Fridge, LowerFridge))//i might need to make another highlight script just for fridge
        {
            hit4Fridge.collider.GetComponent<Highlight4Fridge>()?.ToggleHighlight(true);
            lookingAtHandle = true;
        } else
        {
            //hit4Fridge.collider.GetComponent<Highlight4Fridge>()?.ToggleHighlight(false); something wrong in here , and idk where 
        }


        if (Physics.Raycast(cam.position, cam.forward, out hit4Fridge, hitRange4Fridge, LowerFridge) && talked == false) // if hit collide w fridge's obejct , but we havent talk to customer
        {
            be4talk.SetActive(true);
        }
        else //if hit didnt collider w fridge 
        {
            be4talk.SetActive(false);
        }

        if (Physics.Raycast(cam.position, cam.forward, out hit4Fridge, hitRange4Fridge, LowerFridge) && talked == true)
        {
            //UI of fridge can be use pops out
            Debug.Log("E to open fridge");
        }
        else
        {
            //hide the UI
        }

        if (Physics.Raycast(cam.position, cam.forward, out hit4Fridge, hitRange4Fridge, foodA) && talked == true && pickedObject != null)
        {
            //UI of fridge can be use pops out
            Debug.Log("cant pick anymore");
        }
        else
        {
            //hide the UI
        }


        if (Input.GetKeyDown(KeyCode.E) && talked == true && anim4LowerFridge.animPlay == true)//if the camera hit fridge's food , and we already talked to customer and open the fridge
        {
            if (Physics.Raycast(cam.position, cam.forward, out hit4Fridge, hitRange4Fridge, foodA) && doneWindowTalk == true) //foodA
            {
                Rigidbody rb4Fridge = hit4Fridge.collider.GetComponent<Rigidbody>();
                if (pickedObject == null && cooked == false)
                {
                    foodAFromFridge = Instantiate(foodAFromFridge, Vector3.zero, Quaternion.identity);
                    foodAFromFridge.transform.SetParent(parent.transform, false);
                    foodAFromFridge.transform.rotation = Quaternion.identity;
                    rb4Fridge.isKinematic = true;
                    pickedObject = foodAFromFridge; //this is vry important , if i write foodFromFridge = pickedObject , it wont work
                }
                if (pickedObject != null && cooked == true || pickedObject != null && cooked == false || pickedObject == null && cooked == false || pickedObject == null && cooked == true)
                {
                    //show UI of cant pick anymore food from fridge
                }
            }
            if (Physics.Raycast(cam.position, cam.forward, out hit4Fridge, hitRange4Fridge, foodB) &&doneWindowTalk ==true) //foodB
            {
                Rigidbody rb4Fridge = hit4Fridge.collider.GetComponent<Rigidbody>();
                if (pickedObject == null && cooked == false)
                {
                    foodBFromFridge = Instantiate(foodBFromFridge, Vector3.zero, Quaternion.identity);
                    foodBFromFridge.transform.SetParent(parent.transform, false);
                    foodBFromFridge.transform.rotation = Quaternion.identity;
                    //rb4Fridge.isKinematic = true;
                    pickedObject = foodBFromFridge; //this is vry important , if i write foodFromFridge = pickedObject , it wont work
                }
                if (pickedObject != null && cooked == true || pickedObject != null && cooked == false || pickedObject == null && cooked == false || pickedObject == null && cooked == true)
                {
                    //show UI of cant pick anymore food from fridge
                    Debug.Log("cant pick up anymore");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))//drop object
        {
            if (pickFoodFridge != null)
            {
                pickFoodFridge.transform.SetParent(null);
                pickFoodFridge = null;
            }
        }
        

        /*-------------blackWindow----------------*/
        if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(cam.position, cam.forward, out hit, hitRange, blackWindowLayer) && talked == true && doneWindowTalk == false)
        {
            Debug.Log("You need something ?");
            doneWindowTalk = true;
        }




    }

    /*------------pause-------------*/
    public void resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void menu()
    {
        Debug.Log("Menu");
        //SceneManager.LoadScene("");
    }

    public void quit()
    {
        Debug.Log("Quitting..");
        Application.Quit();
    }


    /*-------------spawn-------------*/
    public void spawn()
    {
        if (amountOfObj == false)
        {
            Instantiate(spawnObj, parentOfSpawn);
        }
    }


    /*------------UIdiactive-------------*/
    public void diactive(GameObject UI){
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("UI set false");
            UI.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            shown = false;
            Destroy(UI);
        }

    }




}
