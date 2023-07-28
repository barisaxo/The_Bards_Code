using System.Collections.Generic;
using UnityEngine;

namespace SheetMusic.Rhythms
{
    public class FourFour : Time
    {
        public FourFour() { Signature = TimeSignature.FourFour; }

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
                        switch (Random.Range(1, ms.RhythmSpecs.HasTriplets ? 4 : 3))
                        {
                            case 1:
                                cells.Add(Quarter);
                                break;

                            case 2:
                                cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripQuarter : Eighth);
                                cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripQuarter : Eighth);
                                break;

                            case 3:
                                switch (Random.Range(0, 3))
                                {
                                    case 0:
                                        cells.Add(TripEighth);
                                        cells.Add(TripEighth);
                                        cells.Add(Random.value > .5f ? TripQuarter : Eighth);
                                        break;

                                    case 1:
                                        cells.Add(Random.value > .5f ? TripQuarter : Eighth);
                                        cells.Add(TripEighth);
                                        cells.Add(TripEighth);
                                        break;

                                    case 2:
                                        cells.Add(TripEighth);
                                        cells.Add(Random.value > .5f ? TripQuarter : Eighth);
                                        cells.Add(TripEighth);
                                        break;
                                };
                                break;
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        if (ms.RhythmSpecs.HasTriplets)
                        {
                            switch (Random.Range(0, 3))
                            {
                                case 0:
                                    cells.Add(TripEighth);
                                    cells.Add(TripEighth);
                                    cells.Add(Eighth);
                                    break;

                                case 1:
                                    cells.Add(Eighth);
                                    cells.Add(TripEighth);
                                    cells.Add(TripEighth);
                                    break;

                                case 2:
                                    cells.Add(TripEighth);
                                    cells.Add(Eighth);
                                    cells.Add(TripEighth);
                                    break;
                            };
                            break;
                        }
                        else
                        {
                            cells.Add(Eighth);
                            cells.Add(Eighth);
                        }
                        break;

                    case SubDivisionTier.D1AndD2:
                        switch (Random.Range(3, 5))
                        {
                            case 3:
                                switch (Random.Range(0, 3))
                                {
                                    case 0:
                                        cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth : Sixteenth);
                                        cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth : Sixteenth);
                                        cells.Add(Eighth);
                                        break;
                                    case 1:
                                        cells.Add(Eighth);
                                        cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth : Sixteenth);
                                        cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth : Sixteenth);
                                        break;
                                    case 2:
                                        cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth : Sixteenth);
                                        cells.Add(Eighth);
                                        cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth : Sixteenth);
                                        break;
                                };
                                break;

                            case 4:
                                cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth : Sixteenth);
                                cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth : Sixteenth);
                                cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth : Sixteenth);
                                cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth : Sixteenth);
                                break;
                        };
                        break;

                    case SubDivisionTier.D2Only:
                        cells.Add(Sixteenth);
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
                                    .SetRhythmicShape(this.RandomQuadCell());
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