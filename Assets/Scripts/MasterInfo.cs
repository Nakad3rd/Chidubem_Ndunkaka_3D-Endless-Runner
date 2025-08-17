using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterInfo : MonoBehaviour
{
    public static int coinCount = 0;
    [SerializeField] GameObject coinDisplay;


    
    void Update()
    {
        // Update the coin display text
        coinDisplay.GetComponent<TMPro.TMP_Text>().text = "COINS: " + coinCount;
    }
}
