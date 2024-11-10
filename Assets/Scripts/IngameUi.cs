using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameUi : MonoBehaviour
{

    public TextMeshProUGUI levelText;

    private void LateUpdate()
    {

        levelText.text =$"Difficulty: {GameHardness.level.ToString()}";

    }

}
