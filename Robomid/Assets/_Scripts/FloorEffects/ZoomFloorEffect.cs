using UnityEngine;

public class ZoomFloorEffect : MonoBehaviour
{

    void Update()
    {
        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, 2, 0.125f * Time.deltaTime);
    }

}
