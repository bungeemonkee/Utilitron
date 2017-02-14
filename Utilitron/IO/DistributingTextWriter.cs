using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitron.IO
{
    /// <summary>
    ///     A text writer that simply delegates all writing to one or more delegate <see cref="TextWriter" />s.
    /// </summary>
    public class DistributingTextWriter : TextWriter
    {
        private readonly TextWriter[] _writers;

        /// <summary>
        ///     See <see cref="TextWriter.Encoding" />.
        /// </summary>
        /// <exception cref="InvalidOperationException">All the delegate writers do not share the same encoding.</exception>
        public override Encoding Encoding
        {
            get
            {
                var encoding = _writers.First().Encoding;
                if (_writers.Any(writer => !Equals(writer.Encoding, encoding)))
                {
                    throw new InvalidOperationException("The dependent TextWriters do not all share the same encoding.");
                }
                return encoding;
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.NewLine" />.
        /// </summary>
        public override string NewLine
        {
            get { return _writers.First().NewLine; }
            set
            {
                foreach (var writer in _writers)
                {
                    writer.NewLine = value;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <exception cref="ArgumentNullException">Argument 'writers' is null.</exception>
        /// <exception cref="ArgumentException">Argument 'writers' does not contain at least one <see cref="TextWriter" />.</exception>
        /// <param name="writers">The collection of <see cref="TextWriter" />s to distribute all writing to.</param>
        public DistributingTextWriter(params TextWriter[] writers)
        {
            if (writers == null)
            {
                throw new ArgumentNullException(nameof(writers));
            }

            if (writers.Length < 1)
            {
                throw new ArgumentException("At least one TextWriter is required.", nameof(writers));
            }

            _writers = writers;
        }

        /// <summary>
        ///     See <see cref="TextWriter.Flush()" />.
        /// </summary>
        public override void Flush()
        {
            FlushAsync().Wait();
        }

        /// <summary>
        ///     See <see cref="TextWriter.FlushAsync()" />.
        /// </summary>
        public override Task FlushAsync()
        {
            var tasks = _writers.Select(x => x.FlushAsync());
            return Task.WhenAll(tasks);
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(bool)" />.
        /// </summary>
        public override void Write(bool value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(char)" />.
        /// </summary>
        public override void Write(char value)
        {
            WriteAsync(value).Wait();
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(char[])" />.
        /// </summary>
        public override void Write(char[] buffer)
        {
            WriteAsync(buffer).Wait();
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(char[],int,int)" />.
        /// </summary>
        public override void Write(char[] buffer, int index, int count)
        {
            WriteAsync(buffer, index, count).Wait();
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(double)" />.
        /// </summary>
        public override void Write(double value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(float)" />.
        /// </summary>
        public override void Write(float value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(int)" />.
        /// </summary>
        public override void Write(int value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(long)" />.
        /// </summary>
        public override void Write(long value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(object)" />.
        /// </summary>
        public override void Write(object value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(uint)" />.
        /// </summary>
        public override void Write(uint value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(ulong)" />.
        /// </summary>
        public override void Write(ulong value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(decimal)" />.
        /// </summary>
        public override void Write(decimal value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(string)" />.
        /// </summary>
        public override void Write(string value)
        {
            WriteAsync(value).Wait();
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(string, object)" />.
        /// </summary>
        public override void Write(string format, object arg0)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg0);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(string,object,object)" />.
        /// </summary>
        public override void Write(string format, object arg0, object arg1)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg0, arg1);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(string,object,object,object)" />.
        /// </summary>
        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg0, arg1);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.Write(string,object[])" />.
        /// </summary>
        public override void Write(string format, params object[] arg)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteAsync(char)" />.
        /// </summary>
        public override Task WriteAsync(char value)
        {
            var tasks = _writers.Select(x => x.WriteAsync(value));
            return Task.WhenAll(tasks);
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteAsync(char[],int,int)" />.
        /// </summary>
        public override Task WriteAsync(char[] buffer, int index, int count)
        {
            var tasks = _writers.Select(x => x.WriteAsync(buffer, index, count));
            return Task.WhenAll(tasks);
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteAsync(string)" />.
        /// </summary>
        public override Task WriteAsync(string value)
        {
            var tasks = _writers.Select(x => x.WriteAsync(value));
            return Task.WhenAll(tasks);
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(bool)" />.
        /// </summary>
        public override void WriteLine(bool value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(char)" />.
        /// </summary>
        public override void WriteLine(char value)
        {
            WriteLineAsync(value).Wait();
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(char[])" />.
        /// </summary>
        public override void WriteLine(char[] buffer)
        {
            WriteLineAsync(buffer).Wait();
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(char[],int,int)" />.
        /// </summary>
        public override void WriteLine(char[] buffer, int index, int count)
        {
            WriteLineAsync(buffer, index, count).Wait();
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(double)" />.
        /// </summary>
        public override void WriteLine(double value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(float)" />.
        /// </summary>
        public override void WriteLine(float value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(int)" />.
        /// </summary>
        public override void WriteLine(int value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(long)" />.
        /// </summary>
        public override void WriteLine(long value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(object)" />.
        /// </summary>
        public override void WriteLine(object value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(uint)" />.
        /// </summary>
        public override void WriteLine(uint value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(ulong)" />.
        /// </summary>
        public override void WriteLine(ulong value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(decimal)" />.
        /// </summary>
        public override void WriteLine(decimal value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine()" />.
        /// </summary>
        public override void WriteLine()
        {
            WriteLineAsync().Wait();
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(string)" />.
        /// </summary>
        public override void WriteLine(string value)
        {
            WriteLineAsync(value).Wait();
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(string, object)" />.
        /// </summary>
        public override void WriteLine(string format, object arg0)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg0);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(string, object, object)" />.
        /// </summary>
        public override void WriteLine(string format, object arg0, object arg1)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg0, arg1);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(string, object, object, object)" />.
        /// </summary>
        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLine(string, object[])" />.
        /// </summary>
        public override void WriteLine(string format, params object[] arg)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg);
            }
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLineAsync()" />.
        /// </summary>
        public override Task WriteLineAsync()
        {
            var tasks = _writers.Select(x => x.WriteLineAsync());
            return Task.WhenAll(tasks);
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLineAsync(char)" />.
        /// </summary>
        public override Task WriteLineAsync(char value)
        {
            var tasks = _writers.Select(x => x.WriteLineAsync(value));
            return Task.WhenAll(tasks);
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLineAsync(string)" />.
        /// </summary>
        public override Task WriteLineAsync(string value)
        {
            var tasks = _writers.Select(x => x.WriteLineAsync(value));
            return Task.WhenAll(tasks);
        }

        /// <summary>
        ///     See <see cref="TextWriter.WriteLineAsync(char[],int,int)" />.
        /// </summary>
        public override Task WriteLineAsync(char[] buffer, int index, int count)
        {
            var tasks = _writers.Select(x => x.WriteLineAsync(buffer, index, count));
            return Task.WhenAll(tasks);
        }

        /// <summary>
        ///     See <see cref="TextWriter.Dispose(bool)" />.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            foreach (var writer in _writers)
            {
                writer.Dispose();
            }
        }
    }
}