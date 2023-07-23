using System.Collections.Generic;
using SheetMusic.Rhythms;
using UnityEngine;

namespace SheetMusic
{
    public static class SheetMusicScribingSystems
    {
        public static MusicSheet DrawRhythms(this MusicSheet ms)
        {
            ms.SetUpStaff();
            ms.SetUpCounts(ms.SubCountsPerMeasure());
            //ms.AssignNoteSprites();
            return ms;
        }

        private static void SetUpStaff(this MusicSheet ms)
        {
            ms.ScribedStaves = new Card[ms.Measures.Length];
            ms.ScribedStaves[0] = new Card("Measure 1 staff", ms.Parent)
                .SetImageSprite(Assets.StaffDoubleLeft)
                .SetImageSize(new Vector3(Cam.Io.OrthoX(), Cam.Io.OrthoX() * .7f, 1))
                .SetImagePosition(MeasurePos(MeasureNumber.One));

            //m1.transform.SetParent(ms.Parent);
            //m1.transform.position = NotePosition(MeasureNumber.One, BeatLocation.Thr) + new Vector3(-.15f, 0, 1);
            //m1.sprite = Assets.StaffDoubleLeft;
            //m1.transform.localScale = new Vector3(5.5f, 8f, 1);//new Vector3(6.375f, 8f, 1);
            //m1.color = Color.white * .8f;

            if (ms.Measures.Length == 2)
            {
                ms.ScribedStaves[1] = new Card("Measure 2 staff", ms.Parent)
                    .SetImageSprite(Assets.StaffDoubleRight)
                    .SetImageSize(new Vector3(Cam.Io.OrthoX(), Cam.Io.OrthoX() * .7f, 1))
                    .SetImagePosition(MeasurePos(MeasureNumber.Two));
                //Card m2 = new GameObject(nameof(m2)).AddComponent<Card>();
                //m2.transform.SetParent(ms.Parent);
                //m2.transform.position = NotePosition(MeasureNumber.Two, BeatLocation.Thr) + new Vector3(-.15f, 0.0f, 1);
                //m2.sprite = Assets.StaffDoubleRight;
                //m2.transform.localScale = new Vector3(5.5f, 8f, 1);
                //m2.color = Color.white * .8f;
            }

            else if (ms.Measures.Length == 4)
            {
                ms.ScribedStaves[1] = new Card("Measure 2 staff", ms.Parent)
                    .SetImageSprite(Assets.Staff)
                    .SetImageSize(new Vector3(Cam.Io.OrthoX(), Cam.Io.OrthoX() * .7f, 1))
                    .SetImagePosition(MeasurePos(MeasureNumber.Two));
                //Card m2 = new GameObject(nameof(m2)).AddComponent<Card>();
                //m2.transform.SetParent(ms.Parent);
                //m2.transform.position = NotePosition(MeasureNumber.Two, BeatLocation.Thr) + new Vector3(-.15f, 0, 1);
                //m2.sprite = Assets.Staff;
                //m2.transform.localScale = new Vector3(5.5f, 8f, 1);
                //m2.color = Color.white * .8f;
                ms.ScribedStaves[2] = new Card("Measure 3 staff", ms.Parent)
                    .SetImageSprite(Assets.Staff)
                   .SetImageSize(new Vector3(Cam.Io.OrthoX(), Cam.Io.OrthoX() * .7f, 1))
                    .SetImagePosition(MeasurePos(MeasureNumber.Thr));
                //Card m3 = new GameObject(nameof(m3)).AddComponent<Card>();
                //m3.transform.SetParent(ms.Parent);
                //m3.transform.position = NotePosition(MeasureNumber.Three, BeatLocation.Thr) + new Vector3(-.15f, 0, 1);
                //m3.sprite = Assets.Staff;
                //m3.transform.localScale = new Vector3(5.5f, 8f, 1);
                //m3.color = Color.white * .8f;

                ms.ScribedStaves[3] = new Card("Measure 4 staff", ms.Parent)
                    .SetImageSprite(Assets.StaffDoubleRight)
                   .SetImageSize(new Vector3(Cam.Io.OrthoX(), Cam.Io.OrthoX() * .7f, 1))
                    .SetImagePosition(MeasurePos(MeasureNumber.For));
                //Card m4 = new GameObject(nameof(m4)).AddComponent<Card>();
                //m4.transform.SetParent(ms.Parent);
                //m4.transform.position = NotePosition(MeasureNumber.Four, BeatLocation.Thr) + new Vector3(-.15f, .01f, 1);
                //m4.sprite = Assets.StaffDoubleRight;
                //m4.transform.localScale = new Vector3(5.5f, 8f, 1);
                //m4.color = Color.white * .8f;
            }
        }


        //public static void AssignNoteSprites(this MusicSheet ms)
        //{
        //    foreach (Note n in ms.Notes)
        //    {
        //        Transform parent = new GameObject(n.BeatLocation.ToString()).transform;
        //        parent.SetParent(ms.Parent);
        //        parent.position = NotePosition(n.BeatLocation);

        //        switch (n.QuantizedRhythmicValue)
        //        {
        //            case RhythmicValue.Whole:
        //                {
        //                    Card note = AddNote(n, parent);
        //                    note.transform.position = n.Rest ? parent.position + new Vector3(.4f, .035f) : parent.position;
        //                    note.sprite = n.Rest ? Assets.WholeRest : Assets.WhiteNote;

        //                    if (n.Tied)
        //                    {
        //                        AddTie(n, note, parent);
        //                    }
        //                    break;
        //                }

        //            case RhythmicValue.DotHalf:
        //                {
        //                    Card note = AddNote(n, parent);
        //                    AddDot(note, parent);

        //                    if (n.Rest)
        //                    {
        //                        note.sprite = Assets.HalfRest;
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        note.sprite = Assets.WhiteNote;
        //                        AddStem(note);
        //                    }

        //                    if (n.Tied)
        //                    {
        //                        AddTie(n, note, parent);
        //                    }
        //                    break;
        //                }

        //            case RhythmicValue.Half:
        //                {
        //                    Card note = AddNote(n, parent);

        //                    if (n.Rest)
        //                    {
        //                        note.sprite = Assets.HalfRest;
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        note.sprite = Assets.WhiteNote;
        //                        AddStem(note);
        //                    }

        //                    if (n.Tied)
        //                    {
        //                        AddTie(n, note, parent);
        //                    }
        //                    break;
        //                }

        //            case RhythmicValue.TripHalf:
        //                {
        //                    Card note = AddNote(n, parent);

        //                    if (n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Thr)
        //                    {
        //                        AddTripBracket(note, new Vector3(.125f, .15f), new Vector2(.3f, 1));
        //                        AddTrip3(note, new Vector3(.125f, .15f), Vector2.one * .5f);
        //                    }

        //                    if (n.Rest)
        //                    {
        //                        note.sprite = Assets.HalfRest;
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        note.sprite = Assets.WhiteNote;
        //                        AddStem(note);
        //                    }

        //                    if (n.Tied)
        //                    {
        //                        AddTie(n, note, parent);
        //                    }


        //                    break;
        //                }

        //            case RhythmicValue.DotQuarter:
        //                {
        //                    Card note = AddNote(n, parent);
        //                    note.sprite = n.Rest ? Assets.QuarterRest : Assets.BlackNote;
        //                    AddDot(note, parent);

