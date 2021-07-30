using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    /// <summary>
    /// Is used in communication between the game environment and the BoardController.
    /// </summary>
    interface ISetupTool
    {
        /// <summary>
        /// The description to display in the settings.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The method to setup using of the specific setting.
        /// </summary>
        void Setup();
    }
}
