using UnityEngine;

public class Shop : MonoBehaviour
{
    //setup these variable in the inspector
    public TurretBlueprint basicTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint defenseTurret;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectBasicTurret()
    {
        Debug.Log("Basic selected");
        buildManager.SelectTurretToBuild(basicTurret);
    } 
    public void SelectMissileTurret()
    {
        Debug.Log("Missile selected");
        buildManager.SelectTurretToBuild(missileTurret);
    }
    public void SelectDefenseTurret()
    {
        Debug.Log("Defense selected");
        buildManager.SelectTurretToBuild(defenseTurret);
    }

}
