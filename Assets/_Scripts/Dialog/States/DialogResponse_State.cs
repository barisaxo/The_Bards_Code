using UnityEngine;
using Dialog;
using System;

public class DialogResponse_State : State
{
    readonly Dialog.Dialog Dialog;
    readonly State SubsequentState;
    Reply Reply;

    public DialogResponse_State(Dialog.Dialog dialog, State subsequentState)
    {
        Dialog = dialog;
        SubsequentState = subsequentState;
    }

    protected override void PrepareState(Action callback)
    {
        Reply = new Reply(Dialog.CurrentLine.Responses);
        callback();
    }

    protected override void ClickedOn(GameObject go)
    {
        for (int i = 0; i < Reply.ResponseCards.Length; i++)
        {
            if (go == Reply.ResponseCards[i].GO)
            {
                //if (Dialog.CurrentLine.Responses[i].HasPlayerAction())
                //{
                //    switch (Dialog.CurrentLine.Responses[i].PlayerAction)
                //    {
                //        case PlayerAction.BuyRationsLarge: BuyRations(10, 250); break;
                //        case PlayerAction.BuyRationsMedium: BuyRations(5, 150); break;
                //        case PlayerAction.BuyRationsSmall: BuyRations(1, 50); break;
                //        case PlayerAction.BuyMaterialsLarge: BuyMaterials(25, 250); break;
                //        case PlayerAction.BuyMaterialsMedium: BuyMaterials(10, 150); break;
                //        case PlayerAction.BuyMaterialsSmall: BuyMaterials(1, 50); break;
                //        case PlayerAction.RepairLarge: RepairShip(25, 350, 50); break;
                //        case PlayerAction.RepairMedium: RepairShip(10, 250, 20); break;
                //        case PlayerAction.RepairSmall: RepairShip(5, 150, 5); break;
                //        case PlayerAction.BuySextant: BuySextant(); break;
                //        default: Debug.Log("??"); break;
                //    }
                //}

                if (Dialog.CurrentLine.Responses[i].HasGoToLine())
                {
                    Dialog.SetCurrentLine(Dialog.CurrentLine.Responses[i].GoToLine);
                }

                else if (Dialog.CurrentLine.Responses[i].HasNextState())
                {
                    Reply.SelfDestruct();

                    DisengageState();
                    SetStateDirectly(new EndDialog_State(
                        Dialog,
                        Dialog.CurrentLine.Responses[i].NextState,
                        Dialog.CurrentLine.Responses[i].FadeOut));

                    return;
                }
                else if (Dialog.CurrentLine.Responses[i].HasNextDialogue())
                {
                    Dialog.Dialogue =
                        Dialog.CurrentLine.Responses[i].GoToDialogue
                            .SetSpeakerColor(Dialog.CurrentLine.SpeakerColor)
                            .SetSpeakerIcon(Dialog.CurrentLine.SpeakerIcon)
                            .SetSpeakerName(Dialog.CurrentLine.SpeakerName)
                            .Initiate();

                    Dialog.SetCurrentLine(Dialog.Dialogue.FirstLine);
                }

                Reply.SelfDestruct();

                SetStateDirectly(new DialogPrinting_State(Dialog, SubsequentState));

                DisengageState();

                return;
            }
        }
    }

    //private void BuySextant()
    //{
    //    Data.CharacterData.Coins -= 500;
    //    Data.CharacterData.Sextant = true;
    //    //Board.TryInstantiateCromatica(Data.CharacterData, Data.GameplayData);
    //}
    //void BuyRations(int amount, int spent)
    //{
    //    Data.CharacterData.Rations += amount;
    //    Data.CharacterData.Coins -= spent;
    //}
    //void BuyMaterials(int amount, int spent)
    //{
    //    Data.CharacterData.Materials += amount;
    //    Data.CharacterData.Coins -= spent;
    //}
    //void RepairShip(int amount, int spentCoin, int spentMat)
    //{
    //    Data.CharacterData.CurrentHealth += amount;
    //    Data.CharacterData.Materials -= spentMat;
    //    Data.CharacterData.Coins -= spentCoin;
    //}
}
