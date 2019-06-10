using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Payout : MonoBehaviour
{
    public GameObject[] PayLineSprites;

    protected void SetActivePayline(Hands.HandResult result)
    {
        var paylineHit = result.PayLineHit.Replace(" ", "").ToLower();
        foreach (var sprite in PayLineSprites)
        {
            if (sprite.name.ToLower().Replace("paylines", "") == paylineHit)
                sprite.SetActive(true);
            else
                sprite.SetActive(false);
        }
    }
}