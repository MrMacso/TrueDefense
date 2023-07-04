using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTurret : MonoBehaviour
{
    public Transform partToRotate;
    private Transform target;
    private float turnSpeed = 8f;
    private float randomNum = 1;
    private float maxTargets = 6f;
    private AudioManager audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        //change target every 1.5 second
        InvokeRepeating("UpdateTarget", 0f, 1.848f);
        randomNum = Random.Range(0, maxTargets);

    }
    private void UpdateTarget()
    {
        //detect tranform point on the map
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        randomNum = Random.Range(0, maxTargets);
        target = enemies[(int)randomNum].transform;
        //sound
        audioManager.Play("Rotate");
    }

    void Update()
    {
        if (target == null) return;
        //rotate turret to the point
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
}
