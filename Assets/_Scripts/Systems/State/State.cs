using System;
using System.Collections;
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
        MonoHelper.OnUpdate -= RStickAltReadLoop;
        MonoHelper.OnUpdate -= UpdateStickInput;
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
        MonoHelper.OnUpdate += RStickAltReadLoop;
        MonoHelper.OnUpdate += UpdateStickInput;
    }

    /// <summary>
    /// Called by SetStateDirectly() and FadeInToScene(). OK to set new states here.
    /// </summary>
    protected virtual void EngageState() { }

    protected void SetStateDirectly(State newState)
    {
        DisableInput();
        DisengageState();

        newState.PrepareState(Initiate().StartCoroutine);

        IEnumerator Initiate()
        {
            yield return null;
            newState.EnableInput();
            newState.EngageState();
        }
    }

    protected void FadeToState(State newState)
    {
        ScreenFader fader = new();
        InitiateFade(newState).StartCoroutine();
        return;

        IEnumerator InitiateFade(State newState)
        {
            DisableInput();
            yield return null;
            FadeOutToBlack(newState).StartCoroutine();
        }

        IEnumerator FadeOutToBlack(State newState)
        {
            while (fader.Screen.color.a < .99f)
            {
                yield return null;
                fader.Screen.color += new Color(0, 0, 0, Time.deltaTime * 1.25f);
            }

            fader.Screen.color = Color.black;

            yield return null;
            newState.PrepareState(FadeInToScene().StartCoroutine);
        }

        IEnumerator FadeInToScene()
        {
            DisengageState();

            while (fader.Screen.color.a > .01f)
            {
                yield return null;
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

    private Vector2 LStick;
    private Vector2 RStick;

    private void GPStickInput(GamePadButton gpi, Vector2 v2)
    {
        switch (gpi)
        {
            case GamePadButton.LStick: LStick = v2; break;
            case GamePadButton.RStick: RStick = v2; break;
        }
    }

    private void UpdateStickInput()
    {
        if (LStick != Vector2.zero) LStickInput(LStick);
        if (RStick != Vector2.zero) RStickInput(RStick);
    }

    ///nintendo switch R stick
    private bool NewRStickAltThisFrame;

    private float _rStickAltX;
    private float RStickAltX { get => _rStickAltX; set { NewRStickAltThisFrame = true; _rStickAltX = value; } }
    private float _rStickAltY;
    private float RStickAltY { get => _rStickAltY; set { NewRStickAltThisFrame = true; _rStickAltY = value; } }

    private Vector2 RStickAlt => new(RStickAltX, RStickAltY);

    private void RAltXInput(float f) => RStickAltX = f;
    private void RAltYInput(float f) => RStickAltY = f;

    private void RStickAltReadLoop()
    {
        if (!NewRStickAltThisFrame) return;
        RStickInput(RStickAlt);
        NewRStickAltThisFrame = false;
    }


    #endregion INPUT HANDLING
}