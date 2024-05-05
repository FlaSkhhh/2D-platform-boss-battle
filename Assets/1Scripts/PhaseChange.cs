using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseChange : MonoBehaviour
{
    public SpriteRenderer sword;
    public SpriteRenderer helmet;
    public SpriteRenderer face;
    public SpriteRenderer body;

    void PhaseBurn()
    {
        sword.color = new Color(0.38f, 0.38f, 0.38f,1f);
        helmet.color = new Color(0.38f, 0.38f, 0.38f, 1f);
        face.color = new Color(0.58f, 0.37f, 0f, 1f);
        body.color = new Color(0.377f, 0.343f, 0.343f, 1f);
    }
}

