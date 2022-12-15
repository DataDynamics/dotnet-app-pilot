using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDynamics.Common.Utils
{
    /// <summary>
    /// A simple SimpleStopWatch implemetion for use in Performance Tests
    /// </summary>
    /// <example>
    /// The following example shows the usage:
    /// <code>
    /// SimpleStopWatch watch = new SimpleStopWatch();
    /// using (watch.Start("Duration: {0}"))
    /// {
    ///    // do some work...
    /// }
    /// </code>
    /// The format string passed to the start method is used to print the result message. 
    /// The example above will print <c>Duration: 0.123s</c>.
    /// </example>
    /// <author>Erich Eichinger</author>
    public class SimpleStopWatch
    {
        private DateTime _startTime;
        private TimeSpan _elapsed;

        private class Stopper : IDisposable
        {
            private readonly SimpleStopWatch _owner;
            private readonly string _format;

            public Stopper(SimpleStopWatch owner, string format)
            {
                _owner = owner;
                _format = format;
            }

            public void Dispose()
            {
                _owner.Stop(_format);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Starts the timer and returns and "handle" that must be disposed to stop the timer.
        /// </summary>
        /// <param name="outputFormat">the output format string, that is used to render the result message. Use '{0}' to print the elapsed timespan.</param>
        /// <returns>the handle to dispose for stopping the timer</returns>
        public IDisposable Start(string outputFormat)
        {
            Stopper stopper = new Stopper(this, outputFormat);
            _startTime = DateTime.Now;
            return stopper;
        }

        private void Stop(string outputFormat)
        {
            _elapsed = DateTime.Now.Subtract(_startTime);
            if (outputFormat != null)
            {
                Console.WriteLine(outputFormat, _elapsed);
            }
        }

        public DateTime StartTime
        {
            get { return _startTime; }
        }

        public TimeSpan Elapsed
        {
            get { return _elapsed; }
        }
    }
}