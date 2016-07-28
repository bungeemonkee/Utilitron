using System;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Utilitron.IO
{
    public class DistributingTextWriter : TextWriter
    {
        private readonly TextWriter[] _writers;

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

        public override Encoding Encoding
        {
            get
            {
                var encoding = _writers.First().Encoding;
                if (_writers.Any(writer => writer.Encoding != encoding))
                {
                    throw new InvalidOperationException("The dependent TextWriters do not all share the same encoding.");
                }
                return encoding;
            }
        }

        public override void Close()
        {
            foreach (var writer in _writers)
            {
                writer.Close();
            }
        }

        protected override void Dispose(bool disposing)
        {
            foreach (var writer in _writers)
            {
                writer.Dispose();
            }
        }

        public override ObjRef CreateObjRef(Type requestedType)
        {
            throw new NotImplementedException();
        }

        public override void Flush()
        {
            FlushAsync().Wait();
        }

        public override Task FlushAsync()
        {
            var tasks = _writers.Select(x => x.FlushAsync());
            return Task.WhenAll(tasks);
        }

        public override object InitializeLifetimeService()
        {
            throw new NotImplementedException();
        }

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

        public override void Write(bool value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        public override void Write(char value)
        {
            WriteAsync(value).Wait();
        }

        public override void Write(char[] buffer)
        {
            WriteAsync(buffer).Wait();
        }

        public override void Write(char[] buffer, int index, int count)
        {
            WriteAsync(buffer, index, count).Wait();
        }

        public override void Write(double value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        public override void Write(float value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        public override void Write(int value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        public override void Write(long value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        public override void Write(object value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        public override void Write(uint value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        public override void Write(ulong value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        public override void Write(decimal value)
        {
            foreach (var writer in _writers)
            {
                writer.Write(value);
            }
        }

        public override void WriteLine(bool value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void WriteLine(char value)
        {
            WriteLineAsync(value).Wait();
        }

        public override void WriteLine(char[] buffer)
        {
            WriteLineAsync(buffer).Wait();
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            WriteLineAsync(buffer, index, count).Wait();
        }

        public override void WriteLine(double value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void WriteLine(float value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void WriteLine(int value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void WriteLine(long value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void WriteLine(object value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void WriteLine(uint value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void WriteLine(ulong value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void WriteLine(decimal value)
        {
            foreach (var writer in _writers)
            {
                writer.WriteLine(value);
            }
        }

        public override Task WriteAsync(char value)
        {
            var tasks = _writers.Select(x => x.WriteAsync(value));
            return Task.WhenAll(tasks);
        }

        public override Task WriteAsync(char[] buffer, int index, int count)
        {
            var tasks = _writers.Select(x => x.WriteAsync(buffer, index, count));
            return Task.WhenAll(tasks);
        }

        public override Task WriteAsync(string value)
        {
            var tasks = _writers.Select(x => x.WriteAsync(value));
            return Task.WhenAll(tasks);
        }

        public override Task WriteLineAsync()
        {
            var tasks = _writers.Select(x => x.WriteLineAsync());
            return Task.WhenAll(tasks);
        }

        public override void WriteLine()
        {
            WriteLineAsync().Wait();
        }

        public override Task WriteLineAsync(char value)
        {
            var tasks = _writers.Select(x => x.WriteLineAsync(value));
            return Task.WhenAll(tasks);
        }

        public override Task WriteLineAsync(string value)
        {
            var tasks = _writers.Select(x => x.WriteLineAsync(value));
            return Task.WhenAll(tasks);
        }

        public override Task WriteLineAsync(char[] buffer, int index, int count)
        {
            var tasks = _writers.Select(x => x.WriteLineAsync(buffer, index, count));
            return Task.WhenAll(tasks);
        }

        public override void Write(string value)
        {
            WriteAsync(value).Wait();
        }

        public override void Write(string format, object arg0)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg0);
            }
        }

        public override void Write(string format, object arg0, object arg1)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg0, arg1);
            }
        }

        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg0, arg1);
            }
        }

        public override void Write(string format, params object[] arg)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg);
            }
        }

        public override void WriteLine(string value)
        {
            WriteLineAsync(value);
        }

        public override void WriteLine(string format, object arg0)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg0);
            }
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg0, arg1);
            }
        }

        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg0, arg1, arg2);
            }
        }

        public override void WriteLine(string format, params object[] arg)
        {
            foreach (var writer in _writers)
            {
                writer.Write(format, arg);
            }
        }
    }
}
