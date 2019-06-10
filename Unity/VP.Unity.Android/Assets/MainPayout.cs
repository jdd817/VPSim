using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPayout : Payout
{
    // Start is called before the first frame update
    void Start()
    {
        VpMachine.OnHandDealt += OnDeal;
        VpMachine.OnCardsDrawn += OnDraw;
    }

    // Update is called once per frame
    protected void Update()
    {
    }

    public void OnDeal(VpMachine.DealEventArgs e)
    {
        SetActivePayline(e.MainHandResult);
    }

    public void OnDraw(VpMachine.DrawEventArgs e)
    {
        SetActivePayline(e.MainHandResult);
    }
}
