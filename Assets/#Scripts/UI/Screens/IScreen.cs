using System;

public interface IScreen
{
    EScreenType ScreenType {get;}
    void DoShow(bool force = false, Action<IScreen> callback = null);
    void DoHide(bool force = false, Action<IScreen> callback = null);
}