        //                    if (n.Tied)
        //                    {
        //                        AddTie(n, note, parent);
        //                    }
        //                    break;
        //                }

        //            case RhythmicValue.Quarter:
        //                {
        //                    Card note = AddNote(n, parent);
        //                    note.sprite = n.Rest ? Assets.QuarterRest : Assets.BlackNote;

        //                    if (n.Tied)
        //                    {
        //                        AddTie(n, note, parent);
        //                    }
        //                    break;
        //                }

        //            case RhythmicValue.TripQuarter:
        //                {
        //                    Card note = AddNote(n, parent);
        //                    note.sprite = n.Rest ? Assets.QuarterRest : Assets.BlackNote;

        //                    if (n.ParentCell.Quantizement == Quantizement.EighthTrips &&
        //                       (n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Thr ||
        //                        n.BeatLocation == BeatLocation.Two || n.BeatLocation == BeatLocation.For))
        //                    {
        //                        AddTripBracket(note, new Vector3(.05f, .15f), new Vector2(.3f, 1));
        //                        AddTrip3(note, new Vector3(.05f, .15f), Vector2.one * .5f);
        //                        break;
        //                    }

        //                    if (n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Thr)
        //                    {
        //                        AddTripBracket(note, new Vector3(.125f, .15f), new Vector2(.3f, 1));
        //                        AddTrip3(note, new Vector3(.125f, .15f), Vector2.one * .5f);
        //                    }

        //                    if (n.Tied)
        //                    {
        //                        AddTie(n, note, parent);
        //                    }
        //                    break;
        //                }

        //            case RhythmicValue.DotEighth:
        //                {
        //                    Card note = AddNote(n, parent);
        //                    AddDot(note, parent);

        //                    if (n.Rest)
        //                    {
        //                        note.sprite = Assets.EighthRest;
        //                        break;
        //                    }

        //                    note.sprite = Assets.BlackNote;

        //                    if ((n.ParentCell.RhythmicShape == CellShape.dhq ||
        //                        n.ParentCell.RhythmicShape == CellShape.qdh) &&
        //                        n.ParentCell.Rest)
        //                    {
        //                        AddEighthFlag(note);
        //                    }
        //                    else if (n.ParentCell.RhythmicShape == CellShape.dhq)
        //                    {
        //                        AddEighthBeam(note, Vector3.right * .015f, new Vector2(2, 1));
        //                    }
        //                    else if (n.ParentCell.RhythmicShape == CellShape.qdh)
        //                    {
        //                        // AddEighthBeamRevemse(note, size: Vector3.one);
        //                    }

        //                    if (n.Tied)
        //                    {
        //                        AddTie(n, note, parent);
        //                    }
        //                    break;
        //                }

        //            case RhythmicValue.Eighth:
        //                {
        //                    Card note = AddNote(n, parent);
        //                    note.sprite = n.Rest ? Assets.EighthRest : Assets.BlackNote;

        //                    switch (n.ParentCell.Quantizement)
        //                    {
        //                        case Quantizement.Eighth:
        //                            {
        //                                switch (n.ParentCell.RhythmicShape)
        //                                {
        //                                    case CellShape.dhq:
        //                                        {
        //                                            AddEighthFlag(note);
        //                                        }
        //                                        break;

        //                                    case CellShape.hqq:
        //                                        {
        //                                            if (n.BeatLocation == BeatLocation.Two || n.BeatLocation == BeatLocation.For)
        //                                            {
        //                                                AddEighthBeam(note, Vector3.zero, Vector3.one);
        //                                            }
        //                                            else
        //                                            {
        //                                                AddEighthBeamRevemse(note, size: Vector3.one);
        //                                            }
        //                                        }
        //                                        break;

        //                                    case CellShape.qdh:
        //                                        {
        //                                            if (n.Rest) { break; }
        //                                            AddEighthFlag(note);
        //                                        }
        //                                        break;

        //                                    case CellShape.qhq:
        //                                        {
        //                                            if (n.Rest && (n.BeatLocation == BeatLocation.One
        //                                                || n.BeatLocation == BeatLocation.Thr)) { break; }
        //                                            AddEighthFlag(note);
        //                                        }
        //                                        break;

        //                                    case CellShape.qqh:
        //                                        {
        //                                            if (n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Thr)
        //                                            {
        //                                                if (n.Rest) { break; }
        //                                                AddEighthBeam(note, Vector3.zero, Vector3.one);
        //                                            }
        //                                            else if (n.ParentCell.Rest)
        //                                            {
        //                                                AddEighthFlag(note);
        //                                            }
        //                                            else
        //                                            {
        //                                                AddEighthBeamRevemse(note, size: Vector3.one);
        //                                            }

        //                                        }
        //                                        break;

        //                                    case CellShape.qqqq:
        //                                        {
        //                                            if (n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Thr)
        //                                            {
        //                                                if (n.Rest) { break; }
        //                                                AddEighthBeam(note, Vector3.zero, Vector3.one);
        //                                            }
        //                                            else if (n.ParentCell.Rest && (n.BeatLocation == BeatLocation.OneN || n.BeatLocation == BeatLocation.ThrN))
        //                                            {
        //                                                if (n.Rest) { break; }
        //                                                AddEighthBeam(note, Vector3.zero, Vector3.one);
        //                                            }
        //                                            else if (n.BeatLocation == BeatLocation.TwoN || n.BeatLocation == BeatLocation.ForN)
        //                                            {
        //                                                AddEighthBeamRevemse(note, size: Vector3.one);
        //                                            }
        //                                            else
        //                                            {
        //                                                if (n.Rest) { break; }
        //                                                AddEighthBeam(note, Vector3.zero, new Vector2(1.5f, 1));
        //                                                AddEighthBeamRevemse(note, size: new Vector2(1.5f, 1));
        //                                            }
        //                                        }
        //                                        break;
        //                                }
        //                                break;
        //                            }

        //                        case Quantizement.Sixteenth:
        //                            {
        //                                if (n.ParentCell.RhythmicShape == CellShape.hh && n.ParentCell.Rest && !n.Rest)
        //                                {
        //                                    AddEighthFlag(note);
        //                                }
        //                                else if ((n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Two ||
        //                                          n.BeatLocation == BeatLocation.Thr || n.BeatLocation == BeatLocation.For) && !n.Rest)
        //                                {
        //                                    AddEighthBeam(note, Vector2.zero, new Vector2(.9f, 1));
        //                                }
        //                                else if (n.BeatLocation == BeatLocation.OneE || n.BeatLocation == BeatLocation.TwoE ||
        //                                         n.BeatLocation == BeatLocation.ThrE || n.BeatLocation == BeatLocation.ForE)
        //                                {
        //                                    AddEighthBeam(note, Vector3.zero, new Vector2(.9f, 1));

        //                                    if (!n.ParentCell.Rest)
        //                                    {
        //                                        AddEighthBeamRevemse(note, size: new Vector2(.8f, 1));
        //                                    }
        //                                }
        //                                else if (n.BeatLocation == BeatLocation.OneN || n.BeatLocation == BeatLocation.TwoN ||
        //                                         n.BeatLocation == BeatLocation.ThrN || n.BeatLocation == BeatLocation.ForN)
        //                                {
        //                                    AddEighthBeamRevemse(note, size: new Vector2(.8f, 1));
        //                                }
        //                                break;
        //                            }
        //                    };

