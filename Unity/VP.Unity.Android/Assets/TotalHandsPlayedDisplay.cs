using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalHandsPlayedDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VpMachine.OnHandDealt += OnDeal;
        GetComponent<Text>().text = VpMachine.statistics.HandsPlayed.ToString();
    }

    // Update is called once per frame
    void OnDeal(VpMachine.DealEventArgs e)
    {
        GetComponent<Text>().text = VpMachine.statistics.HandsPlayed.ToString();
    }
}
