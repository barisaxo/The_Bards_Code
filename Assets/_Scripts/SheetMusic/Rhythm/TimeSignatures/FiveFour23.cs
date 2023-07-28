using System.Collections.Generic;
using UnityEngine;

namespace SheetMusic.Rhythms
{
    public class FiveFour23 : Time
    {
        public FiveFour23() { Signature = TimeSignature.FiveFour23; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(DupQuarter);

                        cells.Add(TripQuarter);
                        break;

                    case SubDivisionTier.BeatAndD1:
                        cells.Add(Random.value > .5f ? DupQuarter : QuadEighth);

                        if (Random.value > .5f)
                        {
                            cells.Add(TripQuarter);
                        }
                        else
                        {
                            cells.Add(DupEighth);
                            cells.Add(DupEighth);
                            cells.Add(DupEighth);
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        cells.Add(QuadEighth);

                        cells.Add(DupEighth);
                        cells.Add(DupEighth);
                        cells.Add(DupEighth);
                        break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }
        }
        RhythmCell TripQuarter => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.Beat)
                                    .SetQuantizement(Quantizement.Quarter)
                                    .SetRhythmicShape(this.RandomTripCell());
        RhythmCell DupQuarter => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.Beat)
                                    .SetQuantizement(Quantizement.Quarter)
                                    .SetRhythmicShape(this.RandomDupleCell());
        RhythmCell QuadEighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomQuadCell());
        RhythmCell DupEighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomDupleCell());
    }
}