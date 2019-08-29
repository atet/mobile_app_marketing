using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
<<<<<<< HEAD
    public static CameraControl instance;
=======
>>>>>>> b2c77b4453039f460cb0e921e638927b7d857e6d
    private bool swipeEnabled = true;
    Vector3 touchStart;

    // For Swipe()
    Vector2 firstPressPos;
    float firstPressTime;
    Vector2 secondPressPos;
    float secondPressTime;
    Vector2 currentSwipe;

    Vector2 currentScreen;

    private const float currentSwipeMagnitudeThreshold = 10;
    private const float currentSwipeTimeThreshold = 2; // Time in second

    // Current camera position
<<<<<<< HEAD
    Dictionary<string, Vector2> cameraPositions = new Dictionary<string, Vector2>()
    {
        { "Shop", new Vector2(0f, 0f) },
        { "Colosseum", new Vector2(5.65f, 0f) },
        { "Mine", new Vector2(-5.65f, 0f) },
        { "Menu", new Vector2(0f, 10f) },
        { "Town", new Vector2(0f, -10f) }
    };
    Vector2 currentScreen;

    void Awake()
    {
        instance = this;
    }
=======
    Vector2 cameraPositionShop = new Vector2(0f, 0f);
    Vector2 cameraPositionColosseum = new Vector2(5.65f, 0f);
    Vector2 cameraPositionMine = new Vector2(-5.65f, 0f);
    Vector2 cameraPositionMenu = new Vector2(0f, 10f);
    Vector2 cameraPositionTown = new Vector2(0f, -10f);
>>>>>>> b2c77b4453039f460cb0e921e638927b7d857e6d

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        Camera.main.transform.position = cameraPositions["Shop"];
        currentScreen = cameraPositions["Shop"];
=======
        Camera.main.transform.position = currentScreen = cameraPositionShop;
        currentScreen = Camera.main.transform.position;
>>>>>>> b2c77b4453039f460cb0e921e638927b7d857e6d
        //Debug.Log("Start camera position is the shop: x = " +  Camera.main.transform.position.x.ToString() + ": y = " +  Camera.main.transform.position.y.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        //CameraPan();
        if(swipeEnabled)
        {
            Swipe();
        }
<<<<<<< HEAD
    }

    public void EnableSwipe()
    {
        swipeEnabled = true;
        firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        // First click down registered as pressing the button to disable, then click down registered as what re-enabled
        // This may cause the camera to think this was a swipe.
        // Therefore resetting first click down as current position will result in the magnitude of 0.
        //Camera.main.transform.position = currentScreen;
    }
    public void DisableSwipe()
    {
        swipeEnabled = false;
        //currentScreen = Camera.main.transform.position;

    }
=======

    }

    public void EnableSwipe()
    {
        swipeEnabled = true;
        //Camera.main.transform.position = currentScreen;
    }
    public void DisableSwipe()
    {
        swipeEnabled = false;
    }
