namespace SheetMusic.Rhythms
{
    //Whole note = 240 / BPM
    //Half note = 120 / BPM
    //Dotted quarter note = 90 / BPM
    //Quarter note = 60 / BPM
    //Dotted eighth note = 45 / BPM
    //Eighth note = 30 / BPM
    //Triplet eighth note = 20 / BPM
    //Sixteenth note = 15 / BPM
    //
    // when you quantize something, it undergoes quantization, and is now quantized.
    // 
    // quantizement is a things current state, or resolution, of quantization.
    // 
    // quanta (plural;  singular: quantum) is the smallest amount something possible can be measured at.
    //
    //     while technically the quantum of rhythmic resolution is infinite,
    //     the use of 64th notes is not unheard of.
    //     16th notes will be the smallest unit used in this game.

    //    One = 01 + 00, OneE = 04 + 00, OneT = 05 + 00, OneN = 07 + 00, OneL = 09 + 00, OneA = 10 + 00,
    //    Two = 01 + 12, TwoE = 04 + 12, TwoT = 05 + 12, TwoN = 07 + 12, TwoL = 09 + 12, TwoA = 10 + 12,
    //    Thr = 01 + 24, ThrE = 04 + 24, ThrT = 05 + 24, ThrN = 07 + 24, ThrL = 09 + 24, ThrA = 10 + 24,
    //    For = 01 + 36, ForE = 04 + 36, ForT = 05 + 36, ForN = 07 + 36, ForL = 09 + 36, ForA = 10 + 36
    //    //out of 48 (last 2 are space)
    //    //||1 . . e T . + . L a . . 2 . . e T . + . L a . . 3 . . e T . + . L a . . 4 . . e T . + . L a . . |ALL
    //    //||1 . . . 5 . . . . 10. . . . 5 . . . . 20. . . . 5 . . . . 30. . . . 5 . . . . 40. . . . 5 . . 48|spaces
    //    //||1 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . |WHOLE
    //    //||1 . . . . . . . . . . . . . . . . . . . . . . . 3 . . . . . . . . . . . . . . . . . . . . . . . |HALF
    //    //||1 . . . . . . . . . . . . . . . T . . . . . . . . . . . . . . . L . . . . . . . . . . . . . . . |TRIP QUARTER
    //    //||1 . . . . . . . . . . . 2 . . . . . . . . . . . 3 . . . . . . . . . . . 4 . . . . . . . . . . . |QUARTER
    //    //||1 . . . . . . . T . . . . . . . L . . . . . . . 3 . . . . . . . T . . . . . . . L . . . . . . . |TRIP QUARTER
    //    //||1 . . . . . + . . . . . 2 . . . . . + . . . . . 3 . . . . . + . . . . . 4 . . . . . + . . . . . |EIGHTH
    //    //||1 . . . T . . . L . . . 2 . . . T . . . L . . . 3 . . . T . . . L . . . 4 . . . T . . . L . . . |TRIP EIGHTH
    //    //||1 . . e . . + . . a . . 2 . . e . . + . . a . . 3 . . e . . + . . a . . 4 . . e . . + . . a . . |SIXTEENTH


    public enum Count { One = 1, Two = 2, Thr = 3, For = 4, Fiv = 5, Six = 6, Sev = 7, Eht = 8, Nin = 9, Ten = 10, Elv = 11, Tlv = 12 }
    public enum SubCount { One = 1, Two = 2, For = 4, Eht = 8 }
    public enum SubBeatAssignment
    {
        D = 1, E = 4, T = 5, N = 7, L = 9, A = 10
        //Down   e      tup    +      let    a

