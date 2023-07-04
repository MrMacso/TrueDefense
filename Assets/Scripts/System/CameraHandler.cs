using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Collider currentMap;

    readonly float panSpeed = 40f;
    readonly float borderGap = 10f;
    private bool doMovement = true;
    //camera
    readonly float scrollScaler = 1000f;
    readonly float scrollSpeed = 10f;
    readonly float minZoom = 20f;
    readonly float maxZoom = 100f;
    readonly float cameraGap = 15f;
    void Update()
    {
        //enable/disable camera control
        if(Input.GetKeyUp(KeyCode.Escape))
           doMovement = !doMovement;
        if (!doMovement) 
            return;
        if (currentMap == null) 
            return;
        //sides of the map
        float rightX = currentMap.transform.position.x + (currentMap.bounds.size.x / 2);
        float leftX = currentMap.transform.position.x - (currentMap.bounds.size.x / 2); 
        float topZ = currentMap.transform.position.z + (currentMap.bounds.max.z / 2) - cameraGap;
        float bottomZ = currentMap.transform.position.z - (currentMap.bounds.max.z / 2) - cameraGap;
        //move camera up
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - borderGap)
        {
            if((int)transform.position.z >  topZ)
            {
                Vector3 v = new Vector3(transform.position.x, transform.position.y, topZ);
                transform.SetPositionAndRotation(v, transform.rotation);
            }
            else
            {
                transform.Translate(Vector3.forward * panSpeed* Time.deltaTime, Space.World);
            }
        }
        //move camera down
        if (Input.GetKey("s") || Input.mousePosition.y <= borderGap)
        {
            if (transform.position.z < bottomZ)
            {
                Vector3 v = new Vector3(transform.position.x, transform.position.y, bottomZ);
                transform.SetPositionAndRotation(v, transform.rotation);
            }
            else
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }
        }
        //move camera right
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - borderGap)
        {
            if(transform.position.x > rightX)
            {
                Vector3 v = new Vector3(rightX, transform.position.y, transform.position.z);
                transform.SetPositionAndRotation(v, transform.rotation);
            }
            else
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }
        }
        //move camera left
        if (Input.GetKey("a") || Input.mousePosition.x <= borderGap)
        {
            if(transform.position.x < leftX)
            {
                Vector3 v = new Vector3(leftX, transform.position.y, transform.position.z);
                transform.SetPositionAndRotation(v, transform.rotation);
            }
            else
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }
        }
        //zoom in/out
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 scrollPosition = transform.position;
        scrollPosition.y -= scroll * scrollScaler * scrollSpeed * Time.deltaTime;
        //limit zooming
        scrollPosition.y = Mathf.Clamp(scrollPosition.y, minZoom, maxZoom);
        transform.position = scrollPosition;
    }
    public void SetCurrentMap(Collider map)
    {
        currentMap = map;
    }
}
