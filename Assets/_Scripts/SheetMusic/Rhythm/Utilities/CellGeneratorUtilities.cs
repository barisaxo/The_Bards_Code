using UnityEngine;
namespace SheetMusic.Rhythms
{
    public static class CellGeneratorUtilities
    {
        public static CellShape RandomDupleCell(this Time _) => Random.value > .5f ? CellShape.L : CellShape.LL;
        public static CellShape RandomQuadCell(this Time _) => (CellShape)Random.Range(0, 8);
        public static CellShape RandomTripCellNoTL(this Time _) => (CellShape)Random.Range(8, 11);
        public static CellShape RandomTripCell(this Time _) => (CellShape)Random.Range(8, 12);

        public static void GetRestsAndTies(this MusicSheet ms)
        {
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

        public static RhythmCell PreviousCellOrDefault(this MusicSheet ms, int m, int c)
        {
            if (c == 0)
            {
                if (m == 0)
                {
                    return default;
                }
                return ms.Measures[m - 1].Cells[^1];
            }
            return ms.Measures[m].Cells[c - 1];
        }

        public static bool GetTiedTo(this MusicSheet ms, int m, int c)
        {
            if (!ms.RhythmSpecs.HasTies) return false;
            if (m == ms.Measures.Length - 1 && c == ms.Measures[^1].Cells.Length - 1) return false;//don't tie last cell to nothing
            if (c == 0 &&
                ms.Measures[m].Cells?.Length == 2 &&
                ms.Measures[m].Cells?[0].Shape == CellShape.L &&
                ms.Measures[m].Cells?[1].Shape == CellShape.L) return false;//don't tie long to long in same measure.
            return Random.value > .666f;
        }

        public static bool GetTiedFrom(this MusicSheet ms, int m, int c)
        {
            if (c == 0)
            {
                if (m == 0)
                {
                    return false;
                }
                return ms.Measures[m - 1].Cells[^1].TiedTo;
            }
            return ms.Measures[m].Cells[c - 1].TiedTo;
        }

        public static bool GetRest(this MusicSheet ms, int m, int c)
        {
            if (!ms.RhythmSpecs.HasRests) return false;
            if (ms.Measures[m].Cells[c].TiedFrom) return false;//prevents ties to rests
            if (ms.Measures[m].Cells[c].TiedTo &&
               (ms.Measures[m].Cells[c].Shape == CellShape.L ||
                ms.Measures[m].Cells[c].Shape == CellShape.TL)) return false;//prevents ties from rests
            return Random.value > .5f;
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
