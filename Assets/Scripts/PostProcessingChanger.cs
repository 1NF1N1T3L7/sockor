using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingChanger : MonoBehaviour
{

    [SerializeField] PlayerMovement player;
    [SerializeField] ChasingEnemy enemy;
    [SerializeField] float weaksetEffectAt = 5f;
    float distance;
    float t;
   

    public PostProcessVolume volume;

    ChromaticAberration chromatic;

    [SerializeField]
    [Range(0,1)]
    float minChromatic;
    [SerializeField]
    [Range(0, 1)]
    float maxChromatic;
    // Start is called before the first frame update


    void Start()
    {
        volume.profile.TryGetSettings<ChromaticAberration>(out chromatic);

        
        
    }

    void UpdateDistance()
    {
        distance = player.corridors.Count * (player.transform.position - enemy.transform.position).magnitude;
        if (distance == 0)
        {
            distance = (player.transform.position - enemy.transform.position).magnitude;
        }
        t = distance / weaksetEffectAt;
        if(t > 1)  t = 1; 

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistance();
       if (chromatic != null)
        {
            chromatic.intensity.value = Mathf.Lerp(minChromatic,maxChromatic, t );
        }
    }
}
