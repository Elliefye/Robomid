using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteFloorEffect : MonoBehaviour
{

    void Update()
    {
        Camera.main.GetComponent<PostProcessVolume>().enabled = true;
    }
}
