using System.Collections.Generic;
using UnityEngine;
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
    public enum Quantizement { Half, Quarter, QuarterTrips, Eighth, EighthTrips, Sixteenth }


    // Rhythm Cell Shapes:
    // Duple has 2 shapes: L & LL(or SS relatively)
    // Triple has 4 shapes: TL, TLS, TSL, TSSS
    // Quadruple has 8 shapes: L, LL, SL, LSS, SSL, SLS, SSSS.
    // The first two quadruple cells are not unqiue as they overlap with the duple cells.
    public enum CellShape { L, LL, LS, SL, LSS, SSL, SLS, SSSS, TLS, TSL, TSSS, TL, }


    public enum CellPosition { One = 1, Two = 2, Thr = 3, For = 4 }
    public enum RhythmicValue
    {
        Whole = 48, Half = 24, Quarter = 12, Eighth = 6, Sixteenth = 3,
        DotHalf = 36, DotQuarter = 18, DotEighth = 9,
        TripHalf = 16, TripQuarter = 8, TripEighth = 4, TripWhole = 32//?? not sure on trip whole
    }
    public enum RhythmOption { Ties, Rests, SomeTrips, TripsOnly }
    public enum SubDivisionTier { BeatOnly, BeatAndD1, D1Only, D1AndD2, D2Only, }
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
        public Meter Meter;
        public RhythmicValue BeatLevelValue;

        public static TimeSignature TwoTwo => new() { Quantity = Count.Two, Quality = SubCount.Two, Meter = Meter.SimpleDuple, BeatLevelValue = RhythmicValue.Half };
        public static TimeSignature ThreeTwo => new() { Quantity = Count.Thr, Quality = SubCount.Two, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Half };

        public static TimeSignature TwoFour => new() { Quantity = Count.Two, Quality = SubCount.For, Meter = Meter.SimpleDuple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignature ThreeFour => new() { Quantity = Count.Thr, Quality = SubCount.For, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignature FourFour => new() { Quantity = Count.For, Quality = SubCount.For, Meter = Meter.SimpleQuadruple, BeatLevelValue = RhythmicValue.Quarter };

        public static TimeSignature FiveFour23 => new() { Quantity = Count.Fiv, Quality = SubCount.For, Meter = Meter.IrregularDupleTriple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignature FiveFour32 => new() { Quantity = Count.Fiv, Quality = SubCount.For, Meter = Meter.IrregularTripleDuple, BeatLevelValue = RhythmicValue.Quarter };

        public static TimeSignature SixFour => new() { Quantity = Count.Six, Quality = SubCount.For, Meter = Meter.CompoundDuple, BeatLevelValue = RhythmicValue.DotHalf };
        public static TimeSignature SevenFour43 => new() { Quantity = Count.Sev, Quality = SubCount.For, Meter = Meter.IrregularQuadrupleTriple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignature SevenFour34 => new() { Quantity = Count.Sev, Quality = SubCount.For, Meter = Meter.IrregularTripleDuple, BeatLevelValue = RhythmicValue.Quarter };

        public static TimeSignature ThreeEight => new() { Quantity = Count.Thr, Quality = SubCount.Eht, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignature FiveEight23 => new() { Quantity = Count.Fiv, Quality = SubCount.Eht, Meter = Meter.IrregularDupleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignature FiveEight32 => new() { Quantity = Count.Fiv, Quality = SubCount.Eht, Meter = Meter.IrregularTripleDuple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignature SixEight => new() { Quantity = Count.Six, Quality = SubCount.Eht, Meter = Meter.CompoundDuple, BeatLevelValue = RhythmicValue.DotQuarter };
        public static TimeSignature SevenEight43 => new() { Quantity = Count.Sev, Quality = SubCount.Eht, Meter = Meter.IrregularQuadrupleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignature SevenEight34 => new() { Quantity = Count.Sev, Quality = SubCount.Eht, Meter = Meter.IrregularQuadrupleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignature NineEight => new() { Quantity = Count.Nin, Quality = SubCount.Eht, Meter = Meter.CompoundTriple, BeatLevelValue = RhythmicValue.DotHalf };
        public static TimeSignature TwelveEight => new() { Quantity = Count.Tlv, Quality = SubCount.Eht, Meter = Meter.CompoundQuadruple, BeatLevelValue = RhythmicValue.DotHalf };

        public static bool operator ==(TimeSignature a, TimeSignature b) => a.Quality == b.Quality && a.Quantity == b.Quantity;
        public static bool operator !=(TimeSignature a, TimeSignature b) => a.Quality != b.Quality || a.Quantity != b.Quantity;
        public override readonly bool Equals(object obj) => obj is TimeSignature t && Quality == t.Quality && Quantity == t.Quantity;
        public override readonly int GetHashCode() => System.HashCode.Combine(Quality, Quantity);
    }

    //unnecessary to define is the 'Multiple Levels' as in the combined beat level eg half & whole notes in 4/4
    //D1 is the fist division level, eg 8th notes in 4/4
    //D1T is triplets at the first division, eg trip 8ths in 4/4
    //D2 is the second division level, eg 16th notes in 4/4.
    //There will be no need to go beyond D2 in this game.
    public enum MetricLevel { Beat, BeatT, D1, D1T, D2, }


    //The elements of Metric Structure
    public enum PulseStress { Duple, Triple, Quadruple }
    //Unnecessary to define is the 'Irregular' as it is a combination of simple & compound and is implied as such.
    public enum BeatDivisor { Simple, Compound }

    public struct Meter
    {
        public readonly BeatDivisor Divisor => Divisors[0];
        public readonly PulseStress Pulse => Pulses[0];

        public BeatDivisor[] Divisors;
        public PulseStress[] Pulses;

        public static Meter SimpleDuple => new() { Divisors = new[] { BeatDivisor.Simple }, Pulses = new[] { PulseStress.Duple } };// 2/2, 2/4, 2/8...
        public static Meter SimpleTriple => new() { Divisors = new[] { BeatDivisor.Simple, }, Pulses = new[] { PulseStress.Triple } };// 3/2, 3/4, 3/8...
        public static Meter SimpleQuadruple => new() { Divisors = new[] { BeatDivisor.Simple, }, Pulses = new[] { PulseStress.Quadruple } };// 4/2, 4/4, 4/8...

        public static Meter CompoundDuple => new() { Divisors = new[] { BeatDivisor.Compound, }, Pulses = new[] { PulseStress.Duple } };// 6/2, 6/4, 6/8
        public static Meter CompoundTriple => new() { Divisors = new[] { BeatDivisor.Compound, }, Pulses = new[] { PulseStress.Triple } };// 9/2, 9/4, 9/8
        public static Meter CompoundQuadruple => new() { Divisors = new[] { BeatDivisor.Compound, }, Pulses = new[] { PulseStress.Quadruple } };// 12/2, 12/4, 12/8

        public static Meter IrregularDupleTriple => new() { Divisors = new[] { BeatDivisor.Simple, BeatDivisor.Simple }, Pulses = new[] { PulseStress.Duple, PulseStress.Triple } };// 2 + 3
        public static Meter IrregularTripleDuple => new() { Divisors = new[] { BeatDivisor.Simple, BeatDivisor.Simple }, Pulses = new[] { PulseStress.Triple, PulseStress.Duple } };// 3 + 2
        public static Meter IrregularQuadrupleTriple => new() { Divisors = new[] { BeatDivisor.Simple, BeatDivisor.Simple }, Pulses = new[] { PulseStress.Quadruple, PulseStress.Triple } };// 4 + 3
        public static Meter IrregularTripleQuadruple => new() { Divisors = new[] { BeatDivisor.Simple, BeatDivisor.Simple }, Pulses = new[] { PulseStress.Triple, PulseStress.Quadruple } };// 3 + 4

        public static bool operator ==(Meter a, Meter b) => a.Divisors == b.Divisors && a.Pulses == b.Pulses;
        public static bool operator !=(Meter a, Meter b) => a.Divisors != b.Divisors || a.Pulses != b.Pulses;
        public override readonly bool Equals(object obj) => obj is Meter m && Divisors == m.Divisors && Pulses == m.Pulses;
        public override readonly int GetHashCode() => System.HashCode.Combine(Divisors, Pulses);
    }


    public struct Measure
    {
        public RhythmSpecs Specs;
        public RhythmCell[] Cells;

        public Measure SetSpecs(RhythmSpecs specs) { Specs = specs; return this; }
        public Measure SetCells(RhythmCell[] cells) { Cells = cells; return this; }
    }

    public static class RhythmUtilities
    {

        public static PulseStress GetBeatLevel(this (Meter, MetricLevel) mm) => BeatLevels.GetValueOrDefault(mm);

        public static Dictionary<(Meter, MetricLevel), PulseStress> BeatLevels => new Dictionary<(Meter, MetricLevel), PulseStress>
        {
            // 2/* time
            { (Meter.SimpleDuple, MetricLevel.Beat), PulseStress.Duple },
            { (Meter.SimpleDuple, MetricLevel.D1), PulseStress.Duple },
            { (Meter.SimpleDuple, MetricLevel.D1T), PulseStress.Triple},
            { (Meter.SimpleDuple, MetricLevel.D2), PulseStress.Quadruple},

            // 3/* time
            { (Meter.SimpleTriple, MetricLevel.Beat), PulseStress.Triple },
            { (Meter.SimpleTriple, MetricLevel.D1), PulseStress.Duple },
            { (Meter.SimpleTriple, MetricLevel.D1T), PulseStress.Triple},
            { (Meter.SimpleTriple, MetricLevel.D2), PulseStress.Quadruple},

            // 4/* time
            { (Meter.SimpleQuadruple, MetricLevel.Beat), PulseStress.Quadruple },
            { (Meter.SimpleQuadruple, MetricLevel.D1), PulseStress.Quadruple },
            { (Meter.SimpleQuadruple, MetricLevel.D1T), PulseStress.Triple},
            { (Meter.SimpleQuadruple, MetricLevel.D2), PulseStress.Quadruple},

            // 6/* time
            { (Meter.CompoundDuple, MetricLevel.Beat), PulseStress.Triple },
            { (Meter.CompoundDuple, MetricLevel.D1), PulseStress.Duple },
            { (Meter.CompoundDuple, MetricLevel.D1T), PulseStress.Triple},
            { (Meter.CompoundDuple, MetricLevel.D2), PulseStress.Quadruple},
            
            // 9/* time
            { (Meter.CompoundTriple, MetricLevel.Beat), PulseStress.Triple },
            { (Meter.CompoundTriple, MetricLevel.D1), PulseStress.Duple },
            { (Meter.CompoundTriple, MetricLevel.D1T), PulseStress.Triple},
            { (Meter.CompoundTriple, MetricLevel.D2), PulseStress.Quadruple},
            
            // 12/* time
            { (Meter.CompoundTriple, MetricLevel.Beat), PulseStress.Triple },
            { (Meter.CompoundTriple, MetricLevel.D1), PulseStress.Duple },
            { (Meter.CompoundTriple, MetricLevel.D1T), PulseStress.Triple},
            { (Meter.CompoundTriple, MetricLevel.D2), PulseStress.Quadruple},

        };

        public static Dictionary<(Meter, MetricLevel), int> NumberOfCells => new Dictionary<(Meter, MetricLevel), int>
        {
            // 2/* time
            { (Meter.SimpleDuple, MetricLevel.Beat), 1 },
            { (Meter.SimpleDuple, MetricLevel.D1), 2 },
            { (Meter.SimpleDuple, MetricLevel.D1T), 4},
            { (Meter.SimpleDuple, MetricLevel.D2), 4},
            
            // 3/* time
            { (Meter.SimpleTriple, MetricLevel.Beat),1 },
            { (Meter.SimpleTriple, MetricLevel.D1), 3 },
            { (Meter.SimpleTriple, MetricLevel.D1T), 6},
            { (Meter.SimpleTriple, MetricLevel.D2), 6},

            // 4/* time
            { (Meter.SimpleQuadruple, MetricLevel.Beat), 1 },
            { (Meter.SimpleQuadruple, MetricLevel.D1), 2 },
            { (Meter.SimpleQuadruple, MetricLevel.D1T), 4},
            { (Meter.SimpleQuadruple, MetricLevel.D2), 4},
        };

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
