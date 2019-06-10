using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DealButton : MonoBehaviour, ITouchable
{
    public GameObject DrawSprite;
    public GameObject DealSprite;

    public void Touched(Vector2 touch)
    {
        if (!VpMachine.Deal())
            Handheld.Vibrate();
    }

    public BoxCollider2D Collider => GetComponent<BoxCollider2D>();


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
        DealSprite.SetActive(false);
        DrawSprite.SetActive(true);
    }

    public void OnDraw(VpMachine.DrawEventArgs e)
    {
        DealSprite.SetActive(true);
        DrawSprite.SetActive(false);
    }
}
