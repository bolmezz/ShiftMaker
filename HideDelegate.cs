using System;

namespace Mission.GUI
{
    internal class HideDelegate
    {
        private Action hideText;

        public HideDelegate(Action hideText)
        {
            this.hideText = hideText;
        }
    }
}