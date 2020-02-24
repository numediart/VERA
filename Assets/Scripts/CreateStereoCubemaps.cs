using UnityEngine;
using UnityEngine.Rendering;
using System.IO;

//attach this script to your camera object
public class CreateStereoCubemaps : MonoBehaviour
{
    public RenderTexture cubemapLeft;
    public RenderTexture cubemapRight;
    public RenderTexture equirect;
    public bool renderStereo = true;
    public float stereoSeparation = 0.064f;

    void LateUpdate()
    {
        Camera cam = GetComponent<Camera>();

        if (cam == null)
        {
            cam = GetComponentInParent<Camera>();
        }

        if (cam == null)
        {
            Debug.Log("stereo 360 capture node has no camera or parent camera");
        }

        if (renderStereo)
        {
            cam.stereoSeparation = stereoSeparation;
            cam.RenderToCubemap(cubemapLeft, 63, Camera.MonoOrStereoscopicEye.Left);
            cam.RenderToCubemap(cubemapRight, 63, Camera.MonoOrStereoscopicEye.Right);
        }
        else
        {
            cam.RenderToCubemap(cubemapLeft, 63, Camera.MonoOrStereoscopicEye.Mono);
        }

        //optional: convert cubemaps to equirect

        if (equirect == null)
        {
            return;
        }

        if (renderStereo)
        {
            cubemapLeft.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Left);
            cubemapRight.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Right);
        }
        else
        {
            cubemapLeft.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Mono);
        }
        Texture2D tex = new Texture2D(2048, 2048, TextureFormat.RGB24, false);
        RenderTexture.active = equirect;
        tex.ReadPixels(new Rect(0, 0, equirect.width, equirect.height), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToPNG();

        var file = File.Open("C:\\Users\\victo\\Documents\\Background.png", FileMode.Create);
        var binary = new BinaryWriter(file);
        binary.Write(bytes);
        file.Close();
    }
}