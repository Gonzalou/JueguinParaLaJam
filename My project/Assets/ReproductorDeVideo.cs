using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ReproductorDeVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Asigna el target texture del VideoPlayer al RawImage
        videoPlayer.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        rawImage.texture = videoPlayer.targetTexture;

        // Reproduce el video
        videoPlayer.Play();
    }
}
