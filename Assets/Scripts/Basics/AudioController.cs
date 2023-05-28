using UnityEngine;

namespace Basics
{
    public static class AudioController
    {
        public static void PlayOneTimeSfx(AudioSource audioSource)
        {
            if (audioSource.isPlaying)
            {
                return;
            }

            audioSource.PlayOneShot(audioSource.clip);
        }

        public static void PlayOnLoopSfx(AudioSource audioSource)
        {
            if (audioSource.isPlaying)
            {
                return;
            }
    
            audioSource.Play();
        }

        public static void StopSfx(AudioSource audioSource)
        { 
            if (!audioSource.isPlaying)
            {
                return;
            }

            audioSource.Stop();
        }
    }
}