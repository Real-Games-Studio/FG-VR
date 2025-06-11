public interface ISelectable
{
    /// <summary>Inicia o “hold” (quando o gatilho é pressionado)</summary>
    void OnSelect();

    /// <summary>Cancela o “hold” (quando o gatilho é solto antes de completar)</summary>
    void OnDeselect();
}
