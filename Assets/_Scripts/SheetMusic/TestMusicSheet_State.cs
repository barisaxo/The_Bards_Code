using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SheetMusic;
using SheetMusic.Rhythms;
public class TestMusicSheet_State : State
{
    protected override void PrepareState(Action callback)
    {

        MusicSheet ms = new()
        {
            //Measures = new Measure[4],
            RhythmSpecs = new()
            {
                Time = new TwoTwo(),
                SubDivisionTier = SubDivisionTier.D1AndD2,
                HasTies = true,
                HasRests = true,
                NumberOfMeasures = 4,

            },
        };

        ms.RhythmSpecs.Time.GenerateRhythmCells(ms);

        for (int m = 0; m < ms.Measures.Length; m++)
        {
            for (int c = 0; c < ms.Measures[m].Cells.Length; c++)
            {
                Debug.Log("measure " + m + ", cell " + c + " " + ms.Measures[m].Cells[c].Shape + ", " + ms.Measures[m].Cells[c].Quantizement +
                    ", has rest " + ms.Measures[m].Cells[c].Rest +
                    ", is tied from previous cell " + ms.Measures[m].Cells[c].TiedFrom +
                    ", ties to next cell " + ms.Measures[m].Cells[c].TiedTo);
            }
        }
        //ms.RhythmSpecs.AddRhythmOption(RhythmOption.TripsOnly);

        //ms.DrawRhythms();

        base.PrepareState(callback);
    }
}
