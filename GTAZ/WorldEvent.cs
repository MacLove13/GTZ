
using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace GTAZ {

    public delegate void WorldEventChanged(object sender, EventArgs e);

    public abstract class WorldEvent {

        protected event WorldEventChanged Begin, End;

        private readonly string _id;

        protected WorldEvent(string id) {
            _id = id;
        }

        /// <summary>
        /// Start this WorldEvent.
        /// </summary>
        public void Start() {

            if (Begin != null) Begin(this, EventArgs.Empty);

            Event();

            if (End != null) End(this, EventArgs.Empty);

        }

        /// <summary>
        /// This is the main event of this WorldEvent.
        /// </summary>
        protected abstract void Event();

        /// <summary>
        /// Returns the event-specific identifier of this WorldEvent.
        /// </summary>
        /// <returns></returns>
        public string GetId() {
            return _id;
        }

    }

}
