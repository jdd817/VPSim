using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject[] Cards;
    public GameObject Payout;
    public int HandIndex;

    // Start is called before the first frame update
    void Start()
    {
        Payout.GetComponent<HandPayout>().HandIndex = HandIndex;
        VpMachine.OnHandDealt += OnDeal;
        VpMachine.OnCardsDrawn += OnDraw;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDeal(VpMachine.DealEventArgs e)
    {
        foreach (var card in Cards)
            card.GetComponent<Card>().SetCard(0, 0);
    }

    public void OnDraw(VpMachine.DrawEventArgs e)
    {
        if (e.HandResults.Length > HandIndex)
            for (var i = 0; i < 5; i++)
                Cards[i].GetComponent<Card>().SetCard(e.HandResults[HandIndex].Hand[i]);
    }
}
