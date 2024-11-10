using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingChanger : MonoBehaviour
{
    [SerializeField] Map map;
    PlayerMovement player;
    ChasingEnemy enemy;
    [SerializeField] float weaksetEffectAt = 5f;
    float distance;
    float t;
   

    public PostProcessVolume volume;

    ChromaticAberration chromatic;


    float minChromatic;
    [SerializeField]
    [Range(0, 1)]
    float maxChromatic;
    // Start is called before the first frame update


    void Awake()
    {
        player = map.player;
        enemy = map.enemy;
        volume.profile.TryGetSettings<ChromaticAberration>(out chromatic);
        minChromatic = chromatic.intensity;
    }

    void UpdateDistance()
    {
        distance = player.corridors.Count * (player.transform.position - enemy.transform.position).magnitude;
        if (distance == 0)
        {
            distance = (player.transform.position - enemy.transform.position).magnitude;
        }
       
        if (distance <= 0)
        {
            t = 0;
        }
        else
        {
            t = distance / weaksetEffectAt;
            if (t > 1) t = 1;
        }
        print(t);

    }

    // Update is called once per frame
    void Update()
    {
        if(player == null ||enemy == null)
        {
            Debug.LogError("Sockor errror: player or enemy missing from " + this);
        }
        UpdateDistance();
       if (chromatic != null)
        {
           
            chromatic.intensity.value = Mathf.Lerp(maxChromatic, minChromatic,t);
           
        }
    }
}
