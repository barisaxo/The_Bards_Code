using System.Collections.Generic;
using UnityEngine;

namespace SheetMusic.Rhythms
{
    public class ThreeFour : Time
    {
        public ThreeFour() { Signature = TimeSignature.ThreeFour; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(Quarter);
                        break;

                    case SubDivisionTier.BeatAndD1:
                        if (Random.value > .5f)
                        {
                            cells.Add(Eighth);
                            cells.Add(Eighth);
                            cells.Add(Eighth);
                        }
                        else
                        {
                            cells.Add(Quarter);
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        cells.Add(Eighth);
                        cells.Add(Eighth);
                        cells.Add(Eighth);
                        break;

                    case SubDivisionTier.D1AndD2:
                        cells.Add(Random.value > .5f ? Eighth : Sixteenth);
                        cells.Add(Random.value > .5f ? Eighth : Sixteenth);
                        cells.Add(Random.value > .5f ? Eighth : Sixteenth);
                        break;

                    case SubDivisionTier.D2Only:
                        cells.Add(Sixteenth);
                        cells.Add(Sixteenth);
                        cells.Add(Sixteenth);
                        break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }
        }
        RhythmCell Quarter => new RhythmCell()
                            .SetMetricLevel(MetricLevel.Beat)
                            .SetQuantizement(Quantizement.Quarter)
                            .SetRhythmicShape(this.RandomTripCell());
        RhythmCell Eighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomDupleCell());
        RhythmCell Sixteenth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D2)
                                    .SetQuantizement(Quantizement.Sixteenth)
                                    .SetRhythmicShape(this.RandomQuadCell());
    }
}
