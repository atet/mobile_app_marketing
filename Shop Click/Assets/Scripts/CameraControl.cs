using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector3 touchStart;

    // For Swipe()
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private const float currentSwipeMagnitudeThreshold = 10;

    // Current camera position
    Vector2 cameraPositionShop = new Vector2(0f, 0f);
    Vector2 cameraPositionColosseum = new Vector2(5.65f, 0f);
    Vector2 cameraPositionMine = new Vector2(-5.65f, 0f);
    Vector2 cameraPositionMenu = new Vector2(0f, 10f);
    Vector2 cameraPositionTown = new Vector2(0f, -10f);

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = cameraPositionShop;
        Debug.Log("Start camera position is the shop: x = " +  Camera.main.transform.position.x.ToString() + ": y = " +  Camera.main.transform.position.y.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        //CameraPan();
        Swipe();
    }



    public void Swipe()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if(Input.GetMouseButtonUp(0))
        {
            // save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            // create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            if(currentSwipe.magnitude > currentSwipeMagnitudeThreshold)
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
            if(cameraPositionShop.Equals(Camera.main.transform.position)){
                //Debug.Log("At Shop, going down to Town");
                Camera.main.transform.position = cameraPositionTown;
            }
            else if(cameraPositionMenu.Equals(Camera.main.transform.position))
            {
                //Debug.Log("At Menu, going down to Shop");
                Camera.main.transform.position = cameraPositionShop;
            }
        }
        if(direction == "up")
        {
            //Debug.Log("Swiped down, going up");
            if(cameraPositionShop.Equals(Camera.main.transform.position)){
                //Debug.Log("At Shop, going up to Menu");
                Camera.main.transform.position = cameraPositionMenu;
            }
            else if(cameraPositionTown.Equals(Camera.main.transform.position))
            {
                //Debug.Log("At Town, going up to Shop");
                Camera.main.transform.position = cameraPositionShop;
            }
        }
        if(direction == "right")
        {
            //Debug.Log("Swiped left, going right");
            if(cameraPositionShop.Equals(Camera.main.transform.position)){
                //Debug.Log("At Shop, going right to Colosseum");
                Camera.main.transform.position = cameraPositionColosseum;
            }
            else if(cameraPositionMine.Equals(Camera.main.transform.position))
            {
                //Debug.Log("At Mine, going right to Shop");
                Camera.main.transform.position = cameraPositionShop;
            }
        }
        if(direction == "left")
        {
            //Debug.Log("Swiped right, going left");
            if(cameraPositionShop.Equals(Camera.main.transform.position)){
                //Debug.Log("At Shop, going left to Mine");
                Camera.main.transform.position = cameraPositionMine;
            }
            else if(cameraPositionColosseum.Equals(Camera.main.transform.position))
            {
                //Debug.Log("At Colosseum, going left to Shop");
                Camera.main.transform.position = cameraPositionShop;
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
