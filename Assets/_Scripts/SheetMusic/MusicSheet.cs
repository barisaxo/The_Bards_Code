using SheetMusic.Rhythms;
using UnityEngine;
using System.Collections.Generic;

namespace SheetMusic
{
    public class MusicSheet
    {
        public void SelfDestruct()
        {
            Object.Destroy(_parent.gameObject);
        }
        //public IGenerateRhythmCells TimeSignature;
        public RhythmSpecs RhythmSpecs;
        //public RhythmCell[][] Measures;
        public Measure[] Measures;
        public List<Note> Notes;

        public List<MappedBeat> BeatMap;

        private Transform _parent;
        public Transform Parent => _parent != null ? _parent : _parent = new GameObject(nameof(MusicSheet)).transform;

        public Card[] ScribedStaves;
        public Card[] ScribedNotes;
        public Card[] ScribedCounts;

        private Card _bg;
        private Card BG => _bg ??= new Card(nameof(BG), Parent)
            .SetImageSprite(Assets.White)
            .SetImageColor(new Color(0, 0, 0, .65f))
            .SetImageSize(new Vector3(12, 6))
            .SetImagePosition(new Vector3(0, .5f, 3));
    }
}