using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayStatics : MonoBehaviour
{
    public static void SetGamePaused(bool paused)
    {
        Time.timeScale = paused ? 0 : 1;
    }
}
