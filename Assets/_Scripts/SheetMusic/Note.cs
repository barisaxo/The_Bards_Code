using SheetMusic.Rhythms;

namespace SheetMusic
{
    public struct Note
    {
        public bool Rest;
        public bool Tied;
        public bool Trip;

        public RhythmCell ParentCell;
        public BeatLocation BeatLocation;
        public RhythmicValue QuantizedRhythmicValue;

        public Note(
            bool rest,
            bool tied,
            bool trip,
            RhythmCell parentCell,
            BeatLocation beatLocation,
            RhythmicValue rhythmicNoteValue)
        {
            Rest = rest;
            Tied = tied;
            Trip = trip;
            ParentCell = parentCell;
            BeatLocation = beatLocation;
            QuantizedRhythmicValue = rhythmicNoteValue;
        }
    }
}

