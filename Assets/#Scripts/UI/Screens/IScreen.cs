using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IScreen
{
    EScreenType ScreenType {get;}
    void DoShow(bool force = false, Action callback = null);
    void DoHide(bool force = false, Action callback = null);
}
