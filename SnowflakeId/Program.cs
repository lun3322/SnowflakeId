﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace SnowflakeId
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<SnowFlakeBenchmarks>();
        }
    }
}
