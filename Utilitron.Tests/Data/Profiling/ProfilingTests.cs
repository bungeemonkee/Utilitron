using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.CodeDom.Compiler;
using System.Data.Common;
using System.Linq;
using Utilitron.Data.Profiling;

namespace Utilitron.Tests.Data.Profiling
{
    [TestClass]
    //[ExcludeFromCodeCoverage]
    [GeneratedCode("Testing", "")] // TODO: .NET Core won't have [ExcludeFromCodeCoverage] until 2.0 so this is temporary
    public class ProfilingTests
    {
        [TestMethod]
        public void Profiling_Command_Fires_CommandExecuteEnd_When_DbDataReader_NextResult_Returns_False()
        {
            var connectionMock = new Mock<DbConnection>(MockBehavior.Strict);
            var transactionMock = new Mock<DbTransaction>(MockBehavior.Strict);

            var dataReaderMock = new Mock<DbDataReader>(MockBehavior.Strict);
            dataReaderMock.Setup(x => x.NextResult())
                .Returns(false);

            var testingCommand = new TestingCommand(dataReaderMock.Object);
            testingCommand.Connection = connectionMock.Object;
            testingCommand.Transaction = transactionMock.Object;

            var beforeExecuteReader = false;
            var beforeNextResult = false;
            var afterNextResult = false;
            var firedBeforeNextResult = false;
            var firedDuringNextResult = false;

            var command = new ProfilingCommand(testingCommand);
            command.CommandExecuteEnd += (s, e) =>
            {
                if (afterNextResult)
                {
                    firedBeforeNextResult = false;
                    firedDuringNextResult = false;
                    return;
                }

                if (beforeNextResult)
                {
                    firedBeforeNextResult = false;
                    firedDuringNextResult = true;
                    return;
                }

                if (beforeExecuteReader)
                {
                    firedBeforeNextResult = false;
                    firedDuringNextResult = false;
                    return;
                }
            };

            beforeExecuteReader = true;
            var reader = command.ExecuteReader();
            beforeNextResult = true;
            reader.NextResult();
            afterNextResult = true;

            command.Dispose();

            Assert.IsFalse(firedBeforeNextResult);
            Assert.IsTrue(firedDuringNextResult);

            dataReaderMock.VerifyAll();
            connectionMock.VerifyAll();
            transactionMock.VerifyAll();
        }

        [TestMethod]
        public void Profiling_Command_Fires_CommandExecuteEnd_When_DbDataReader_Closed()
        {
            var connectionMock = new Mock<DbConnection>(MockBehavior.Strict);
            var transactionMock = new Mock<DbTransaction>(MockBehavior.Strict);

            var dataReaderMock = new Mock<DbDataReader>(MockBehavior.Strict);
            dataReaderMock.Setup(x => x.Close());

            var testingCommand = new TestingCommand(dataReaderMock.Object);
            testingCommand.Connection = connectionMock.Object;
            testingCommand.Transaction = transactionMock.Object;

            var beforeExecuteReader = false;
            var beforeClose = false;
            var afterClose = false;
            var firedBeforeClose = false;
            var firedDuringClose = false;

            var command = new ProfilingCommand(testingCommand);
            command.CommandExecuteEnd += (s, e) =>
            {
                if (afterClose)
                {
                    firedBeforeClose = false;
                    firedDuringClose = false;
                    return;
                }

                if (beforeClose)
                {
                    firedBeforeClose = false;
                    firedDuringClose = true;
                    return;
                }

                if (beforeExecuteReader)
                {
                    firedBeforeClose = false;
                    firedDuringClose = false;
                    return;
                }
            };

            beforeExecuteReader = true;
            var reader = command.ExecuteReader();
            beforeClose = true;
            reader.Close();
            afterClose = true;

            command.Dispose();

            Assert.IsFalse(firedBeforeClose);
            Assert.IsTrue(firedDuringClose);

            dataReaderMock.VerifyAll();
            connectionMock.VerifyAll();
            transactionMock.VerifyAll();
        }

        [TestMethod]
        public void Profiling_Command_Fires_CommandExecuteEnd_When_DbDataReader_Enumerator_Ends()
        {
            var connectionMock = new Mock<DbConnection>(MockBehavior.Strict);
            var transactionMock = new Mock<DbTransaction>(MockBehavior.Strict);

            var dataReaderMock = new Mock<DbDataReader>(MockBehavior.Strict);
            dataReaderMock.Setup(x => x.GetEnumerator())
                .Returns(Enumerable.Empty<DataRowAttribute>().GetEnumerator());

            var testingCommand = new TestingCommand(dataReaderMock.Object);
            testingCommand.Connection = connectionMock.Object;
            testingCommand.Transaction = transactionMock.Object;

            var beforeMoveNext = false;
            var afterMoveNext = false;
            var firedCorrectly = false;

            var command = new ProfilingCommand(testingCommand);
            command.CommandExecuteEnd += (s, e) =>
            {
                if (afterMoveNext)
                {
                    return;
                }

                if (beforeMoveNext)
                {
                    firedCorrectly = true;
                    return;
                }
            };

            var reader = command.ExecuteReader();
            var enumerator = reader.GetEnumerator();
            beforeMoveNext = true;
            var result = enumerator.MoveNext();
            afterMoveNext = true;

            command.Dispose();

            Assert.IsTrue(firedCorrectly);

            dataReaderMock.VerifyAll();
            connectionMock.VerifyAll();
            transactionMock.VerifyAll();
        }

        [TestMethod]
        public void Profiling_Command_Fires_CommandExecuteEnd_Only_Once()
        {
            var connectionMock = new Mock<DbConnection>(MockBehavior.Strict);
            var transactionMock = new Mock<DbTransaction>(MockBehavior.Strict);

            var dataReaderMock = new Mock<DbDataReader>(MockBehavior.Strict);
            dataReaderMock.Setup(x => x.Close());
            dataReaderMock.Setup(x => x.NextResult())
                .Returns(false);
            dataReaderMock.Setup(x => x.GetEnumerator())
                .Returns(Enumerable.Empty<DataRowAttribute>().GetEnumerator());

            var testingCommand = new TestingCommand(dataReaderMock.Object);
            testingCommand.Connection = connectionMock.Object;
            testingCommand.Transaction = transactionMock.Object;

            var fireCount = 0;

            var command = new ProfilingCommand(testingCommand);
            command.CommandExecuteEnd += (s, e) =>
            {
                ++fireCount;
            };

            var reader = command.ExecuteReader();
            var enumerator = reader.GetEnumerator();
            enumerator.MoveNext();
            reader.NextResult();
            reader.Close();

            command.Dispose();

            Assert.AreEqual(1, fireCount);

            dataReaderMock.VerifyAll();
            connectionMock.VerifyAll();
            transactionMock.VerifyAll();
        }
    }
}