        //                    if (n.Tied)
        //                    {
        //                        AddTie(n, note, parent);
        //                    }
        //                    break;
        //                }

        //            case RhythmicValue.TripEighth:
        //                {
        //                    Card note = AddNote(n, parent);
        //                    note.sprite = n.Rest ? Assets.EighthRest : Assets.BlackNote;

        //                    if (n.Tied)
        //                    {
        //                        AddTie(n, note, parent);
        //                    }

        //                    if (n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Two ||
        //                        n.BeatLocation == BeatLocation.Thr || n.BeatLocation == BeatLocation.For)
        //                    {
        //                        AddTripBracket(note, new Vector3(.05f, .15f), new Vector2(.3f, 1));
        //                        AddTrip3(note, new Vector3(.05f, .15f), Vector2.one * .5f);
        //                    }

        //                    if ((n.ParentCell.RhythmicShape == CellShape.tqh || n.ParentCell.RhythmicShape == CellShape.thq) && !n.Rest)
        //                    {
        //                        AddEighthFlag(note);
        //                    }
        //                    else if ((n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Two ||
        //                              n.BeatLocation == BeatLocation.Thr || n.BeatLocation == BeatLocation.For) & !n.Rest)
        //                    {
        //                        AddEighthBeam(note, new Vector3(.004f, 0), new Vector3(1.2f, 1));
        //                    }
        //                    else if ((n.BeatLocation == BeatLocation.OneT || n.BeatLocation == BeatLocation.TwoT ||
        //                              n.BeatLocation == BeatLocation.ThrT || n.BeatLocation == BeatLocation.ForT))
        //                    {
        //                        AddEighthBeam(note, Vector3.zero, Vector3.one);
        //                    }
        //                    else if ((n.BeatLocation == BeatLocation.OneL || n.BeatLocation == BeatLocation.TwoL ||
        //                              n.BeatLocation == BeatLocation.ThrL || n.BeatLocation == BeatLocation.ForL))
        //                    {
        //                        AddEighthBeamRevemse(note, size: new Vector2(.9f, 1));
        //                    }
        //                    break;
        //                }

        //            case RhythmicValue.Sixteenth:
        //                {
        //                    Card note = AddNote(n, parent);
        //                    note.sprite = n.Rest ? Assets.SixteenthRest : Assets.BlackNote;

        //                    switch (n.ParentCell.RhythmicShape)
        //                    {
        //                        case CellShape.dhq:
        //                            {
        //                                if (n.ParentCell.Rest)
        //                                {
        //                                    AddSixteenthFlag(note);
        //                                }
        //                                else
        //                                {
        //                                    AddEighthBeamRevemse(note, new Vector2(.8f, 1));

        //                                    AddSixteenthBeamRevemse(note, Vector3.right * -.005f, new Vector2(.5f, 1));
        //                                }
        //                            }
        //                            break;

        //                        case CellShape.qdh:
        //                            {
        //                                if (!n.Rest)
        //                                {
        //                                    AddSixteenthBeam(note, -Vector3.right * .005f, new Vector2(.5f, 1));
        //                                    AddEighthBeam(note, Vector3.zero, new Vector2(.9f, 1));
        //                                }
        //                            }
        //                            break;

        //                        case CellShape.hqq:
        //                            {
        //                                if (n.BeatLocation == BeatLocation.OneN || n.BeatLocation == BeatLocation.TwoN ||
        //                                    n.BeatLocation == BeatLocation.ThrN || n.BeatLocation == BeatLocation.ForN)
        //                                {
        //                                    if (!n.ParentCell.Rest)
        //                                    {
        //                                        AddEighthBeamRevemse(note, new Vector2(.85f, 1));
        //                                    }
        //                                    AddSixteenthBeam(note, -Vector3.right * .005f, new Vector2(.5f, 1));
        //                                    AddEighthBeam(note, Vector3.zero, new Vector2(.9f, 1));
        //                                }
        //                                else
        //                                {
        //                                    AddEighthBeamRevemse(note, new Vector2(.85f, 1));
        //                                    AddSixteenthBeamRevemse(note, -Vector3.right * .0055f, new Vector2(.8f, 1));
        //                                }
        //                            }
        //                            break;

        //                        case CellShape.qqh:
        //                            {
        //                                if ((n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Two ||
        //                                 n.BeatLocation == BeatLocation.Thr || n.BeatLocation == BeatLocation.For) && !n.Rest)
        //                                {
        //                                    AddSixteenthBeam(note, -Vector3.right * .005f, new Vector2(.5f, 1));
        //                                    AddEighthBeam(note, Vector3.zero, new Vector2(.9f, 1));
        //                                }
        //                                else if (!n.Rest && !n.ParentCell.Rest)
        //                                {
        //                                    AddEighthBeamRevemse(note, new Vector2(.85f, 1));
        //                                    AddSixteenthBeamRevemse(note, -Vector3.right * .0055f, new Vector2(.8f, 1));
        //                                }
        //                                if ((n.BeatLocation == BeatLocation.OneE || n.BeatLocation == BeatLocation.TwoE ||
        //                                 n.BeatLocation == BeatLocation.ThrE || n.BeatLocation == BeatLocation.ForE) && n.ParentCell.Rest)
        //                                {
        //                                    AddSixteenthBeam(note, -Vector3.right * .005f, new Vector2(.5f, 1));
        //                                }
        //                            }
        //                            break;

        //                        case CellShape.qhq:
        //                            {
        //                                if ((n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Two ||
        //                                    n.BeatLocation == BeatLocation.Thr || n.BeatLocation == BeatLocation.For) && !n.Rest)
        //                                {
        //                                    AddSixteenthBeam(note, -Vector3.right * .005f, new Vector2(.5f, 1));
        //                                    AddEighthBeam(note, Vector3.zero, new Vector2(.9f, 1));
        //                                }
        //                                else if (!n.Rest)
        //                                {
        //                                    AddEighthBeamRevemse(note, new Vector2(.85f, 1));
        //                                    AddSixteenthBeamRevemse(note, -Vector3.right * .0055f, new Vector2(.5f, 1));
        //                                }
        //                            }
        //                            break;

        //                        case CellShape.qqqq:
        //                            {
        //                                if (!n.Rest &&
        //                                   (n.BeatLocation != BeatLocation.OneA && n.BeatLocation != BeatLocation.TwoA &&
        //                                    n.BeatLocation != BeatLocation.ThrA && n.BeatLocation != BeatLocation.ForA))
        //                                {
        //                                    AddSixteenthBeam(note, -Vector3.right * .005f, new Vector2(.5f, 1));
        //                                    AddEighthBeam(note, Vector3.zero, new Vector2(.9f, 1));
        //                                }

        //                                if (n.BeatLocation == BeatLocation.OneA || n.BeatLocation == BeatLocation.TwoA ||
        //                                    n.BeatLocation == BeatLocation.ThrA || n.BeatLocation == BeatLocation.ForA ||
        //                                    n.BeatLocation == BeatLocation.OneN || n.BeatLocation == BeatLocation.TwoN ||
        //                                    n.BeatLocation == BeatLocation.ThrN || n.BeatLocation == BeatLocation.ForN)
        //                                {
        //                                    AddEighthBeamRevemse(note, new Vector2(.85f, 1));
        //                                    AddSixteenthBeamRevemse(note, -Vector3.right * .0055f, new Vector2(.5f, 1));
        //                                }

