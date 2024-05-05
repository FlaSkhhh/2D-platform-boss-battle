using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider volume;

    void Start()
    {
        volume.value = 0.05f;
    }

    void Update()
    {
        volume.onValueChanged.AddListener(delegate { VolumeChange(); });
    }

    void VolumeChange()
    {
        GetComponent<AudioManager>().Volume(volume.value);
    }
}
