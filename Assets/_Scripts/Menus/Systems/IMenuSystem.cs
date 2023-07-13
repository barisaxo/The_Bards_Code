using UnityEngine;

public interface IMenuSystem
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="go">GameObject to interact with.</param>
    public void InteractWith(GameObject go);
    public void AdjustSetting();
    public void ActivateTexts();
    public void UpdateTexts();
    public void ColorTexts();
    public void Destroy();
    public void Disable();
    public void Enable();

}