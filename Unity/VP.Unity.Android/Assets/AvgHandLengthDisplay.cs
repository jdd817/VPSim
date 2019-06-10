using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvgHandLengthDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VpMachine.OnCardsDrawn += OnDraw;
        GetComponent<Text>().text = VpMachine.statistics.AvgHandLength.ToString("0.00");
    }

    // Update is called once per frame
    void OnDraw(VpMachine.DrawEventArgs e)
    {
        GetComponent<Text>().text = VpMachine.statistics.AvgHandLength.ToString("0.00");
    }
}
