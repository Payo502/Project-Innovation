﻿using FMODUtilityPackage.Audioplayers.UnityFunctionHandlers.BaseClasses;
using FMODUtilityPackage.Structs;
using SerializableDictionaryPackage.SerializableDictionary;
using UnityEngine;
using UtilityPackage.Utility.UnityFunctionHandlers.Enums;

namespace FMODUtilityPackage.Audioplayers.UnityFunctionHandlers.AudioPlayerFunctionHandlerExtras
{
	/// <summary>
	///     Sets parameters to the <see cref="AudioPlayerFunctionHandler" /> on given
	///     <see cref="UtilityPackage.Utility.UnityFunctionHandlers.Enums.UnityFunction" />s
	/// </summary>
	public class AudioParametersFunctionHandler : AbstractAudioFunctionHandler
    {
        [SerializeField] private AudioPlayerFunctionHandler audioPlayerFunctionHandler;

        [SerializeField] private SerializableEnumDictionary<UnityFunction, EventParameters> parameters;

        private void Reset()
        {
            audioPlayerFunctionHandler = GetComponent<AudioPlayerFunctionHandler>();
        }

        protected override void ReactToEvent(UnityFunction unityFunction)
        {
            var eventInstance = audioPlayerFunctionHandler.AudioEventInstance;

            foreach (var pair in parameters[unityFunction]) eventInstance.setParameterByName(pair.Key, pair.Value);
        }
    }
}