using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Card))]
[RequireComponent(typeof(BoxCollider2D))]
public class HoldableCard : Card, ITouchable
{
    public GameObject Held;
    public GameObject Card;
    public int CardIndex;
    public bool isHeld { get; set; }

    public override void SetCard(int value, int suit)
    {
        Value = value;
        Suit = suit;
        Card.GetComponent<Card>().SetCard(value, suit);
    }

    public void Touched(Vector2 touch)
    {
        if (VpMachine.GameState == 2)
        {
            isHeld = !isHeld;
            Held.SetActive(isHeld);
            VpMachine.Holds[CardIndex] = isHeld;
        }
    }

    public BoxCollider2D Collider => GetComponent<BoxCollider2D>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isHeld!=VpMachine.Holds[CardIndex])
        {
            isHeld = VpMachine.Holds[CardIndex];
            Held.SetActive(isHeld);
        }
    }
}
