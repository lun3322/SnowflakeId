using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnowflakeId.Core;

namespace SnowflakeId.Test
{
    [TestClass]
    public class SnowflakeIdWorkerTest
    {
        [TestMethod]
        public void NextId_Test()
        {
            var worker = new SnowflakeIdWorker(0, 0);

            var id1 = worker.NextId();
            var id2 = worker.NextId();

            Assert.IsTrue(id1 > 0);
            Assert.IsTrue(id2 > 0);
            Assert.IsTrue(id1 != id2);
            Console.WriteLine(id1);
            Console.WriteLine(id2);
        }
    }
}