        //                                if (!n.ParentCell.Rest &&
        //                                   (n.BeatLocation == BeatLocation.OneE || n.BeatLocation == BeatLocation.TwoE ||
        //                                    n.BeatLocation == BeatLocation.ThrE || n.BeatLocation == BeatLocation.ForE))
        //                                {
        //                                    AddEighthBeamRevemse(note, new Vector2(.85f, 1));
        //                                    AddSixteenthBeamRevemse(note, -Vector3.right * .0055f, new Vector2(.5f, 1));
        //                                }

        //                                // if (n.BeatLocation != BeatLocation.One && n.BeatLocation != BeatLocation.Two &&
        //                                //   n.BeatLocation != BeatLocation.Thr && n.BeatLocation != BeatLocation.For &&

        //                                //   !((n.BeatLocation == BeatLocation.OneE || n.BeatLocation == BeatLocation.TwoE ||
        //                                //   n.BeatLocation == BeatLocation.ThrE || n.BeatLocation == BeatLocation.ForE)

        //                                //    &&
        //                                //   n.ParentCell.Rest))
        //                                // {
        //                                //     Debug.Log(!((n.BeatLocation == BeatLocation.OneE || n.BeatLocation == BeatLocation.TwoE ||
        //                                //                  n.BeatLocation == BeatLocation.ThrE || n.BeatLocation == BeatLocation.ForE)));

        //                                //     // AddEighthBeamRevemse(note, new Vector2(.5f, 1));
        //                                //     AddSixteenthBeamRevemse(note, -Vector3.right * .0055f, new Vector2(.5f, 1));
        //                                // }
        //                            }
        //                            break;
        //                    }

        //                    if (n.Tied)
        //                    {
        //                        AddTie(n, note, parent);
        //                    }
        //                    break;
        //                }
        //        }

        //        if (n.MeasureNumber == MeasureNumber.Three &&
        //            n.BeatLocation == BeatLocation.One &&
        //            rb.Measures[1][^1].Tied)
        //        {
        //            Card tie = new GameObject(nameof(tie)).AddComponent<Card>();
        //            tie.transform.SetParent(parent);
        //            tie.transform.position = parent.position + (Vector3.left * .14f);
        //            tie.transform.localScale = new Vector2(2f, 1);
        //            tie.sprite = Assets.Tie;
        //        }

        //        parent.localScale = Vector2.one * 7;
        //    }
        //}

        //private static Card AddTripBracket(Card note, Vector3 pos, Vector2 size)
        //{
        //    Card tripBracket = new GameObject(nameof(tripBracket)).AddComponent<Card>();
        //    tripBracket.transform.SetParent(note.transform);
        //    tripBracket.transform.position = note.transform.position + pos;
        //    tripBracket.sprite = Assets.TripletBracket;
        //    tripBracket.transform.localScale = size;
        //    return tripBracket;
        //}

        //private static Card AddTrip3(Card note, Vector3 pos, Vector2 size)
        //{
        //    Card trip3 = new GameObject(nameof(trip3)).AddComponent<Card>();
        //    trip3.transform.SetParent(note.transform);
        //    trip3.transform.position = note.transform.position + pos;
        //    trip3.sprite = Assets.Triplet3;
        //    trip3.transform.localScale = size;
        //    return trip3;
        //}

        //private static Card AddSixteenthBeam(Card note, Vector3 offset, Vector2 size)
        //{
        //    Card sixteenthBeam = new GameObject(nameof(sixteenthBeam)).AddComponent<Card>();
        //    sixteenthBeam.transform.SetParent(note.transform);
        //    sixteenthBeam.transform.position = note.transform.position + offset;
        //    sixteenthBeam.transform.localScale = size;
        //    sixteenthBeam.sprite = Assets.SixteenthBeam;
        //    return sixteenthBeam;
        //}

        //private static Card AddSixteenthBeamRevemse(Card note, Vector3 offset, Vector2 size)
        //{
        //    Card doubleBeamRevemse = new GameObject(nameof(doubleBeamRevemse)).AddComponent<Card>();
        //    doubleBeamRevemse.transform.SetParent(note.transform);
        //    doubleBeamRevemse.transform.position = note.transform.position + offset;
        //    doubleBeamRevemse.transform.localScale = size;
        //    doubleBeamRevemse.sprite = Assets.SixteenthBeamRevemse;
        //    return doubleBeamRevemse;
        //}

        //private static Card AddSixteenthFlag(Card note)
        //{
        //    Card sixteenthFlag = new GameObject(nameof(sixteenthFlag)).AddComponent<Card>();
        //    sixteenthFlag.transform.SetParent(note.transform);
        //    sixteenthFlag.transform.position = note.transform.position; //+offset
        //    sixteenthFlag.sprite = Assets.SixteenthFlag;
        //    return sixteenthFlag;
        //}

        //private static Card AddEighthBeamRevemse(Card note, Vector2 size)
        //{
        //    Card eighthBeamRevemse = new GameObject(nameof(eighthBeamRevemse)).AddComponent<Card>();
        //    eighthBeamRevemse.transform.SetParent(note.transform);
        //    eighthBeamRevemse.transform.position = note.transform.position + (Vector3.left * .005f);
        //    eighthBeamRevemse.sprite = Assets.EighthBeamRevemse;
        //    eighthBeamRevemse.transform.localScale = size;
        //    return eighthBeamRevemse;
        //}

        //private static Card AddEighthBeam(Card note, Vector3 offset, Vector2 size)
        //{
        //    Card eighthBeam = new GameObject(nameof(eighthBeam)).AddComponent<Card>();
        //    eighthBeam.transform.SetParent(note.transform);
        //    eighthBeam.transform.position = note.transform.position + offset;
        //    eighthBeam.transform.localScale = size;
        //    eighthBeam.sprite = Assets.EighthBeam;
        //    return eighthBeam;
        //}

        //private static Card AddEighthFlag(Card note)
        //{
        //    Card eighthFlag = new GameObject(nameof(eighthFlag)).AddComponent<Card>();
        //    eighthFlag.transform.SetParent(note.transform);
        //    eighthFlag.transform.position = note.transform.position;
        //    eighthFlag.sprite = Assets.EighthFlag;
        //    return eighthFlag;
        //}

        //private static Card AddStem(Card note)
        //{
        //    Card stem = new GameObject(nameof(stem)).AddComponent<Card>();
        //    stem.transform.SetParent(note.transform);
        //    stem.transform.position = note.transform.position;
        //    stem.sprite = Assets.Stem;
        //    return stem;
        //}

        //private static Card AddDot(Card note, Transform parent)
        //{
        //    Card dot = new GameObject(nameof(dot)).AddComponent<Card>();
        //    dot.transform.SetParent(note.transform);
        //    dot.transform.position = parent.position + (Vector3.right * -.01f);
        //    dot.sprite = Assets.Dot;
        //    return dot;
        //}

        //private static Card AddNote(Note n, Transform parent)
        //{
        //    Card note = new GameObject(n.QuantizedRhythmicNoteValue.ToString()).AddComponent<Card>();
        //    note.transform.SetParent(parent);
        //    note.transform.position = parent.position;
        //    return note;
        //}

