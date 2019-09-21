using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] public Camera cameraControl;
    public static CameraControl instance;
    //private bool inputEnabled;
    private bool swipeEnabled;
    private bool swipeEnabledTown;
    private bool swipeEnabledMine;
    private bool swipeEnabledColosseum;
    Vector3 touchStart;

    // For Swipe()
    Vector2 firstPressPos;
    float firstPressTime;
    Vector2 secondPressPos;
    float secondPressTime;
    Vector2 currentSwipe;

    Vector2 currentScreen;

    private const float currentSwipeMagnitudeThreshold = 100;
    private const float currentSwipeTimeThreshold = 2; // Time in second

    // Tutorial controls
    // TODO: Fix this, the actual input coordinates are different than the relative UI coordinates...
    private bool restrictOnClick; public void EnableRestrictOnClick(){ restrictOnClick = true; } public void DisableRestrictOnClick(){ restrictOnClick = false; }
    private Vector3 unrestrictedOnClickAreaTopLeft, unrestrictedOnClickAreaBottomRight;
    public void SetUnrestrictedOnClickArea(Vector3 unrestrictedOnClickAreaTopLeft, Vector3 unrestrictedOnClickAreaBottomRight)
    {
        Debug.Log("Unrestricted area set as: Top-Left x,y = " + unrestrictedOnClickAreaTopLeft.x.ToString() + "," + unrestrictedOnClickAreaTopLeft.y.ToString() +
        " and Bottom-Right x,y = " + unrestrictedOnClickAreaBottomRight.x.ToString() + "," + unrestrictedOnClickAreaBottomRight.y.ToString());
        this.unrestrictedOnClickAreaTopLeft = unrestrictedOnClickAreaTopLeft;
        this.unrestrictedOnClickAreaBottomRight = unrestrictedOnClickAreaBottomRight;
    }
    public void CheckUnrestrictedOnClickArea()
    {

        // TODO: Fix this, the actual input coordinates are different than the relative UI coordinates...
        if(Input.GetMouseButtonDown(0))
        {
            float scale = 5;
            Vector3 absolutePosition = Input.mousePosition * scale;

            if
            (
                (absolutePosition.x >= unrestrictedOnClickAreaTopLeft.x) &&
                (absolutePosition.x <= unrestrictedOnClickAreaBottomRight.x) &&
                (absolutePosition.y >= unrestrictedOnClickAreaBottomRight.y) &&
                (absolutePosition.y <= unrestrictedOnClickAreaTopLeft.y)
            )
            {
                // DO SOMETHING
                // TODO: Allow clicking
                Debug.Log("Inside unrestricted area: x = " + absolutePosition.x + ", y = " + absolutePosition.y);
                Tutorial.instance.RemoveUIOverlayPointer();
            }
            else
            {
                // DO SOMETHING
                // TODO: Don't allow clicking
                Debug.Log("Outside unrestricted area: x = " + absolutePosition.x + ", y = " + absolutePosition.y);

                // WARNING: Weird issues with above not executing on phone, stuck on last tutorial with no navigation, disabling for now by also calling this outside of unrestricted area.
                Tutorial.instance.RemoveUIOverlayPointer();
            }
        }
    }

    // public void CheckCoordinatesOnClick() // For debugging
    // {
    //     if(Input.GetMouseButtonDown(0))
    //     {
    //         float scale = 5;
    //         Vector3 absolutePosition = Input.mousePosition * scale;
    //         Debug.Log("Absolute Position (x,y) = (" + absolutePosition.x + "," + absolutePosition.y + ")");
    //     }
    // }


    // Current camera position
    private Dictionary<string, Vector2> cameraPositions = new Dictionary<string, Vector2>()
    {
        { "Shop", new Vector2(0f, 0f) },
        { "Colosseum", new Vector2(5.65f, 0f) },
        { "Mine", new Vector2(-5.65f, 0f) },
        { "Menu", new Vector2(0f, 10f) },
        { "Town", new Vector2(0f, -10f) }
    };
    public Dictionary<string, Vector2> GetCameraPositions()
    {
        return(cameraPositions);
    }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //inputEnabled = true;
        swipeEnabled = true;
        swipeEnabledTown = true;
        swipeEnabledMine = true;
        swipeEnabledColosseum = true;



        restrictOnClick = false;
        unrestrictedOnClickAreaTopLeft = new Vector3(0,0,0);
        unrestrictedOnClickAreaBottomRight = new Vector3(0,0,0);

        Camera.main.transform.position = cameraPositions["Shop"];

        // Start with navigating to these screens disabled.
        DisableSwipeColosseum();
        DisableSwipeMine();
        DisableSwipeTown();
        //currentScreen = cameraPositions["Shop"];
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
        if(restrictOnClick) // Required to make pointer go away.
        {
            CheckUnrestrictedOnClickArea();
        }
        //CheckCoordinatesOnClick(); // For debugging Input coordinates to UI coordinates
    }

    public void EnableSwipe()
    {
        swipeEnabled = true;
        //swipeEnabledTown = true;
        //swipeEnabledColosseum = true;
        //swipeEnabledMine = true;
        firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        // First click down registered as pressing the button to disable, then click down registered as what re-enabled
        // This may cause the camera to think this was a swipe.
        // Therefore resetting first click down as current position will result in the magnitude of 0.
        //Camera.main.transform.position = currentScreen;
    }
    public void DisableSwipe(){ swipeEnabled = false; }
    public void DisableSwipeColosseum(){ swipeEnabledColosseum = false; } public void EnableSwipeColosseum(){ swipeEnabledColosseum = true; }
    public void DisableSwipeTown(){ swipeEnabledTown = false; } public void EnableSwipeTown(){ swipeEnabledTown = true; }
    public void DisableSwipeMine(){ swipeEnabledMine = false; } public void EnableSwipeMine(){ swipeEnabledMine = true; }
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
            if(cameraPositions["Shop"].Equals(Camera.main.transform.position) & swipeEnabledTown){
                //Debug.Log("At Shop, going down to Town");
                if(!Tutorial.instance.SEEN_UIOVERLAYTEXTBOX_TOWN_1)
                {
                    Tutorial.instance.SummonUIOverlayTextBoxImageLarge("Upgrading", "Click on your workers to upgrade resource rates and caps\n\n- Biggs", "Images/Tutorial/town");
                    Tutorial.instance.SEEN_UIOVERLAYTEXTBOX_TOWN_1 = true;
                }
                CameraPosition("Town");
                //currentScreen = new Dictionary<string, Vector2>(){ {"Town", cameraPositions["Town"]} };
                //Debug.Log("currentScreen = Town");
            }
            else if(cameraPositions["Menu"].Equals(Camera.main.transform.position))
            {
                //Debug.Log("At Menu, going down to Shop");
                CameraPosition("Shop");
                //currentScreen = new Dictionary<string, Vector2>(){ {"Shop", cameraPositions["Shop"]} };
                //Debug.Log("currentScreen = Shop");
            }
        }
        if(direction == "up")
        {
            //Debug.Log("Swiped down, going up");
            if(cameraPositions["Shop"].Equals(Camera.main.transform.position)){
                //Debug.Log("At Shop, going up to Menu");
                CameraPosition("Menu");
                //currentScreen = new Dictionary<string, Vector2>(){ {"Menu", cameraPositions["Menu"]} };
                //Debug.Log("currentScreen = Menu");
            }
            else if(cameraPositions["Town"].Equals(Camera.main.transform.position))
            {
                //Debug.Log("At Town, going up to Shop");
                CameraPosition("Shop");
                //currentScreen = new Dictionary<string, Vector2>(){ {"Shop", cameraPositions["Shop"]} };
                //Debug.Log("currentScreen = Shop");
            }
        }
        if(direction == "right")
        {
            //Debug.Log("Swiped left, going right");
            if(cameraPositions["Shop"].Equals(Camera.main.transform.position) & swipeEnabledColosseum){
                //Debug.Log("At Shop, going right to Colosseum");
                CameraPosition("Colosseum");
                //currentScreen = new Dictionary<string, Vector2>(){ {"Colosseum", cameraPositions["Colosseum"]} };
                //Debug.Log("currentScreen = Colosseum");
            }
            else if(cameraPositions["Mine"].Equals(Camera.main.transform.position))
            {
                //Debug.Log("At Mine, going right to Shop");
                CameraPosition("Shop");
                //currentScreen = new Dictionary<string, Vector2>(){ {"Shop", cameraPositions["Shop"]} };
                //Debug.Log("currentScreen = Shop");
            }
        }
        if(direction == "left")
        {
            //Debug.Log("Swiped right, going left");
            if(cameraPositions["Shop"].Equals(Camera.main.transform.position) & swipeEnabledMine){
                //Debug.Log("At Shop, going left to Mine");
                if(!Tutorial.instance.SEEN_UIOVERLAYTEXTBOX_SHOP_1)
                {
                    Tutorial.instance.SummonUIOverlayTextBoxImageLarge("Crafting", "Do this:\n\n- Biggs", "Images/Tutorial/shop");
                    Tutorial.instance.SEEN_UIOVERLAYTEXTBOX_SHOP_1 = true;
                }
                CameraPosition("Mine");
                //currentScreen = new Dictionary<string, Vector2>(){ {"Mine", cameraPositions["Mine"]} };
                //Debug.Log("currentScreen = Mine");
            }
            else if(cameraPositions["Colosseum"].Equals(Camera.main.transform.position))
            {
                //Debug.Log("At Colosseum, going left to Shop");
                CameraPosition("Shop");
                //currentScreen = new Dictionary<string, Vector2>(){ {"Shop", cameraPositions["Shop"]} };
                //Debug.Log("currentScreen = Shop");
            }
        }
    }
    public void CameraPosition(string screen)
    {
        Camera.main.transform.position = cameraPositions[screen];
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
