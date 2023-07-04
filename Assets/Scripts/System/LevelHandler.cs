using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    private CameraTransition cameraTransition;
    private CameraHandler cameraHandler;
    private BuildManager buildManager;
    //level variables for camera handling
    public Collider levelOne;
    public Collider levelTwo;
    public Collider levelThree;
    //prefabs
    public GameObject portal;
    //position of portals
    public Transform portalPosOne;
    public Transform portalPosTwo;
    public Transform portalPosThree;
    //position of endpoints
    public GameObject endPosOne;
    public GameObject endPosTwo;
    public GameObject endPosThree;
    //interface
    public GameObject startButton;

    Level currentlevel = Level.NONE;
    public enum Level {NONE, ONE ,TWO, THREE };
    private void Awake()
    {
        cameraTransition = FindObjectOfType<CameraTransition>();
        cameraHandler = FindObjectOfType<CameraHandler>();
        buildManager = FindObjectOfType<BuildManager>();
    }
    private void Start()
    {
        SetupLevel();
    }

    public void SetupLevel()
    {
        currentlevel++;
        //check is there an actual level to set
        if (currentlevel == Level.NONE)
        {
            Debug.Log("Level is not set");
            return;
        }
        //setup level One
        if(currentlevel == Level.ONE)
            LevelValues(levelOne, portalPosOne,endPosOne, 500);
        
        //setup level Two
        if (currentlevel == Level.TWO){
            endPosOne.SetActive(false);
            LevelValues(levelTwo, portalPosTwo, endPosTwo, 500);
        }
        //setup level Three
        if (currentlevel == Level.THREE){
            endPosTwo.SetActive(false);
            LevelValues(levelThree, portalPosThree, endPosThree, 700);
        }
        
    }
    //setup all the variables what are needed for a level
    private void LevelValues(Collider level,Transform portalPos,GameObject endPos, int currency)
    {
        cameraTransition.SetCurrentCamPoint((int)currentlevel);         //set camera position
        cameraHandler.SetCurrentMap(level);                             //set the map for the camera handler 
        CreatePortal(portalPos);                                        //set portal on the actual level
        SetEndPoint(endPos, true);                                      //set end point on the actual level
        SetupStartButton();                                             //activate Start button to be able to play the level
        PlayerStats.currency = currency;                                //set currency for the level
        buildManager.SetCurrencyText(PlayerStats.currency.ToString());  //update currency text
    }
    private void CreatePortal(Transform portPos)
    {
        //set the portal object to the right position and rotation
        portal.transform.position = portPos.position;
        portal.transform.rotation = portPos.rotation;
    }
    private void SetEndPoint(GameObject pos, bool isactive)
    {
        //set the end point for the monsters
        pos.SetActive(isactive);
    }
    private void SetupStartButton()
    {
        startButton.SetActive(true);
    }
    public int GetLevelInInt()
    {
       return (int)currentlevel;
    }

}
