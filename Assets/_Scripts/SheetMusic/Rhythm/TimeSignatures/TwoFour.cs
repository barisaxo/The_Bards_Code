using System.Collections.Generic;
using UnityEngine;

namespace SheetMusic.Rhythms
{
    public class TwoFour : Time
    {
        public TwoFour() { Signature = TimeSignature.TwoFour; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripQuarter : Quarter);
                        break;

                    case SubDivisionTier.BeatAndD1:
                        if (Random.value > .5f)
                        {
                            if (ms.RhythmSpecs.HasTriplets && Random.value > .5f)
                            {
                                cells.Add(TripEighth);
                                cells.Add(TripEighth);
                            }
                            else
                            {
                                cells.Add(Eighth);
                            }
                        }
                        else
                        {
                            cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripQuarter : Quarter);
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        if (ms.RhythmSpecs.HasTriplets && Random.value > .5f)
                        {
                            cells.Add(TripEighth);
                            cells.Add(TripEighth);
                        }
                        else
                        {
                            cells.Add(Eighth);
                        }
                        break;

                    case SubDivisionTier.D1AndD2:
                        if (Random.value > .5f)
                        {
                            cells.Add(Eighth);
                        }
                        else
                        {
                            cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth : Sixteenth);
                            cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth : Sixteenth);
                        }
                        break;

                    case SubDivisionTier.D2Only:
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
                                    .SetRhythmicShape(this.RandomDupleCell());
        RhythmCell TripQuarter => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.BeatT)
                                    .SetQuantizement(Quantizement.QuarterTrips)
                                    .SetRhythmicShape(this.RandomTripCellNoTL());
        RhythmCell Eighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomQuadCell());
        RhythmCell TripEighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1T)
                                    .SetQuantizement(Quantizement.EighthTrips)
                                    .SetRhythmicShape(this.RandomTripCellNoTL());
        RhythmCell Sixteenth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D2)
                                    .SetQuantizement(Quantizement.Sixteenth)
                                    .SetRhythmicShape(this.RandomQuadCell());
    }
}