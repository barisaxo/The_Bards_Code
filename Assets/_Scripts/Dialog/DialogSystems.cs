using System;
using UnityEngine;
using System.Threading.Tasks;

namespace Dialog
{
    public static class DialogSystems
    {
        private const string clearFlag = "<alpha=#00>";

        public static void PrintDialog(this Dialog dialog, Action callback)
        {
            dialog.LetType = true;
            dialog.DialogCard.SetTextString(string.Empty);

            TypeDialogViaColor(0, dialog, callback);

            static async void TypeDialogViaColor(int charMarker, Dialog dialog, Action callback)
            {
                while (dialog.LetType)
                {
                    if (!Application.isPlaying) return;

                    string printingDialogue = dialog.CurrentLine.SpeakerName;

                    for (int i = 0; i < dialog.CurrentLine.SpeakerText.Length; i++)
                    {
                        printingDialogue += dialog.CurrentLine.SpeakerText[i];
                        if (charMarker == i) { printingDialogue += clearFlag; }
                    }

                    dialog.DialogCard.SetTextString(printingDialogue);

                    if (++charMarker == dialog.CurrentLine.SpeakerText.Length)
                    {
                        dialog.LetType = false;
                    }
                    await Task.Delay(30);
                    //yield return new WaitForSecondsRealtime(.025f);
                }

                dialog.DialogCard.SetTextString(dialog.CurrentLine.SpeakerName + dialog.CurrentLine.SpeakerText);
                callback();
            }
        }

        public static void SetCurrentLine(this Dialog dialog, Line nextLine) { dialog.CurrentLine = nextLine; }

        static Line NextLine(this Dialog dialog) { return dialog.CurrentLine.NextLine; }
        public static void SetNextLine(this Dialog dialog) { dialog.CurrentLine = dialog.NextLine(); }
        public static bool HasNextLine(this Dialog dialog) { return dialog.CurrentLine.NextLine != null; }

        public static Line GoToLine(Response response) { return response.GoToLine; }
        public static bool HasGoToLine(this Response response) { return response.GoToLine != null; }

        //public static bool HasPlayerAction(this Response response) { return response.PlayerAction != null; }

        public static bool HasResponses(this Dialog dialog) { return dialog.CurrentLine.Responses != null; }
        public static Response[] Responses(this Dialog dialog) { return dialog.CurrentLine.Responses; }

        public static bool HasNextState(this Response response) { return response.NextState != null; }
        public static bool HasNextState(this Dialog dialog) { return dialog.CurrentLine.NextState != null; }

        public static bool HasNextDialogue(this Response response) { return response.GoToDialogue != null; }
        public static bool HasNextDialogue(this Dialog dialog) { return dialog.CurrentLine.NextDialogue != null; }

        // public static PlayerAction? GetAction(this Response response)
        // {
        //     return response.PlayerAction;
        // }

    }
}