        //pass    the  god  damn  but-ter
        //|1  .  .  e  T  .  +  .  L  a  .  . 
        //|1  2  3  4  5  6  7  8  9  10 11 12 
    }
    public enum Quantizement { Quarter, QuarterTrips, Eighth, EighthTrips, Sixteenth }
    public enum CellShape { L, LS, LL, SL, LSS, SSL, SLS, SSSS, TLS, TSL, TSSS, TL, }
    public enum CellPosition { One = 1, Two = 2, Thr = 3, For = 4 }
    public enum RhythmicValue
    {
        Whole = 48, Half = 24, Quarter = 12, Eighth = 6, Sixteenth = 3,
        DotHalf = 36, DotQuarter = 18, DotEighth = 9,
        TripHalf = 16, TripQuarter = 8, TripEighth = 4, TripWhole = 32//?? not sure on trip whole
    }
    public enum RhythmOption { Ties, Rests, SomeTrips, TripsOnly }
    public enum SubDivisionTier { QuartersOnly, QuartersAndEighths, EighthsOnly, EighthsAndSixteenths, SixteenthsOnly, }
    public enum NoteFunction { Attack, Hold, Rest, Ignore }
    public enum MeasureNumber { One = 1, Two = 2, Thr = 3, For = 4 }

    public struct BeatLocation
    {
        public MeasureNumber MeasureNumber;
        public Count Count;
        public SubBeatAssignment SubBeatAssignment;
    }

    public struct TimeSignature
    {
        public Count Quantity;
        public SubCount Quality;
        public PulseStress PulseStress;
        public BeatDivisor BeatDivisor;

        public static TimeSignature TwoTwo => new() { Quantity = Count.Two, Quality = SubCount.Two, };
        public static TimeSignature ThreeTwo => new() { Quantity = Count.Thr, Quality = SubCount.Two, };
        public static TimeSignature TwoFour => new() { Quantity = Count.Two, Quality = SubCount.For };
        public static TimeSignature ThreeFour => new() { Quantity = Count.Thr, Quality = SubCount.For };
        public static TimeSignature FourFour => new() { Quantity = Count.For, Quality = SubCount.For };
        public static TimeSignature FiveFour => new() { Quantity = Count.Fiv, Quality = SubCount.For };
        public static TimeSignature ThreeEight => new() { Quantity = Count.Thr, Quality = SubCount.Eht };
        public static TimeSignature SixEight => new() { Quantity = Count.Six, Quality = SubCount.Eht };
        public static TimeSignature SevenEight => new() { Quantity = Count.Sev, Quality = SubCount.Eht };
        public static TimeSignature NineEight => new() { Quantity = Count.Nin, Quality = SubCount.Eht };
        public static TimeSignature TwelveEight => new() { Quantity = Count.Tlv, Quality = SubCount.Eht };
    }

    public enum MetricLevel { Beat, Division1, Division2 }

    public enum PulseStress { Duple, Triple, Quadruple, Quintuple, Septuple }
    public enum BeatDivisor { Simple, Compound, Irregular }

    public struct Meter
    {
        public BeatDivisor[] Divisor;
        public PulseStress[] Pulse;

        public static Meter SimpleDuple => new() { Divisor = new[] { BeatDivisor.Simple }, Pulse = new[] { PulseStress.Duple } };
        //public static Meter SimpleTriple => new() { Divisor = BeatDivisor.Simple, Pulse = PulseStress.Triple };
        //public static Meter SimpleQuadruple => new() { Divisor = BeatDivisor.Simple, Pulse = PulseStress.Quadruple };
        //public static Meter CompoundDuple => new() { Divisor = BeatDivisor.Compound, Pulse = PulseStress.Duple };
        //public static Meter CompoundTriple => new() { Divisor = BeatDivisor.Compound, Pulse = PulseStress.Triple };
        //public static Meter CompoundQuadruple => new() { Divisor = BeatDivisor.Compound, Pulse = PulseStress.Quadruple };

        public static Meter IrregularDupleTriple => new() { Divisor = new[] { BeatDivisor.Simple, BeatDivisor.Simple }, Pulse = new[] { PulseStress.Duple, PulseStress.Triple } };
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