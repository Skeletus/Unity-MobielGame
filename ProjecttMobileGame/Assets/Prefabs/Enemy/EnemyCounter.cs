using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    static int EnemyCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        ++EnemyCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        --EnemyCount;
        if (EnemyCount <= 0)
        {
            LevelManager.LevelFinished();
        }
    }
}
