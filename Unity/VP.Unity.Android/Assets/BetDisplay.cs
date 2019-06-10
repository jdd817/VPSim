using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VpMachine.OnBetChange += OnBetChange;
        VpMachine.OnHandsPlayedChange += OnBetChange;
    }

    // Update is called once per frame
    void OnBetChange(int BetOrHands)
    {
        GetComponent<Text>().text = (VpMachine.Bet * VpMachine.HandsPlayed).ToString();
    }
}
