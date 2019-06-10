using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface ITouchable
{
    void Touched(Vector2 touch);

    BoxCollider2D Collider { get; }
}

