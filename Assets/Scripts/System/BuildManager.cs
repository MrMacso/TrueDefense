using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    [SerializeField] private TextMeshProUGUI currencyText;
    //turret prefabs
    public GameObject standardTurretPrefab;
    public GameObject missileTurretPrefab;
    public GameObject defenseTurretPrefab;
    //selected turret
    private TurretBlueprint turretToBuild;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one build manager in the scene!");
            return;
        }
        instance = this;
    }
    private void Start()
    {
        currencyText.text = PlayerStats.currency.ToString();
    }
    public bool CanBuild { get { return turretToBuild!= null; } }
    //build turret on a node
    public void BuildTurretOn(Node node)
    {
        //check is there enough currency to build
        if(PlayerStats.currency < turretToBuild.cost)
        {
            Debug.Log("Not Enough Gold!");
            return;
        }
        //substract cost
        PlayerStats.currency -= turretToBuild.cost;
        //update currency text 
        currencyText.text = PlayerStats.currency.ToString();
        //build turret
        GameObject turret = Instantiate(turretToBuild.preFab, node.GetBuildPsoition(),Quaternion.identity);
        node.turret= turret;

        Debug.Log("Turret built! Gold left: " + PlayerStats.currency);
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
    public void SetCurrencyText(string text)
    {
        currencyText.text = text;
    }
}
