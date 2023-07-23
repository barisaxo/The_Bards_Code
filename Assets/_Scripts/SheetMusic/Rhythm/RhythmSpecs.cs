using System.Collections.Generic;

namespace SheetMusic.Rhythms
{
    public class RhythmSpecs
    {
        public float Tempo = 80;
        public int NumberOfMeasures = 4;
        public TimeSignature TimeSignature = TimeSignature.FourFour;
        public BeatDivisor Meter = BeatDivisor.Simple;
        public MetricLevel MetricLevel = MetricLevel.Beat;

        public bool HasTies;
        public bool HasRests;
        public bool HasTriplets;

        public SubDivisionTier SubDivisionTier = SubDivisionTier.QuartersOnly;
        public List<RhythmOption> RhythmOptions = new();

        public RhythmSpecs SetTempo(float tempo) { Tempo = tempo; return this; }
        public RhythmSpecs SetNumberOfMeasures(int numberOfMeasures) { NumberOfMeasures = numberOfMeasures; return this; }
        public RhythmSpecs SetTimeSignature(TimeSignature timeSignature) { TimeSignature = timeSignature; return this; }
        public RhythmSpecs SetSubDivision(SubDivisionTier tier) { SubDivisionTier = tier; return this; }
        public RhythmSpecs SetMeter(BeatDivisor meter) { Meter = meter; return this; }
        public RhythmSpecs SetMetricLevel(MetricLevel level) { MetricLevel = level; return this; }
        public RhythmSpecs AddRhythmOption(RhythmOption option) { RhythmOptions.Add(option); return this; }
    }
}