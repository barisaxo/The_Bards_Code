namespace SheetMusic.Rhythms
{
    public abstract class Time
    {
        public TimeSignature Signature;
        public virtual void GenerateRhythmCells(MusicSheet ms) { }

        public static bool operator ==(Time a, Time b) => a.Signature.Quality == b.Signature.Quality && a.Signature.Quantity == b.Signature.Quantity;
        public static bool operator !=(Time a, Time b) => a.Signature.Quality != b.Signature.Quality || a.Signature.Quantity != b.Signature.Quantity;
        public override bool Equals(object obj) => obj is Time t && Signature.Quality == t.Signature.Quality && Signature.Quantity == t.Signature.Quantity;
        public override int GetHashCode() => System.HashCode.Combine(Signature.Quality, Signature.Quantity);

    }
}





//public enum CellShape { w, dhq, hh, qdh, hqq, qqh, qhq, qqqq, thq, tqh, tqqq, tw, }


//
//Whole note             = 64 : 240 / BPM
//Half note              = 32 : 120 / BPM
//Dotted quarter note    = 24 : 90 / BPM
//Quarter note           = 16 : 60 / BPM
//Dotted eighth note     = 12 : 45 / BPM
//Triplet quarter note   = 10 : ??
//Eighth note            =  8 : 30 / BPM
//Triplet eighth note    =  6 : 20 / BPM
//Sixteenth note         =  4 : 15 / BPM
//
