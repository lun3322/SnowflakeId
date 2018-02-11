using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnowflakeId.Core;
using BenchmarkDotNet.Attributes;

namespace SnowflakeId
{
    public class SnowFlakeBenchmarks
    {
        private SnowflakeIdWorker Worker { get; set; }

        public SnowFlakeBenchmarks()
        {
            Worker = new SnowflakeIdWorker(0, 0);
        }

        [Benchmark]
        public long GenerateId() => Worker.NextId();
    }
}
