using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public GameObject[] CardSprites;

    public int Suit { get; protected set; }
    public int Value { get; protected set; }

    protected int SpriteIndex { get { return Value > 0 ? Value == 14 ? Suit : (Value - 1) * 4 + Suit : 0; } }

    public virtual void SetCard(Hands.Entities.Card card)
    {
        SetCard(card.Value, card.Suit);
    }

    public virtual void SetCard(int value, int suit)
    {
        if (value != Value || suit != Suit)
        {
            CardSprites[SpriteIndex].SetActive(false);

            Value = value;
            Suit = suit;

            CardSprites[SpriteIndex].SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
