using System.Collections.Generic;
using UnityEngine;

namespace SheetMusic.Rhythms
{
    public class ThreeTwo : Time
    {
        public ThreeTwo() { Signature = TimeSignature.ThreeTwo; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(Half);
                        break;

                    case SubDivisionTier.BeatAndD1:
                        if (Random.value > .5f)
                        {
                            cells.Add(Quarter);
                            cells.Add(Quarter);
                            cells.Add(Quarter);
                        }
                        else
                        {
                            cells.Add(Half);
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        cells.Add(Quarter);
                        cells.Add(Quarter);
                        cells.Add(Quarter);
                        break;

                    case SubDivisionTier.D1AndD2:
                        cells.Add(Random.value > .5f ? Quarter : Eighth);
                        cells.Add(Random.value > .5f ? Quarter : Eighth);
                        cells.Add(Random.value > .5f ? Quarter : Eighth);
                        break;

                    case SubDivisionTier.D2Only:
                        cells.Add(Eighth);
                        cells.Add(Eighth);
                        cells.Add(Eighth);
                        break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }
        }

        RhythmCell Half => new RhythmCell()
                            .SetMetricLevel(MetricLevel.Beat)
                            .SetQuantizement(Quantizement.Half)
                            .SetRhythmicShape(this.RandomTripCell());
        RhythmCell Quarter => new RhythmCell()
                                .SetMetricLevel(MetricLevel.D1)
                                .SetQuantizement(Quantizement.Quarter)
                                .SetRhythmicShape(this.RandomDupleCell());
        RhythmCell Eighth => new RhythmCell()
                                .SetMetricLevel(MetricLevel.Beat)
                                .SetQuantizement(Quantizement.Eighth)
                                .SetRhythmicShape(this.RandomQuadCell());
    }
}
