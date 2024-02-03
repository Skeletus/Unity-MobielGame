using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorator : BT_Node
{
    BT_Node child;

    protected BT_Node GetChild()
    {
        return child;
    }

    public Decorator(BT_Node child)
    {
        this.child = child;
    }
}
