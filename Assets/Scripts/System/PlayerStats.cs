using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static int currency;
    public static int startCurrency = 0;

    private void Awake()
    {
        currency = startCurrency; 
    }
}