        //private static Card AddTie(Note n, Card note, Transform parent)
        //{
        //    Card tie = new GameObject(nameof(tie)).AddComponent<Card>();
        //    tie.transform.SetParent(parent);
        //    tie.transform.position = GetTieLocation(note, n);
        //    tie.transform.localScale = GetTieSize(n);
        //    tie.sprite = Assets.Tie;
        //    return tie;
        //}

        //private static Vector3 GetTieLocation(Card sr, Note n) => n.ParentCell.Quantizement switch
        //{
        //    Quantizement.Quarter => sr.transform.position + (Vector3.right * .02f),

        //    _ => n.BeatLocation switch
        //    {
        //        BeatLocation.For => sr.transform.position + (Vector3.right * .015f),//good
        //        BeatLocation.ForE => sr.transform.position + (Vector3.right * .015f),
        //        BeatLocation.ForN => sr.transform.position + (Vector3.right * .01f),
        //        BeatLocation.ForA => sr.transform.position + (Vector3.right * .0125f),
        //        BeatLocation.ForT => sr.transform.position + (Vector3.right * .01625f),
        //        BeatLocation.ForL => sr.transform.position + (Vector3.right * .01125f),
        //        BeatLocation.Thr => sr.transform.position + (Vector3.right * .015f),
        //        BeatLocation.ThrE => sr.transform.position + (Vector3.right * .0125f),
        //        BeatLocation.ThrN => sr.transform.position + (Vector3.right * .01f),//good
        //        BeatLocation.ThrA => sr.transform.position + Vector3.right * .0075f,
        //        BeatLocation.ThrT => sr.transform.position + (Vector3.right * .01125f),
        //        BeatLocation.ThrL => sr.transform.position + (Vector3.right * .0125f),
        //        BeatLocation.One => sr.transform.position + (Vector3.right * .01f),
        //        BeatLocation.OneE => sr.transform.position + (Vector3.right * .01f),
        //        BeatLocation.OneT => sr.transform.position + (Vector3.right * .01f),
        //        BeatLocation.OneN => sr.transform.position + (Vector3.right * .01f),//good
        //        BeatLocation.OneL => sr.transform.position + (Vector3.right * .0125f),
        //        BeatLocation.OneA => sr.transform.position + (Vector3.right * .01f),
        //        BeatLocation.Two => sr.transform.position + (Vector3.right * .01f),//good
        //        BeatLocation.TwoE => sr.transform.position + (Vector3.right * .01f),
        //        BeatLocation.TwoT => sr.transform.position + (Vector3.right * .01f),
        //        BeatLocation.TwoN => sr.transform.position,//good
        //        BeatLocation.TwoL => sr.transform.position + (Vector3.right * .0125f),
        //        BeatLocation.TwoA => sr.transform.position,
        //        _ => sr.transform.position,
        //    },
        //};

        //private static Vector3 GetTieSize(Note n) => n.ParentCell.Quantizement switch
        //{
        //    Quantizement.Quarter => n.BeatLocation.MeasureNumber switch
        //    {
        //        MeasureNumber.Two => (ScaledToFit(
        //            NotePosition(n.BeatLocation),
        //            NotePosition(BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),
        //        _ => ScaledToFit(
        //            NotePosition(n.MeasureNumber, n.BeatLocation),
        //            NotePosition(n.MeasureNumber + 1, BeatLocation.One)) * new Vector3(2, 1),
        //    },

        //    Quantizement.QuarterTrips => (n.MeasureNumber, n.BeatLocation) switch
        //    {
        //        (_, BeatLocation.OneL) => ScaledToFit(
        //        NotePosition(n.MeasureNumber, n.BeatLocation),
        //        NotePosition(n.MeasureNumber, BeatLocation.Thr)) * new Vector3(2, 1),

        //        (_, BeatLocation.TwoT) => ScaledToFit(
        //        NotePosition(n.MeasureNumber, n.BeatLocation),
        //        NotePosition(n.MeasureNumber, BeatLocation.Thr)) * new Vector3(2, 1),

        //        (_, BeatLocation.TwoL) => ScaledToFit(
        //        NotePosition(n.MeasureNumber, n.BeatLocation),
        //        NotePosition(n.MeasureNumber, BeatLocation.Thr)) * new Vector3(2, 1),

        //        (MeasureNumber.Two, _) => (ScaledToFit(
        //            NotePosition(n.MeasureNumber, n.BeatLocation),
        //            NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

        //        _ => ScaledToFit(
        //           NotePosition(n.MeasureNumber, n.BeatLocation),
        //           NotePosition(n.MeasureNumber + 1, BeatLocation.One)) * new Vector3(2, 1),
        //    },

        //    Quantizement.EighthTrips => (n.MeasureNumber, n.BeatLocation) switch
        //    {
        //        (MeasureNumber.Two, BeatLocation.For) => (ScaledToFit(
        //             NotePosition(n.MeasureNumber, n.BeatLocation),
        //             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

        //        (MeasureNumber.Two, BeatLocation.ForT) => (ScaledToFit(
        //             NotePosition(n.MeasureNumber, n.BeatLocation),
        //             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

        //        (MeasureNumber.Two, BeatLocation.ForL) => (ScaledToFit(
        //             NotePosition(n.MeasureNumber, n.BeatLocation),
        //             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

        //        _ when n.BeatLocation < BeatLocation.Two => ScaledToFit(
        //        NotePosition(n.MeasureNumber, n.BeatLocation),
        //        NotePosition(n.MeasureNumber, BeatLocation.Two)) * new Vector3(2, 1),

        //        _ when n.BeatLocation < BeatLocation.Thr => ScaledToFit(
        //       NotePosition(n.MeasureNumber, n.BeatLocation),
        //       NotePosition(n.MeasureNumber, BeatLocation.Thr)) * new Vector3(2, 1),

        //        _ when n.BeatLocation < BeatLocation.For => ScaledToFit(
        //       NotePosition(n.MeasureNumber, n.BeatLocation),
        //       NotePosition(n.MeasureNumber, BeatLocation.For)) * new Vector3(2, 1),

        //        _ => ScaledToFit(
        //       NotePosition(n.MeasureNumber, n.BeatLocation),
        //       NotePosition(n.MeasureNumber + 1, BeatLocation.One)) * new Vector3(2, 1),
        //    },

        //    Quantizement.Eighth => (n.MeasureNumber, n.BeatLocation) switch
        //    {
        //        (MeasureNumber.Two, BeatLocation.Thr) => (ScaledToFit(
        //             NotePosition(n.MeasureNumber, n.BeatLocation),
        //             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

        //        (MeasureNumber.Two, BeatLocation.ThrN) => (ScaledToFit(
        //             NotePosition(n.MeasureNumber, n.BeatLocation),
        //             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

        //        (MeasureNumber.Two, BeatLocation.For) => (ScaledToFit(
        //             NotePosition(n.MeasureNumber, n.BeatLocation),
        //             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

        //        (MeasureNumber.Two, BeatLocation.ForN) => (ScaledToFit(
        //             NotePosition(n.MeasureNumber, n.BeatLocation),
        //             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

        //        _ when n.BeatLocation < BeatLocation.Thr => ScaledToFit(
        //       NotePosition(n.MeasureNumber, n.BeatLocation),
        //       NotePosition(n.MeasureNumber, BeatLocation.Thr)) * new Vector3(2, 1),

