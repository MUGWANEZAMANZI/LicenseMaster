using UnityEngine;

public class MixAudio : MonoBehaviour
{
    [SerializeField] public AK.Wwise.RTPC MasterVolume;
    [SerializeField] public AK.Wwise.RTPC MusicVolume;
    [SerializeField] public AK.Wwise.RTPC SfxVolume;

    public void SetMasterVolume(float vol)
    {
        MasterVolume.SetGlobalValue(vol);
    }

    public void SetMusicVolume(float vol)
    {
        MusicVolume.SetGlobalValue(vol);
    }

    public void SetSfxVolume(float vol)
    {
        SfxVolume.SetGlobalValue(vol);
    }
}