>>>>>>> b2c77b4453039f460cb0e921e638927b7d857e6d
    public void Swipe()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            firstPressTime = Time.time;
        }
        if(Input.GetMouseButtonUp(0))
        {
            //Debug.Log("Detected Button Up");
            // save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            secondPressTime = Time.time;
            
            // create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //float swipeDistance = Vector2.Distance(firstPressPos, secondPressPos);
            //Debug.Log("Swipe took: " + System.Math.Round((secondPressTime - firstPressTime), 2).ToString() + "sec. and magnitude of: " + System.Math.Round(swipeDistance, 2).ToString());

            if(
                (currentSwipe.magnitude > currentSwipeMagnitudeThreshold) &&
                ((secondPressTime - firstPressTime) < currentSwipeTimeThreshold)
              )
            {
                // normalize the 2d vector
                currentSwipe.Normalize();

                // swipe is upwards
                if(currentSwipe.y > 0 & currentSwipe.x > -0.5f & currentSwipe.x < 0.5f)
                {
                    CameraTransition("down");
                }
                if(currentSwipe.y < 0 & currentSwipe.x > -0.5f & currentSwipe.x < 0.5f)
                {
                    CameraTransition("up");
                }
                if(currentSwipe.x < 0 & currentSwipe.y > -0.5f & currentSwipe.y < 0.5f)
                {
                    CameraTransition("right");
                }
                if(currentSwipe.x > 0 & currentSwipe.y > -0.5f & currentSwipe.y < 0.5f)
                {
                    CameraTransition("left");
                }
            }
        }
        
    }

    public void CameraTransition(string direction)
    {
        if(direction == "down")
        {
            //Debug.Log("Swiped up, going down");
            if(cameraPositions["Shop"].Equals(Camera.main.transform.position)){
                //Debug.Log("At Shop, going down to Town");
<<<<<<< HEAD
                Camera.main.transform.position = cameraPositions["Town"];
                //currentScreen = new Dictionary<string, Vector2>(){ {"Town", cameraPositions["Town"]} };
                //Debug.Log("currentScreen = Town");
=======
                Camera.main.transform.position = cameraPositionTown;
                currentScreen = cameraPositionTown;
                Debug.Log("currentScreen = Town");
>>>>>>> b2c77b4453039f460cb0e921e638927b7d857e6d
            }
            else if(cameraPositions["Menu"].Equals(Camera.main.transform.position))
            {
                //Debug.Log("At Menu, going down to Shop");
<<<<<<< HEAD
                Camera.main.transform.position = cameraPositions["Shop"];
                //currentScreen = new Dictionary<string, Vector2>(){ {"Shop", cameraPositions["Shop"]} };
                //Debug.Log("currentScreen = Shop");
=======
                Camera.main.transform.position = cameraPositionShop;
                currentScreen = cameraPositionShop;
                Debug.Log("currentScreen = Shop");
>>>>>>> b2c77b4453039f460cb0e921e638927b7d857e6d
            }
        }
        if(direction == "up")
        {
            //Debug.Log("Swiped down, going up");
            if(cameraPositions["Shop"].Equals(Camera.main.transform.position)){
                //Debug.Log("At Shop, going up to Menu");
<<<<<<< HEAD
                Camera.main.transform.position = cameraPositions["Menu"];
                //currentScreen = new Dictionary<string, Vector2>(){ {"Menu", cameraPositions["Menu"]} };
                //Debug.Log("currentScreen = Menu");
=======
                Camera.main.transform.position = cameraPositionMenu;
                currentScreen = cameraPositionMenu;
                Debug.Log("currentScreen = Menu");
>>>>>>> b2c77b4453039f460cb0e921e638927b7d857e6d
            }
            else if(cameraPositions["Town"].Equals(Camera.main.transform.position))
            {
                //Debug.Log("At Town, going up to Shop");
<<<<<<< HEAD
                Camera.main.transform.position = cameraPositions["Shop"];
                //currentScreen = new Dictionary<string, Vector2>(){ {"Shop", cameraPositions["Shop"]} };
                //Debug.Log("currentScreen = Shop");
=======
                Camera.main.transform.position = cameraPositionShop;
                currentScreen = cameraPositionShop;
                Debug.Log("currentScreen = Shop");
>>>>>>> b2c77b4453039f460cb0e921e638927b7d857e6d
            }
        }
        if(direction == "right")
        {
            //Debug.Log("Swiped left, going right");
            if(cameraPositions["Shop"].Equals(Camera.main.transform.position)){
                //Debug.Log("At Shop, going right to Colosseum");
<<<<<<< HEAD
                Camera.main.transform.position = cameraPositions["Colosseum"];
                //currentScreen = new Dictionary<string, Vector2>(){ {"Colosseum", cameraPositions["Colosseum"]} };
                //Debug.Log("currentScreen = Colosseum");
=======
                Camera.main.transform.position = cameraPositionColosseum;
                currentScreen = cameraPositionColosseum;
                Debug.Log("currentScreen = Colosseum");
>>>>>>> b2c77b4453039f460cb0e921e638927b7d857e6d
            }
            else if(cameraPositions["Mine"].Equals(Camera.main.transform.position))
            {
                //Debug.Log("At Mine, going right to Shop");
<<<<<<< HEAD
                Camera.main.transform.position = cameraPositions["Shop"];
                //currentScreen = new Dictionary<string, Vector2>(){ {"Shop", cameraPositions["Shop"]} };
                //Debug.Log("currentScreen = Shop");
=======
                Camera.main.transform.position = cameraPositionShop;
                currentScreen = cameraPositionShop;
                Debug.Log("currentScreen = Shop");
>>>>>>> b2c77b4453039f460cb0e921e638927b7d857e6d
            }
        }
        if(direction == "left")
        {
            //Debug.Log("Swiped right, going left");
            if(cameraPositions["Shop"].Equals(Camera.main.transform.position)){
                //Debug.Log("At Shop, going left to Mine");
<<<<<<< HEAD
                Camera.main.transform.position = cameraPositions["Mine"];
                //currentScreen = new Dictionary<string, Vector2>(){ {"Mine", cameraPositions["Mine"]} };
                //Debug.Log("currentScreen = Mine");
=======
                Camera.main.transform.position = cameraPositionMine;
                currentScreen = cameraPositionMine;
                Debug.Log("currentScreen = Mine");
>>>>>>> b2c77b4453039f460cb0e921e638927b7d857e6d
            }
            else if(cameraPositions["Colosseum"].Equals(Camera.main.transform.position))
            {
                //Debug.Log("At Colosseum, going left to Shop");
<<<<<<< HEAD
                Camera.main.transform.position = cameraPositions["Shop"];
                //currentScreen = new Dictionary<string, Vector2>(){ {"Shop", cameraPositions["Shop"]} };
                //Debug.Log("currentScreen = Shop");
=======
                Camera.main.transform.position = cameraPositionShop;
                currentScreen = cameraPositionShop;
                Debug.Log("currentScreen = Shop");
>>>>>>> b2c77b4453039f460cb0e921e638927b7d857e6d
            }
        }
    }

    public void CameraPan()
    {
        if(Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if(Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
    }








}