        //        _ => ScaledToFit(
        //       NotePosition(n.MeasureNumber, n.BeatLocation),
        //       NotePosition(n.MeasureNumber + 1, BeatLocation.One)) * new Vector3(2, 1),
        //    },

        //    Quantizement.Sixteenth => (n.MeasureNumber, n.BeatLocation) switch
        //    {
        //        (MeasureNumber.Two, BeatLocation.For) => (ScaledToFit(
        //             NotePosition(n.MeasureNumber, n.BeatLocation),
        //             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

        //        (MeasureNumber.Two, BeatLocation.ForE) => (ScaledToFit(
        //             NotePosition(n.MeasureNumber, n.BeatLocation),
        //             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

        //        (MeasureNumber.Two, BeatLocation.ForN) => (ScaledToFit(
        //             NotePosition(n.MeasureNumber, n.BeatLocation),
        //             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

        //        (MeasureNumber.Two, BeatLocation.ForA) => (ScaledToFit(
        //             NotePosition(n.MeasureNumber, n.BeatLocation),
        //             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

        //        _ when n.BeatLocation < BeatLocation.Two => ScaledToFit(
        //        NotePosition(n.MeasureNumber, n.BeatLocation),
        //        NotePosition(n.MeasureNumber, BeatLocation.Two)) * new Vector3(2, 1),

        //        _ when n.BeatLocation < BeatLocation.Thr => ScaledToFit(
        //       NotePosition(n.MeasureNumber, n.BeatLocation),
        //       NotePosition(n.MeasureNumber, BeatLocation.Thr)) * new Vector3(2, 1),

        //        _ when n.BeatLocation < BeatLocation.For => ScaledToFit(
        //       NotePosition(n.MeasureNumber, n.BeatLocation),
        //       NotePosition(n.MeasureNumber, BeatLocation.For)) * new Vector3(2, 1),

        //        _ => ScaledToFit(
        //       NotePosition(n.MeasureNumber, n.BeatLocation),
        //       NotePosition(n.MeasureNumber + 1, BeatLocation.One)) * new Vector3(2, 1),
        //    },

        //    _ => Vector2.one,
        //};


        public static Vector3 NotePosition(this MusicSheet ms, BeatLocation bl)
        {
            Vector3 pos = Vector3.zero;
            pos += MeasurePos(bl.MeasureNumber);
            pos += CountPos(bl.Count, ms.RhythmSpecs.TimeSignature.Quantity);
            pos += SubBeatPos(bl.SubBeatAssignment, ms.RhythmSpecs.TimeSignature.Quantity);
            return pos;
        }

        static Vector3 MeasurePos(MeasureNumber m) => m switch
        {
            MeasureNumber.One => (Cam.Io.OrthoX() * .515f * Vector2.left) + (Vector2.up * 3),
            MeasureNumber.Two => (Cam.Io.OrthoX() * .515f * Vector2.right) + (Vector2.up * 3),
            MeasureNumber.Thr => (Cam.Io.OrthoX() * .515f * Vector2.left) + (Vector2.down * 1),
            MeasureNumber.For => (Cam.Io.OrthoX() * .515f * Vector2.right) + (Vector2.down * 1),
            _ => Vector2.zero
        };

        static Vector3 CountPos(Count c, Count counts)
        {
            float interval = (2 - -2) / ((int)counts - 1);
            return new Vector3(-2 + (interval * (int)c), 0, 0);
        }

        static Vector3 SubBeatPos(SubBeatAssignment b, Count counts)
        {
            //trying to say: spaced from 1 to 12 (sub beat assignment), from 0 to 1, and scaled to fit the number of counts in the measure
            float interval = ((int)counts - 1);
            return new Vector3(interval * (int)b, 0, 0);
        }

        //static Vector2 MeasureOne(BeatLocation bl)
        //{

        //    return new Vector2(
        //           (((int)bl) * ((((Cam.Io.Camera.orthographicSize * 2)) - 1) / 132))
        //           - ((Cam.Io.Camera.orthographicSize)),
        //           2.25f) + Vector2.left * .050f;
        //    //return new Vector2(
        //    //       (((int)bl) * ((((MyCam.Io.Cam.orthographicSize * 2 * MyCam.Io.Cam.aspect)) - 1) / 132))
        //    //       - ((MyCam.Io.Cam.orthographicSize * MyCam.Io.Cam.aspect) - (MyCam.Io.Cam.aspect * .5f)),
        //    //       2.25f) + Vector2.left * .35f;
        //}
        //static Vector2 MeasureTwo(BeatLocation bl)
        //{
        //    return new Vector2(
        //          (((int)bl + 64) * ((((Cam.Io.Camera.orthographicSize * 2)) - 1) / 132))
        //          - ((Cam.Io.Camera.orthographicSize)),
        //          2.255f) + Vector2.right * 1.050f;
        //    //return new Vector2(
        //    //       (((int)bl + 64) * ((((MyCam.Io.Cam.orthographicSize * 2 * MyCam.Io.Cam.aspect)) - 1) / 132))
        //    //       - ((MyCam.Io.Cam.orthographicSize * MyCam.Io.Cam.aspect) - (MyCam.Io.Cam.aspect * .5f)),
        //    //       2.255f) + Vector2.left * .35f;
        //}
        //static Vector2 MeasureThree(BeatLocation bl)
        //{
        //    return new Vector2(
        //           (((int)bl) * ((((Cam.Io.Camera.orthographicSize * 2)) - 1) / 132))
        //           - ((Cam.Io.Camera.orthographicSize)),
        //          -1.25f) + Vector2.left * .050f;
        //    //return new Vector2(
        //    //       (((int)bl) * ((((MyCam.Io.Cam.orthographicSize * 2 * MyCam.Io.Cam.aspect)) - 1) / 132))
        //    //       - ((MyCam.Io.Cam.orthographicSize * MyCam.Io.Cam.aspect) - (MyCam.Io.Cam.aspect * .5f)),
        //    //      -1.25f) + Vector2.left * .35f;

        //}
        //static Vector2 MeasureFour(BeatLocation bl)
        //{
        //    return new Vector2(
        //           (((int)bl + 64) * ((((Cam.Io.Camera.orthographicSize * 2)) - 1) / 132))
        //           - ((Cam.Io.Camera.orthographicSize)),
        //         -1.255f) + Vector2.right * 1.050f;
        //    //return new Vector2(
        //    //       (((int)bl + 64) * ((((MyCam.Io.Cam.orthographicSize * 2 * MyCam.Io.Cam.aspect)) - 1) / 132))
        //    //       - ((MyCam.Io.Cam.orthographicSize * MyCam.Io.Cam.aspect) - (MyCam.Io.Cam.aspect * .5f)),
        //    //     -1.255f) + Vector2.left * .35f;
        //}

        private static void SetUpCounts(this MusicSheet ms, SubBeatAssignment[] beats)
        {
            ms.Counts = new Card[beats.Length];

            for (int i = 0; i < ms.Measures.Length; i++)
            {
                int c = 0;
                int count = 0;
                for (int ii = 0; ii < beats.Length; ii++)
                {
                    if (beats[c] == SubBeatAssignment.D) count++;
                    ms.Counts[c] = new Card(nameof(Count) + ": " + count + beats[c].ToString(), ms.Parent)
                        .SetTextString(beats[c] == SubBeatAssignment.D ? count.ToString() : beats[c].ToString())
                        .SetTMPPosition(ms.NotePosition(new BeatLocation() { Count = (Count)count, MeasureNumber = (MeasureNumber)i + 1, SubBeatAssignment = beats[c] }))
                        .SetFontScale(.4f, .4f);
                    c++;
                }
            }
        }

