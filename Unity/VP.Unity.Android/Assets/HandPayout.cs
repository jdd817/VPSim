using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class HandPayout : Payout
{
    public int HandIndex;

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
        foreach (var sprite in PayLineSprites)
            sprite.SetActive(false);
    }

    public void OnDraw(VpMachine.DrawEventArgs e)
    {
        if (e.HandResults.Length > HandIndex)
        {
            SetActivePayline(e.HandResults[HandIndex]);
        }
    }
}

