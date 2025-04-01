using UnityEngine;

public class BGMController : MonoBehaviour
{
    public AudioSource bgmSource;
    public float startingPitch = 1f;
    public float maxPitch = 1.5f;

    public void PlayFromStart()
    {
        bgmSource.pitch = startingPitch;
        bgmSource.time = 0f;
        bgmSource.Play();
    }

    public void Stop()
    {
        bgmSource.Stop();
    }

    public void UpdatePitch(float timer, float totalTime)
    {
        float timeRatio = Mathf.Clamp01(timer / totalTime);
        bgmSource.pitch = Mathf.Lerp(maxPitch, startingPitch, timeRatio);
    }
}
