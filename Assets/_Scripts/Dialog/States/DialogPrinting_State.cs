using System.Threading.Tasks;
using UnityEngine;
using Dialog;
using System;

public class DialogPrinting_State : State
{
    public DialogPrinting_State(Dialog.Dialog dialog, State subsequentState)
    {
        Dialog = dialog;
        SubsequentState = subsequentState;
    }

    readonly State SubsequentState;
    readonly Dialog.Dialog Dialog;
    bool waitingForInput;

    protected override void PrepareState(Action callback)
    {
        if (Dialog.CurrentLine.VideoClip != null)
        {
            float width = Cam.Io.Camera.orthographicSize * Cam.Io.Camera.aspect * 1.65f;
            Dialog.VideoPlayer.transform.localScale = new Vector3(width, width / (float)(Dialog.CurrentLine.VideoClip.width / Dialog.CurrentLine.VideoClip.height), 1);
            Dialog.VideoPlayer.gameObject.SetActive(true);
            Dialog.VideoPlayer.playOnAwake = false;
            Dialog.VideoPlayer.waitForFirstFrame = false;
            Dialog.VideoPlayer.isLooping = false;
            Dialog.VideoPlayer.clip = Dialog.CurrentLine.VideoClip;
            Dialog.VideoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;
            Dialog.VideoPlayer.Prepare();
            LoadVideo(callback);
            return;
        }


        Dialog.VideoPlayer.gameObject.SetActive(false);
        callback();
    }

    async void LoadVideo(Action callback)
    {
        while (!Dialog.VideoPlayer.isPrepared)
        {
            if (!Application.isPlaying) return;
            await Task.Yield();
        }
        callback();
    }

    protected override void EngageState()
    {
        if (Dialog.CurrentLine.VideoClip != null) { Dialog.VideoPlayer.Play(); }
        //if (Dialog.Dialogue.PlayTypingSounds) { Audio.SoundFX.PlayClip(Assets.TypingClicks); }
        Dialog.NPCIcon(Dialog.CurrentLine);
        Dialog.PrintDialog(FinishedPrinting);
    }

    protected override void Clicked(MouseAction action, Vector3 _)
    {
        if (action != MouseAction.LUp) return;

        if (Dialog.LetType)
        {
            Dialog.LetType = false;
            //Audio.SoundFX.StopClip();
        }
        if (!waitingForInput) { return; }

        if (Dialog.HasNextLine())
        {
            Dialog.SetNextLine();
            SetStateDirectly(new DialogPrinting_State(Dialog, SubsequentState));
            return;
        }
        else if (Dialog.HasNextState())
        {
            Debug.Log("Dialog.HasOutcome");
            SetStateDirectly(new EndDialog_State(Dialog, Dialog.CurrentLine.NextState, Dialog.CurrentLine.FadeOut));
            return;
        }
        else if (Dialog.HasNextDialogue())
        {
            Dialog.Dialogue = Dialog.CurrentLine.NextDialogue
                  .SetSpeakerColor(Dialog.CurrentLine.SpeakerColor)
                  .SetSpeakerIcon(Dialog.CurrentLine.SpeakerIcon)
                  .SetSpeakerName(Dialog.CurrentLine.SpeakerName)
                  .Initiate();

            Dialog.CurrentLine = Dialog.Dialogue.FirstLine;

            SetStateDirectly(new DialogPrinting_State(Dialog, SubsequentState));
            return;
        }



    }


    void FinishedPrinting()
    {
        //Audio.SoundFX.StopClip();
        if (Dialog.HasResponses())
        {
            Debug.Log("Dialog.HasResponses");
            //Dialog.CurrentLine = Dialog.NextLine();
            this.SetStateDirectly(new DialogResponse_State(Dialog, SubsequentState));
            return;
        }

        waitingForInput = true;
    }

}
