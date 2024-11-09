using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHardness :MonoBehaviour
{
    public static int level = 1;


    public int debugLevelVisualValue;

    private void Update()
    {
        debugLevelVisualValue = level;
    }
}
