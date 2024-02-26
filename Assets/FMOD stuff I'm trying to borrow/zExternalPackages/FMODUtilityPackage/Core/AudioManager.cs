using System.Collections.Generic;
using FMODUnity;
using FMODUtilityPackage.Enums;
using FMODUtilityPackage.Structs;
using UnityEngine;
using VDFramework.Singleton;
using VDFramework.Utility;

namespace FMODUtilityPackage.Core
{
	/// <summary>
	///     A Singleton that allows setting the EventReferences, BusPaths and Bus volumes in the inspector
	/// </summary>
	public class AudioManager : Singleton<AudioManager>, ISerializationCallbackReceiver
    {
        public EventPaths EventPaths;

        [SerializeField] private List<InitialVolumePerBus> initialVolumes = new();

        protected override void Awake()
        {
            /*
             * DontDestroyOnLoad Singletons usually don't play nice when placed in a scene manually
             * This is because if the same scene would be reloaded the singleton would exist twice
             * We catch such a case before the Baseclass notices and destroy ourselves if we are the duplicate
             * (This prevents the SingletonViolationException from being thrown)
             */
            if (IsInitialized)
            {
                Destroy(this);
                return;
            }

            if (EventPaths ==
                null) // If EventPaths is null, the AudioManager was not present in the scene already (because otherwise the field would be deserialised)
            {
#if UNITY_EDITOR
                Debug.Log(
                    $"Add an {nameof(AudioManager)} to the scene manually for more control over the bus volumes at the start.");

                if (!EventManager.IsInitialized) //EventManager is an editor script
                    EventManager.Startup();
#endif

                EventPaths = new EventPaths(true);
                OnBeforeSerialize();
            }

            base.Awake();
            EventPaths.AddEmitters(gameObject);

            if (!transform.parent) DontDestroyOnLoad(true);

            SetInitialVolumes();
        }

        private void Reset()
        {
            for (var i = 0; i < initialVolumes.Count; i++) initialVolumes[i] = InitialVolumePerBus.DefaultValue;
        }

        public void OnBeforeSerialize()
        {
            var countBeforeResize = initialVolumes.Count;
            EnumDictionaryUtil.PopulateEnumDictionary<InitialVolumePerBus, BusType, float>(initialVolumes);
            var countAfterResize = initialVolumes.Count;

            if (countAfterResize >
                countBeforeResize) // If we have more values now then we had before, initialize the new ones with default values
                for (var i = countBeforeResize; i < countAfterResize; i++)
                {
                    var initialVolumePerBus = initialVolumes[i];

                    initialVolumes[i] =
                        new InitialVolumePerBus(initialVolumePerBus.Key, 1, initialVolumePerBus.isMuted);
                }
        }

        public void OnAfterDeserialize()
        {
        }

        private void SetInitialVolumes()
        {
            foreach (var pair in initialVolumes)
            {
                if (pair.Key == default) // Use default so people can freely rename the enum value
                {
                    AudioParameterManager.SetMasterVolume(pair.Value);
                    AudioParameterManager.SetMasterMute(pair.isMuted);
                    continue;
                }

                var busPath = EventPaths.GetPath(pair.Key);
                AudioParameterManager.SetBusVolume(busPath, pair.Value);
                AudioParameterManager.SetBusMute(busPath, pair.isMuted);
            }
        }

        public void SetMasterVolume(float volume)
        {
            AudioParameterManager.SetMasterVolume(volume);
        }

        public float GetMasterVolume()
        {
            return AudioParameterManager.GetBusVolume(EventPaths.MASTER_BUS_PATH);
        }

        public void SetVolume(BusType busType, float volume)
        {
            AudioParameterManager.SetBusVolume(EventPaths.GetPath(busType), volume);
        }

        public float GetVolume(BusType busType)
        {
            return AudioParameterManager.GetBusVolume(EventPaths.GetPath(busType));
        }
    }
}