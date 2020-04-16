using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnowflakeId.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [TestMethod]
        public void NextId_Duplicate_issues_2_Test()
        {
            var snowflakeIdWorker = new SnowflakeIdWorker(1, 1);
            var blockingCollection = new BlockingCollection<long>();
            var tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                var task = Task.Run(() =>
                {
                    for (int j = 0; j < 10000; j++)
                    {
                        blockingCollection.Add(snowflakeIdWorker.NextId());
                    }
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            Assert.AreEqual(1000000, blockingCollection.Distinct().Count());
        }

        [TestMethod]
        public void Next_Base36_Test()
        {
            var snowflakeIdWorker = new SnowflakeIdWorker(1, 1);
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(snowflakeIdWorker.Next());
            }
        }

        [TestMethod]
        public void Next_Base36_Decode_Test()
        {
            var snowflakeIdWorker = new SnowflakeIdWorker(1, 1);
            var nextId = snowflakeIdWorker.NextId();
            var encode = Base36Converter.Encode(nextId);
            var decode = Base36Converter.Decode(encode);

            Console.WriteLine(encode);
            Assert.AreEqual(nextId, decode);
        }
    }
}