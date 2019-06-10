using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PaylineTally : MonoBehaviour
{
    // Start is called before the first frame update
    public string PayLine;
    public GameObject Payout;
    public GameObject Hits;

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
        gameObject.SetActive(false);
    }

    public void OnDraw(VpMachine.DrawEventArgs e)
    {
        var hits = e.HandResults.Where(r => r.PayLineHit == PayLine).ToList();
        if(hits.Count>0)
        {
            Payout.GetComponent<Text>().text = hits[0].Payout.ToString();
            Hits.GetComponent<Text>().text = hits.Count.ToString();
            gameObject.SetActive(true);
        }
    }
}
