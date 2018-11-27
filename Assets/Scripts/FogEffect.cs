using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FogEffect : MonoBehaviour {

    public Shader fog;

    // Checks whether the camera has components with this method and invoke them after rendering this scene
    void onRenderImage (RenderTexture source, RenderTexture destination) {
        // draw a full-screen quad with a shader that reads the source texture and outputs the sampled colors
        Graphics.Blit(source, destination);
    }
}
