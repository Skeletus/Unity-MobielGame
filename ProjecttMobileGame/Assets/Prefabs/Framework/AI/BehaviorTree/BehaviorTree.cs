using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorTree : MonoBehaviour
{
    BT_Node Root;

    BlackBoard blackboard = new BlackBoard();

    public BlackBoard Blackboard
    {
        get { return blackboard; }
    }

    // Start is called before the first frame update
    void Start()
    {
        ConstructTree(out Root);
        SortTree();
    }

    private void SortTree()
    {
        int priortyCounter = 0;
        Root.SortPriority(ref priortyCounter);
    }

    protected abstract void ConstructTree(out BT_Node rootNode);

    // Update is called once per frame
    void Update()
    {
        Root.UpdateNode();
    }
}
