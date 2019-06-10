using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VpMachine.OnCardsDrawn += OnDraw;
    }

    // Update is called once per frame
    void OnDraw(VpMachine.DrawEventArgs e)
    {
        GetComponent<Text>().text = e.AmountWon.ToString(); 
    }
}
