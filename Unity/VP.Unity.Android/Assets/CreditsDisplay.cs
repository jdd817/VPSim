using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VpMachine.OnCardsDrawn += OnDrawOrDeal;
        VpMachine.OnHandDealt += OnDrawOrDeal;
    }

    // Update is called once per frame
    void OnDrawOrDeal(VpMachine.DealEventArgs e)
    {
        GetComponent<Text>().text = VpMachine.Credits.ToString();
    }
}
