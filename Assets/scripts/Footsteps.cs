using UnityEngine;
using System.Collections;
using FMODUtilityPackage.Core;
using FMODUtilityPackage.Enums;
using FMOD.Studio;
public class Footsteps : MonoBehaviour
{

    public FMODUnity.EventReference m_EventPath;


    void PlayFootstepSound()
    {
        EventInstance e = AudioPlayer.GetEventInstance(AudioEventType.MC_Footsteps);
        AudioPlayer.PlayOneShot3D(AudioEventType.MC_Footsteps, gameObject);
    }

    void PlayGuardFootstepSound()
    {
        EventInstance e = AudioPlayer.GetEventInstance(AudioEventType.Footsteps);
        AudioPlayer.PlayOneShot3D(AudioEventType.Footsteps, gameObject);
    }
}