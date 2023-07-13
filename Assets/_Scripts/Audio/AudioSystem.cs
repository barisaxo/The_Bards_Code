using System.Collections;
using UnityEngine;
using System.Threading.Tasks;

namespace Audio
{
    public abstract class AudioSystem
    {
        public AudioSystem(int numOfAudioSources, string name)
        {
            _numOfAudioSources = numOfAudioSources;
            _name = name;
        }

        protected double realTime;
        protected double dspTime;

        private readonly int _numOfAudioSources;
        private readonly string _name;

        protected GameObject _parent;
        public virtual GameObject Parent
        {
            get
            {
                if (_parent != null) { _parent.SetActive(true); }
                return _parent != null ? _parent : _parent = new GameObject(_name);
            }
        }

        public bool Running;

        protected bool _muted = false;
        public virtual bool Muted
        {
            get => _muted;
            set
            {
                _muted = value;
                CurrentVolumeLevel = value ? 0 : VolumeLevelSetting;
            }
        }

        protected bool _loop = false;
        public virtual bool Loop
        {
            get => _loop;
            set
            {
                _loop = value;
                foreach (AudioSource a in AudioSources) { a.loop = value; }
            }
        }

        protected int _cuedAudioSource;
        protected int _cuedAudioClip;
        protected int _cuedStartTime;

        private float _volumeLevelSetting = .6f;
        public virtual float VolumeLevelSetting
        {
            get => _volumeLevelSetting;
            set { _volumeLevelSetting = value; CurrentVolumeLevel = value; }
        }

        private float _currentVolumeLevel = .6f;
        public float CurrentVolumeLevel
        {
            get => _currentVolumeLevel;
            set
            {
                _currentVolumeLevel = value;

                if (_audioSources != null)
                {
                    foreach (AudioSource a in AudioSources) { a.volume = value; }
                }
            }
        }

        protected double NextEventTime { get; set; }

        public virtual AudioClipSettings? AudioClipSettings { get; set; }

        private AudioSource[] _audioSources = null;
        public virtual AudioSource[] AudioSources
        {
            get
            {
                return _audioSources ??= SetUpASs();

                AudioSource[] SetUpASs()
                {
                    AudioSource[] audioSources = new AudioSource[_numOfAudioSources];
                    for (int i = 0; i < _numOfAudioSources; i++)
                    {
                        GameObject child = new(nameof(AudioSource) + i);
                        child.transform.SetParent(Parent.transform);

                        audioSources[i] = child.AddComponent<AudioSource>();
                        audioSources[i].loop = false;
                        audioSources[i].playOnAwake = false;
                        audioSources[i].volume = VolumeLevelSetting;
                    }
                    return audioSources;
                }
            }
        }

        public virtual void ResetCues()
        {
            NextEventTime = AudioSettings.dspTime + .5D;
            _cuedAudioSource = 0;
            _cuedAudioClip = 0;
        }

        public virtual void Play(bool isSerial)
        {
            _ = Parent;
            CurrentVolumeLevel = 0f;
            ResetCues();
            Running = true;
            if (isSerial) { SerialAudioClipsUpdateLoop(); }
            foreach (AudioSource a in AudioSources) a.Play();
            PlayAndFadeIn();

            async void PlayAndFadeIn()
            {
                if (!Application.isPlaying) return;

                await Task.Yield();

                if (CurrentVolumeLevel < VolumeLevelSetting)
                {
                    CurrentVolumeLevel += Time.deltaTime * 1.75f;
                    PlayAndFadeIn();
                }
                else
                {
                    CurrentVolumeLevel = VolumeLevelSetting;
                }
            }
        }

        public virtual void Stop()
        {
            FadeOutAndStop();
        }

        async void FadeOutAndStop()
        {

            while (CurrentVolumeLevel > .2f)
            {
                if (!Application.isPlaying) return;

                await Task.Yield();
                CurrentVolumeLevel -= Time.deltaTime * 5f;
            }
            CurrentVolumeLevel = 0;
            foreach (AudioSource a in AudioSources) a.Stop();
            Running = false;
            Destruct();
        }

        public virtual async void SerialAudioClipsUpdateLoop()
        {
            while (Running)
            {
                if (!Application.isPlaying) return;

                double time = AudioSettings.dspTime;
                if (time + 1.00D > NextEventTime)
                {
                    AudioSources[_cuedAudioSource].clip = AudioClipSettings?.AudioClips[_cuedAudioClip];
                    AudioSources[_cuedAudioSource].time = (float)AudioClipSettings?.StartTimes[_cuedStartTime] * (float)AudioClipSettings?.AudioClips[0].length;
                    AudioSources[_cuedAudioSource].PlayScheduled(NextEventTime);

                    NextEventTime += 60.00D / (double)AudioClipSettings?.BPM * (double)AudioClipSettings?.BeatsPerAudioClip;
                    AudioSources[_cuedAudioSource].SetScheduledEndTime(NextEventTime);

                    if (++_cuedAudioSource == AudioSources.Length) { _cuedAudioSource = 0; }
                    if (++_cuedAudioClip == AudioClipSettings?.AudioClips.Length) { _cuedAudioClip = 0; }
                    if (++_cuedStartTime == AudioClipSettings?.StartTimes.Length) { _cuedStartTime = 0; }
                }
                await Task.Yield();
            }
        }

        private void Destruct()
        {
            _audioSources = null;
            AudioClipSettings = null;
            Object.DestroyImmediate(Parent);
        }
    }

    public struct AudioClipSettings
    {
        public float BPM;
        public float[] StartTimes;
        public float BeatsPerAudioClip;
        public AudioClip[] AudioClips;
    }
}