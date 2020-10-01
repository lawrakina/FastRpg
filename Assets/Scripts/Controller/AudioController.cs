using Helper;
using UnityEngine;


namespace Controller
{
    public sealed class AudioController: BaseController
    {
        private AudioSource _audioMusic;
        private Transform _owner;
        public void Initialization(Transform transform)
        {
            _owner = transform;
            _audioMusic = ServiceLocator.Resolve<ThirdCameraController>().GetAudioSourceCamera();
            _audioMusic.loop = true;
            _audioMusic.transform.SetParent(_owner);
            _audioMusic.volume = 0.1f;
        }
    }
}