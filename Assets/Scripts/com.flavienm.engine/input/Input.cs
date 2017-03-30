using UnityEngine;
using System.Collections;

namespace com.flavienm.engine.input
{
    public class Input : EngineObject
    {
        public delegate void DirectionEvent(Vector3 position, Vector3 direction);
        public delegate void PositionEvent(Vector3 position);
        public delegate void InputEvent();

        public static InputEvent left;
        public static InputEvent right;

        protected void DispatchLeftEvent()
        {
            if (left != null)
            {
                left();
            }
        }
        protected void DispatchRighEvent()
        {
            if (right != null)
            {
                right();
            }
        }
    }
}