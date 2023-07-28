using System.Collections.Generic;
using UnityEngine;

namespace SheetMusic.Rhythms
{
    public class SixFour : Time
    {
        public SixFour() { Signature = TimeSignature.SixFour; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        for (int i = 0; i < 2; i++)
                        {
                            cells.Add(Quarter);
                        }
                        break;

                    case SubDivisionTier.BeatAndD1:
                        for (int i = 0; i < 2; i++)
                        {
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
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        for (int i = 0; i < 2; i++)
                        {
                            cells.Add(Eighth);
                            cells.Add(Eighth);
                            cells.Add(Eighth);
                        }
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
    }
}
