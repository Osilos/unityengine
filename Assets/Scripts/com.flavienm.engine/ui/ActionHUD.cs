﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.flavienm.engine.ui
{
    public class ActionHUD : MonoBehaviour
    {
        public delegate void EventHUD();
        public static EventHUD OnPlay;

        public void OnPlayButton ()
        {
            if (OnPlay != null)
            {
                OnPlay();
            }
        }
    }
}