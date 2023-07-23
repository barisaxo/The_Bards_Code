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
            Measures = new RhythmCell[4][],
            RhythmSpecs = new() { TimeSignature = TimeSignature.ThreeFour, SubDivisionTier = SubDivisionTier.EighthsOnly },
        };

        //ms.RhythmSpecs.AddRhythmOption(RhythmOption.TripsOnly);

        ms.DrawRhythms();

        base.PrepareState(callback);
    }
}
