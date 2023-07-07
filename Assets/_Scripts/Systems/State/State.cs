using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class State
{
    #region REFERENCES

    #endregion REFERENCES

    #region STATE SYSTEMS
    ///These state systems are organized in order of execution

    /// <summary>
    /// Called by SetStateDirectly() and InitiateFade().
    /// </summary>
    protected void DisableInput()
    {
        InputKey.ButtonEvent -= GPInput;
        InputKey.StickEvent -= GPStickInput;
        InputKey.RStickAltXEvent -= RAltXInput;
        InputKey.RStickAltYEvent -= RAltYInput;
        InputKey.MouseClickEvent -= Clicked;
        ReadRStickAlt = false;
    }

    /// <summary>
    /// Called by SetStateDirectly() and FadeOutToBlack().
    /// </summary>
    protected virtual void DisengageState() { }

    /// <summary>
    /// Called by SetStateDirectly() and FadeOutToBlack(). Don't set new states here.
    /// </summary>
    protected virtual void PrepareState(Action callback) { callback(); }

    /// <summary>
    /// Called by SetSceneDirectly() and FadeInToScene().
    /// </summary>
    protected void EnableInput()
    {
        InputKey.ButtonEvent += GPInput;
        InputKey.StickEvent += GPStickInput;
        InputKey.RStickAltXEvent += RAltXInput;
        InputKey.RStickAltYEvent += RAltYInput;
        InputKey.MouseClickEvent += Clicked;
        RStickAltReadLoop();
    }

    /// <summary>
    /// Called by SetStateDirectly() and FadeInToScene(). OK to set new states here.
    /// </summary>
    protected virtual void EngageState() { }

    protected void SetStateDirectly(State newState)
    {
        DisableInput();
        DisengageState();

        newState.PrepareState(Initiate);

        async void Initiate()
        {
            await Task.Yield();

            newState.EnableInput();
            newState.EngageState();
        }
    }

    protected void FadeToState(State newState)
    {
        ScreenFader fader = new ScreenFader();
        InitiateFade(newState);
        return;

        async void InitiateFade(State newState)
        {
            DisableInput();
            await Task.Yield();
            FadeOutToBlack(newState);
        }

        async void FadeOutToBlack(State newState)
        {
            while (fader.Screen.color.a < .99f)
            {
                await Task.Yield();
                if (!Application.isPlaying) return;
                fader.Screen.color += new Color(0, 0, 0, Time.deltaTime * 1.25f);
            }

            fader.Screen.color = Color.black;
            await Task.Yield();
            newState.PrepareState(FadeInToScene);
        }

        async void FadeInToScene()
        {
            DisengageState();

            while (fader.Screen.color.a > .01f)
            {
                await Task.Yield();
                if (!Application.isPlaying) return;
                fader.Screen.color -= new Color(0, 0, 0, Time.deltaTime * 2.0f);
            }

            fader?.SelfDestruct();
            newState.EnableInput();
            newState.EngageState();
        }
    }
    #endregion STATE SYSTEMS



    #region INPUT HANDLING


    protected virtual void Clicked(MouseAction action, Vector2 position)
    {
        if (action == MouseAction.LUp)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
            if (hit.collider != null) ClickedOn(hit.collider.gameObject);
        }
    }

    protected virtual void ClickedOn(GameObject go)
    {
        Debug.Log(go.name);
    }

    protected virtual void Holding(GameObject go) { }

    protected virtual void UnClicked() { }


    private void GPInput(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.Up_Press: DirectionPressed(Dir.Up); break;
            case GamePadButton.Down_Press: DirectionPressed(Dir.Down); break;
            case GamePadButton.Left_Press: DirectionPressed(Dir.Left); break;
            case GamePadButton.Right_Press: DirectionPressed(Dir.Right); break;
            case GamePadButton.North_Press: InteractPressed(); break;
            case GamePadButton.East_Press: ConfirmPressed(); break;
            case GamePadButton.South_Press: CancelPressed(); break;
            case GamePadButton.Start_Press: StartPressed(); break;
            //case GamePadButton.Select_Press: SelectPressed(); break;
            case GamePadButton.R1_Press: break;
            case GamePadButton.R2_Press: break;
            case GamePadButton.R3_Press: break;
            case GamePadButton.L1_Press: break;
            case GamePadButton.L2_Press: break;
            case GamePadButton.L3_Press: break;


            case GamePadButton.Up_Release: DirectionPressed(Dir.Reset); break;
            case GamePadButton.Down_Release: DirectionPressed(Dir.Reset); break;
            case GamePadButton.Left_Release: DirectionPressed(Dir.Reset); break;
            case GamePadButton.Right_Release: DirectionPressed(Dir.Reset); break;
            case GamePadButton.North_Release: break;
            case GamePadButton.East_Release: break;
            case GamePadButton.South_Release: break;
            case GamePadButton.Start_Release: break;
            //case GamePadButton.Select_Release: break;
            case GamePadButton.R1_Release: break;
            case GamePadButton.R2_Release: break;
            case GamePadButton.R3_Release: break;
            case GamePadButton.L1_Release: break;
            case GamePadButton.L2_Release: break;
            case GamePadButton.L3_Release: break;
        };
    }

    private void GPStickInput(GamePadButton gpi, Vector2 v2)
    {
        switch (gpi)
        {
            case GamePadButton.LStick: LStickInput(v2); break;
            case GamePadButton.RStick: RStickInput(v2); break;
        }
    }

    protected virtual void DirectionPressed(Dir dir) { }

    protected virtual void ConfirmPressed() { }

    protected virtual void InteractPressed() { }

    protected virtual void CancelPressed() { }

    protected virtual void StartPressed() { }

    ///FIXME I have to ask that we don't use the Select button in this game because macOS
    ///FIXME has some how bound that key on my controller to open a game search screen, and I can't disable it. =/ -Pino
    //protected virtual void SelectPressed() { }

    protected virtual void LStickInput(Vector2 v2) { }

    protected virtual void RStickInput(Vector2 v2) { }

    ///nintendo switch R stick
    private bool ReadRStickAlt;
    private bool NewRStickAltThisFrame;

    private float _rStickAltX;
    private float RStickAltX { get => _rStickAltX; set { NewRStickAltThisFrame = true; _rStickAltX = value; } }
    private float _rStickAltY;
    private float RStickAltY { get => _rStickAltY; set { NewRStickAltThisFrame = true; _rStickAltY = value; } }

    private Vector2 RStickAlt => new(RStickAltX, RStickAltY);

    private void RAltXInput(float f) => RStickAltX = f;
    private void RAltYInput(float f) => RStickAltY = f;

    private async void RStickAltReadLoop()
    {
        ReadRStickAlt = true;
        while (ReadRStickAlt)
        {
            if (!NewRStickAltThisFrame)
            {
                await Task.Yield();
                if (!Application.isPlaying) return;
                continue;
            }

            RStickInput(RStickAlt);
            NewRStickAltThisFrame = false;

            await Task.Yield();
            if (!Application.isPlaying) return;
        }
    }


    #endregion INPUT HANDLING
}