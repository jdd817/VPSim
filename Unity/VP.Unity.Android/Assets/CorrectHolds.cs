using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectHolds : MonoBehaviour
{
    public GameObject[] HoldSprites;

    // Start is called before the first frame update
    void Start()
    {
        VpMachine.OnHandDealt += OnDeal;
        VpMachine.OnCardsDrawn += OnDraw;
    }

    // Update is called once per frame
    void Update()
    { 
    }

    public void OnDeal(VpMachine.DealEventArgs e)
    {
        foreach (var sprite in HoldSprites)
            sprite.SetActive(false);
    }

    public void OnDraw(VpMachine.DrawEventArgs e)
    {
        for (var i = 0; i < 5; i++)
            HoldSprites[i].SetActive(e.CorrectHolds[i]);
    }
}
