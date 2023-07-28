using System.Collections.Generic;
using UnityEngine;

namespace SheetMusic.Rhythms
{
    public class TwoTwo : Time
    {
        public TwoTwo() { Signature = TimeSignature.TwoTwo; }

        public override void GenerateRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(new RhythmCell()
                            .SetMetricLevel(MetricLevel.Beat)
                            .SetQuantizement(Quantizement.Half)
                            .SetRhythmicShape(this.RandomDupleCell())
                        );
                        break;

                    case SubDivisionTier.BeatAndD1:
                        if (Random.value > .5f)
                        {
                            cells.Add(new RhythmCell()
                                .SetMetricLevel(MetricLevel.D1)
                                .SetQuantizement(Quantizement.Quarter)
                                .SetRhythmicShape(this.RandomQuadCell())
                            );
                        }
                        else
                        {
                            cells.Add(new RhythmCell()
                                .SetMetricLevel(MetricLevel.Beat)
                                .SetQuantizement(Quantizement.Half)
                                .SetRhythmicShape(this.RandomDupleCell())
                            );
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        cells.Add(new RhythmCell()
                            .SetMetricLevel(MetricLevel.D1)
                            .SetQuantizement(Quantizement.Quarter)
                            .SetRhythmicShape(this.RandomQuadCell())
                        );
                        break;

                    case SubDivisionTier.D1AndD2:
                        if (Random.value > .5f)
                        {
                            cells.Add(new RhythmCell()
                                .SetMetricLevel(MetricLevel.D1)
                                .SetQuantizement(Quantizement.Quarter)
                                .SetRhythmicShape(this.RandomQuadCell())
                            );
                        }
                        else
                        {
                            cells.Add(new RhythmCell()
                                .SetMetricLevel(MetricLevel.Beat)
                                .SetQuantizement(Quantizement.Eighth)
                                .SetRhythmicShape(this.RandomQuadCell())
                            );
                            cells.Add(new RhythmCell()
                                .SetMetricLevel(MetricLevel.Beat)
                                .SetQuantizement(Quantizement.Eighth)
                                .SetRhythmicShape(this.RandomQuadCell())
                            );
                        }

                        break;
                    case SubDivisionTier.D2Only:
                        cells.Add(new RhythmCell()
                            .SetMetricLevel(MetricLevel.Beat)
                            .SetQuantizement(Quantizement.Eighth)
                            .SetRhythmicShape(this.RandomQuadCell())
                            );
                        cells.Add(new RhythmCell()
                            .SetMetricLevel(MetricLevel.Beat)
                            .SetQuantizement(Quantizement.Eighth)
                            .SetRhythmicShape(this.RandomQuadCell())
                            );
                        break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                for (int c = 0; c < ms.Measures[m].Cells.Length; c++)
                {
                    ms.Measures[m].Cells[c].SetTiedTo(ms.GetTiedTo(m, c));
                }
            }

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                for (int c = 0; c < ms.Measures[m].Cells.Length; c++)
                {
                    ms.Measures[m].Cells[c].SetTiedFrom(ms.GetTiedFrom(m, c));
                }
            }

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                for (int c = 0; c < ms.Measures[m].Cells.Length; c++)
                {
                    ms.Measures[m].Cells[c].SetRest(ms.GetRest(m, c));
                }
            }
        }
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
