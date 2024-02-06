using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorTree : MonoBehaviour
{
    BT_Node Root;

    BlackBoard blackboard = new BlackBoard();

    BehaviorTreeInterface behaviorTreeInterface;

    public BlackBoard Blackboard
    {
        get { return blackboard; }
    }

    // Start is called before the first frame update
    void Start()
    {
        behaviorTreeInterface = GetComponent<BehaviorTreeInterface>();
        ConstructTree(out Root);
        SortTree();
    }

    internal BehaviorTreeInterface GetBehaviorTreeInterface()
    {
        return behaviorTreeInterface;
    }

    private void SortTree()
    {
        int priortyCounter = 0;
        Root.Initialize();
        Root.SortPriority(ref priortyCounter);
    }

    protected abstract void ConstructTree(out BT_Node rootNode);

    // Update is called once per frame
    void Update()
    {
        Root.UpdateNode();
    }

    public void AbortLowerThan(int priority)
    {
        BT_Node currentNode = Root.Get();
        if (currentNode.GetPriority() > priority)
        {
            Root.Abort();
        }
    }
}
