using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandsPlayedDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VpMachine.OnHandsPlayedChange += OnHandsPlayChange;
    }

    // Update is called once per frame
    void OnHandsPlayChange(int Hands)
    {
        GetComponent<Text>().text = Hands.ToString();
    }
}
