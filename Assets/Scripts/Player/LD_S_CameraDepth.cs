using UnityEngine;

[ExecuteInEditMode]
public class LD_S_CameraDepth : MonoBehaviour
{
    // propeties
    private Camera cam;

    void Start()
    {
        // gets the camera
        cam = GetComponent<Camera>();

        // applys depth texture mode to depth
        // can be pretty costy, may need to find a better way if to expensive
        cam.depthTextureMode = DepthTextureMode.Depth;
    }
}
