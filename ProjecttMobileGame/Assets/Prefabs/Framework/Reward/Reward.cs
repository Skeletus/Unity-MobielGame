using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Reward
{
    public int healthReward;
    public int creditReward;
    public int staminaReward;
}

public interface IRewardListener
{
    public void Reward(Reward reward);
}
