using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MaxBetButton : MonoBehaviour, ITouchable
{
    public GameObject MaxBetSprite;

    public void Touched(Vector2 touch)
    {
        VpMachine.MaxBet();
    }

    public BoxCollider2D Collider => GetComponent<BoxCollider2D>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
