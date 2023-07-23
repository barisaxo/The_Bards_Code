
namespace SheetMusic.Rhythms
{
    public struct RhythmCell
    {
        public bool Rest;
        public bool Tied;
        public bool LongCell;
        public CellShape RhythmicShape;
        public Quantizement Quantizement;

        public RhythmCell SetRest(bool tf) { Rest = tf; return this; }
        public RhythmCell SetTied(bool tf) { Tied = tf; return this; }
        public RhythmCell SetLongCell(bool tf) { LongCell = tf; return this; }
        public RhythmCell SetRhythmicShape(CellShape shape) { RhythmicShape = shape; return this; }
        public RhythmCell SetQuantizement(Quantizement quantizement) { Quantizement = quantizement; return this; }
    }
}