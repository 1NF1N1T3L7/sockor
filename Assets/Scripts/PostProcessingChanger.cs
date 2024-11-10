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
    Grain grain;
    Vignette vignette;

    float minChromatic;
    float minGrain;
    float minVignette;
    [SerializeField]
    [Range(0, 1)]
    float maxChromatic;
    [SerializeField]
   
    float maxGrain;
    [SerializeField]
    [Range(0, 1)]
    float maxVignette;
    // Start is called before the first frame update


    void Awake()
    {
        player = map.player;
        enemy = map.enemy;
        volume.profile.TryGetSettings<ChromaticAberration>(out chromatic);
        volume.profile.TryGetSettings<Vignette>(out vignette);
        volume.profile.TryGetSettings<Grain>(out grain);
        minChromatic = chromatic.intensity;
        minGrain = grain.intensity;
        minVignette = vignette.intensity;
    }

    void UpdateDistance()
    {
        if (enemy.sleeping)
        {
            t = 1;
            return;
        }
        float dist = (player.transform.position - enemy.transform.position).magnitude;
        if(dist < 1)
        {
            distance = player.corridors.Count;
        }
        else
        {
            distance = player.corridors.Count * dist;
        }
        
        if (distance == 0)
        {
            distance = dist;
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
       vignette.intensity.value= Mathf.Lerp(maxVignette, minVignette, t);
        grain.intensity.value = Mathf.Lerp(maxGrain, minGrain, t);
    }
}
