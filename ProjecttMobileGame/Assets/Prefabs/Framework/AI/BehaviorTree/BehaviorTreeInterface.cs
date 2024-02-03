using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BehaviorTreeInterface
{
    public void RotateTowards(GameObject target, bool vertialAim = false);
}
