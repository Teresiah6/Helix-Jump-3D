using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 150.0f;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {

        //rotate only if game is started
       if (!GameManager.isGameStarted)
           return;

        //for PC
     if (Input.GetMouseButton(0)) // left mouse

        {

            float mouseX = Input.GetAxisRaw("Mouse X");
            transform.Rotate(0, -mouseX * rotationSpeed * Time.deltaTime, 0);
           // Debug.Log("Mouse is working");

        }

        //For mobile
      if(Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            float xDelta = Input.GetTouch(0).deltaPosition.x;
            transform.Rotate(0, -xDelta * rotationSpeed * Time.deltaTime, 0);
        }
     
        
    }
}