        public static SubBeatAssignment[] SubCountsPerMeasure(this MusicSheet ms)
        {
            List<SubBeatAssignment> beats = new();

            if (!ms.RhythmSpecs.RhythmOptions.Contains(RhythmOption.TripsOnly) && !ms.RhythmSpecs.RhythmOptions.Contains(RhythmOption.SomeTrips))
            {
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.QuartersOnly:
                        for (int i = 0; i < (int)ms.RhythmSpecs.TimeSignature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                        };
                        break;

                    case SubDivisionTier.QuartersAndEighths:
                        for (int i = 0; i < (int)ms.RhythmSpecs.TimeSignature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.N);
                        };
                        break;

                    case SubDivisionTier.EighthsOnly:
                        for (int i = 0; i < (int)ms.RhythmSpecs.TimeSignature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.N);
                        };
                        break;
                    case SubDivisionTier.EighthsAndSixteenths:
                        for (int i = 0; i < (int)ms.RhythmSpecs.TimeSignature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.E);
                            beats.Add(SubBeatAssignment.N);
                            beats.Add(SubBeatAssignment.A);
                        };
                        break;
                    case SubDivisionTier.SixteenthsOnly:
                        for (int i = 0; i < (int)ms.RhythmSpecs.TimeSignature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.E);
                            beats.Add(SubBeatAssignment.N);
                            beats.Add(SubBeatAssignment.A);
                        };
                        break;
                }
            }

            if (ms.RhythmSpecs.RhythmOptions.Contains(RhythmOption.SomeTrips))
            {
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.QuartersOnly:
                        for (int i = 0; i < (int)ms.RhythmSpecs.TimeSignature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                        };
                        break;

                    case SubDivisionTier.QuartersAndEighths:
                        for (int i = 0; i < (int)ms.RhythmSpecs.TimeSignature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.T);
                            beats.Add(SubBeatAssignment.N);
                            beats.Add(SubBeatAssignment.L);
                        };
                        break;

                    case SubDivisionTier.EighthsOnly:
                        for (int i = 0; i < (int)ms.RhythmSpecs.TimeSignature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.T);
                            beats.Add(SubBeatAssignment.N);
                            beats.Add(SubBeatAssignment.L);
                        };
                        break;
                }
            }

            if (ms.RhythmSpecs.RhythmOptions.Contains(RhythmOption.TripsOnly))
            {
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.QuartersOnly:
                        for (int i = 0; i < (int)ms.RhythmSpecs.TimeSignature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                        };
                        break;

                    case SubDivisionTier.QuartersAndEighths:
                        for (int i = 0; i < (int)ms.RhythmSpecs.TimeSignature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.T);
                            beats.Add(SubBeatAssignment.L);
                        };
                        break;

                    case SubDivisionTier.EighthsOnly:
                        for (int i = 0; i < (int)ms.RhythmSpecs.TimeSignature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.T);
                            beats.Add(SubBeatAssignment.L);
                        };
                        break;
                }
            }

            return beats.ToArray();
        }
        //    string GetString(int i)
        //    {
        //        return ms.RhythmSpecs.SubDivisionTier switch
        //        {

        //            SubDivisionTier.QuartersOnly => BeatIndexToNoteLocation(i).Item2 switch
        //            {
        //                Count.One => "1",
        //                Count.Two => "2",
        //                Count.Thr => "3",
        //                Count.For => "4",
        //                _ => ""
        //            },
        //            SubDivisionTier.QuartersAndEighths => (BeatIndexToNoteLocation(i).Item2 switch
        //            {
        //                BeatLocation.One => "1",
        //                BeatLocation.Two => "2",
        //                BeatLocation.Thr => "3",
        //                BeatLocation.For => "4",
        //                BeatLocation.OneN => "+",
        //                BeatLocation.TwoN => "+",
        //                BeatLocation.ThrN => "+",
        //                BeatLocation.ForN => "+",
        //                _ => ""
        //            },
        //            SubDivisionTier.EighthsOnly => BeatIndexToNoteLocation(i).Item2 switch
        //            {
        //                BeatLocation.One => "1",
        //                BeatLocation.Two => "2",
        //                BeatLocation.Thr => "3",
        //                BeatLocation.For => "4",
        //                BeatLocation.OneN => "+",
        //                BeatLocation.TwoN => "+",
        //                BeatLocation.ThrN => "+",
        //                BeatLocation.ForN => "+",
        //                _ => ""
        //            },
        //            SubDivisionTier.EighthsAndSixteenths => BeatIndexToNoteLocation(i).Item2 switch
        //            {
        //                BeatLocation.One => "1",
        //                BeatLocation.Two => "2",
        //                BeatLocation.Thr => "3",
        //                BeatLocation.For => "4",
        //                BeatLocation.OneN => "+",
        //                BeatLocation.TwoN => "+",
        //                BeatLocation.ThrN => "+",
        //                BeatLocation.ForN => "+",
        //                BeatLocation.OneE => "e",
        //                BeatLocation.TwoE => "e",
        //                BeatLocation.ThrE => "e",
        //                BeatLocation.ForE => "e",
        //                BeatLocation.OneA => "a",
        //                BeatLocation.TwoA => "a",
        //                BeatLocation.ThrA => "a",
        //                BeatLocation.ForA => "a",
        //                _ => ""
        //            },

        //            SubDivisionTier.SixteenthsOnly => BeatIndexToNoteLocation(i).Item2 switch
        //            {
        //                BeatLocation.One => "1",
        //                BeatLocation.Two => "2",
        //                BeatLocation.Thr => "3",
        //                BeatLocation.For => "4",
        //                BeatLocation.OneN => "+",
        //                BeatLocation.TwoN => "+",
        //                BeatLocation.ThrN => "+",
        //                BeatLocation.ForN => "+",
        //                BeatLocation.OneE => "e",
        //                BeatLocation.TwoE => "e",
        //                BeatLocation.ThrE => "e",
        //                BeatLocation.ForE => "e",
        //                BeatLocation.OneA => "a",
        //                BeatLocation.TwoA => "a",
        //                BeatLocation.ThrA => "a",
        //                BeatLocation.ForA => "a",
        //                _ => ""
        //            },
        //        };
        //        return BeatIndexToNoteLocation(i).Item2 switch
        //        {
        //            BeatLocation.One => "1",
        //            BeatLocation.Two => "2",
        //            BeatLocation.Thr => "3",
        //            BeatLocation.For => "4",
        //            BeatLocation.OneN => "+",
        //            BeatLocation.TwoN => "+",
        //            BeatLocation.ThrN => "+",
        //            BeatLocation.ForN => "+",
        //            BeatLocation.OneE => "",
        //            BeatLocation.TwoE => "",
        //            BeatLocation.ThrE => "",
        //            BeatLocation.ForE => "",
        //            BeatLocation.OneA => "",
        //            BeatLocation.TwoA => "",
        //            BeatLocation.ThrA => "",
        //            BeatLocation.ForA => "",
        //            BeatLocation.OneT => "",
        //            BeatLocation.TwoT => "",
        //            BeatLocation.ThrT => "",
        //            BeatLocation.ForT => "",
        //            BeatLocation.OneL => "",
        //            BeatLocation.TwoL => "",
        //            BeatLocation.ThrL => "",
        //            BeatLocation.ForL => "",
        //            _ => "s",
        //        };
        //    //}
        //}


        //static Vector2 ScaledToFit(Vector2 pointA, Vector2 pointB)
        //{
        //    return new Vector2(pointB.x - pointA.x, 1);
        //}

        //public static (MeasureNumber, Count, SubBeatAssignment) BeatIndexToNoteLocation(int beatIndex)
        //{
        //    return beatIndex switch
        //    {
        //        00 + 00 => (MeasureNumber.One, Count.One, SubBeatAssignment.D),
        //        01 + 00 => (MeasureNumber.One, Count.One, SubBeatAssignment.E),
        //        02 + 00 => (MeasureNumber.One, Count.One, SubBeatAssignment.N),
        //        03 + 00 => (MeasureNumber.One, Count.One, SubBeatAssignment.A),
        //        04 + 00 => (MeasureNumber.One, Count.Two, SubBeatAssignment.D),
        //        05 + 00 => (MeasureNumber.One, Count.Two, SubBeatAssignment.E),
        //        06 + 00 => (MeasureNumber.One, Count.Two, SubBeatAssignment.N),
        //        07 + 00 => (MeasureNumber.One, Count.Two, SubBeatAssignment.A),
        //        08 + 00 => (MeasureNumber.One, Count.Thr, SubBeatAssignment.D),
        //        09 + 00 => (MeasureNumber.One, Count.Thr, SubBeatAssignment.E),
        //        10 + 00 => (MeasureNumber.One, Count.Thr, SubBeatAssignment.N),
        //        11 + 00 => (MeasureNumber.One, Count.Thr, SubBeatAssignment.A),
        //        12 + 00 => (MeasureNumber.One, Count.For, SubBeatAssignment.D),
        //        13 + 00 => (MeasureNumber.One, Count.For, SubBeatAssignment.E),
        //        14 + 00 => (MeasureNumber.One, Count.For, SubBeatAssignment.N),
        //        15 + 00 => (MeasureNumber.One, Count.For, SubBeatAssignment.A),
        //        00 + 16 => (MeasureNumber.Two, Count.One, SubBeatAssignment.D),
        //        01 + 16 => (MeasureNumber.Two, Count.One, SubBeatAssignment.E),
        //        02 + 16 => (MeasureNumber.Two, Count.One, SubBeatAssignment.N),
        //        03 + 16 => (MeasureNumber.Two, Count.One, SubBeatAssignment.A),
        //        04 + 16 => (MeasureNumber.Two, Count.Two, SubBeatAssignment.D),
        //        05 + 16 => (MeasureNumber.Two, Count.Two, SubBeatAssignment.E),
        //        06 + 16 => (MeasureNumber.Two, Count.Two, SubBeatAssignment.N),
        //        07 + 16 => (MeasureNumber.Two, Count.Two, SubBeatAssignment.A),
        //        08 + 16 => (MeasureNumber.Two, Count.Thr, SubBeatAssignment.D),
        //        09 + 16 => (MeasureNumber.Two, Count.Thr, SubBeatAssignment.E),
        //        10 + 16 => (MeasureNumber.Two, Count.Thr, SubBeatAssignment.N),
        //        11 + 16 => (MeasureNumber.Two, Count.Thr, SubBeatAssignment.A),
        //        12 + 16 => (MeasureNumber.Two, Count.For, SubBeatAssignment.D),
        //        13 + 16 => (MeasureNumber.Two, Count.For, SubBeatAssignment.E),
        //        14 + 16 => (MeasureNumber.Two, Count.For, SubBeatAssignment.N),
        //        15 + 16 => (MeasureNumber.Two, Count.For, SubBeatAssignment.A),
        //        00 + 32 => (MeasureNumber.Thr, Count.One, SubBeatAssignment.D),
        //        01 + 32 => (MeasureNumber.Thr, Count.One, SubBeatAssignment.E),
        //        02 + 32 => (MeasureNumber.Thr, Count.One, SubBeatAssignment.N),
        //        03 + 32 => (MeasureNumber.Thr, Count.One, SubBeatAssignment.A),
        //        04 + 32 => (MeasureNumber.Thr, Count.Two, SubBeatAssignment.D),
        //        05 + 32 => (MeasureNumber.Thr, Count.Two, SubBeatAssignment.E),
        //        06 + 32 => (MeasureNumber.Thr, Count.Two, SubBeatAssignment.N),
        //        07 + 32 => (MeasureNumber.Thr, Count.Two, SubBeatAssignment.A),
        //        08 + 32 => (MeasureNumber.Thr, Count.Thr, SubBeatAssignment.D),
        //        09 + 32 => (MeasureNumber.Thr, Count.Thr, SubBeatAssignment.E),
        //        10 + 32 => (MeasureNumber.Thr, Count.Thr, SubBeatAssignment.N),
        //        11 + 32 => (MeasureNumber.Thr, Count.Thr, SubBeatAssignment.A),
        //        12 + 32 => (MeasureNumber.Thr, Count.For, SubBeatAssignment.D),
        //        13 + 32 => (MeasureNumber.Thr, Count.For, SubBeatAssignment.E),
        //        14 + 32 => (MeasureNumber.Thr, Count.For, SubBeatAssignment.N),
        //        15 + 32 => (MeasureNumber.Thr, Count.For, SubBeatAssignment.A),
        //        00 + 48 => (MeasureNumber.For, Count.One, SubBeatAssignment.D),
        //        01 + 48 => (MeasureNumber.For, Count.One, SubBeatAssignment.E),
        //        02 + 48 => (MeasureNumber.For, Count.One, SubBeatAssignment.N),
        //        03 + 48 => (MeasureNumber.For, Count.One, SubBeatAssignment.A),
        //        04 + 48 => (MeasureNumber.For, Count.Two, SubBeatAssignment.D),
        //        05 + 48 => (MeasureNumber.For, Count.Two, SubBeatAssignment.E),
        //        06 + 48 => (MeasureNumber.For, Count.Two, SubBeatAssignment.N),
        //        07 + 48 => (MeasureNumber.For, Count.Two, SubBeatAssignment.A),
        //        08 + 48 => (MeasureNumber.For, Count.Thr, SubBeatAssignment.D),
        //        09 + 48 => (MeasureNumber.For, Count.Thr, SubBeatAssignment.E),
        //        10 + 48 => (MeasureNumber.For, Count.Thr, SubBeatAssignment.N),
        //        11 + 48 => (MeasureNumber.For, Count.Thr, SubBeatAssignment.A),
        //        12 + 48 => (MeasureNumber.For, Count.For, SubBeatAssignment.D),
        //        13 + 48 => (MeasureNumber.For, Count.For, SubBeatAssignment.E),
        //        14 + 48 => (MeasureNumber.For, Count.For, SubBeatAssignment.N),
        //        15 + 48 => (MeasureNumber.For, Count.For, SubBeatAssignment.A),
        //        _ => (MeasureNumber.One, Count.One, SubBeatAssignment.D)
        //    };
        //}
        //}
    }
}