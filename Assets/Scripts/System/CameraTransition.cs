using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    private Transform cameraPoint;
    readonly float moveSpeed = 1f;
    private int currentCamPoint = 1;
    private GameObject[] points;
    //camera points
    public GameObject pointOne;
    public GameObject pointTwo;
    public GameObject pointThree;
    private bool isTransitionRunning = false;
    private void Start()
    {
        //points = GameObject.FindGameObjectsWithTag("CameraPoint");
        points = new GameObject[] { pointOne, pointTwo, pointThree };
    }
    void Update()
    {
        if (cameraPoint == null) return;

        if (isTransitionRunning)
            StartCoroutine(CallTransition());
    }
    IEnumerator CallTransition()
    {
        //lerp camera from one point to the other under 6 second
        Vector3 vec = new Vector3(0f, 0f, -70f);
        transform.position = Vector3.Lerp(transform.position, cameraPoint.position + vec, Time.deltaTime * moveSpeed);
        yield return new WaitForSeconds(6.0f);
        isTransitionRunning = false;
    }
    public void SetCurrentCamPoint(int level)
    {
        if(level > points.Length)
        {
            Debug.Log("no index with the current number");
            return;
        }
        currentCamPoint = level;
        cameraPoint = points[currentCamPoint - 1].transform;
        isTransitionRunning = true;
    }
}
