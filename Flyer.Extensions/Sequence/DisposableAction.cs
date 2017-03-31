using System;
using System.Collections.Generic;
using System.Text;

namespace Flyer.Extensions.Sequence
{
    /// <summary>
    /// DisposableAction
    /// </summary>
    public class DisposableAction : IDisposable
    {

        readonly Action _action;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="action"></param>
        public DisposableAction(Action action)
        {
            _action = action ?? throw new ArgumentNullException("action");
        }

        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
            _action();
        }
    }
}
