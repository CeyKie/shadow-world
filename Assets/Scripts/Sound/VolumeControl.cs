using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Sound
{
    public class VolumeControl : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer audioMixer;
        
        [SerializeField]
        private string parameter;

        [SerializeField]
        private Slider slider;

        private void Awake()
        {
            var currentVolume = PlayerPrefs.GetFloat("MasterVolume");
            slider.onValueChanged.RemoveAllListeners();
            slider.onValueChanged.AddListener(ChangeVolume);
            slider.value = currentVolume;
            ChangeVolume(currentVolume);
        }

        private void ChangeVolume(float value)
        {
            audioMixer.SetFloat(parameter, Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat("MasterVolume", value);
        }
    }
}