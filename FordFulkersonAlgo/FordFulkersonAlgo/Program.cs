using System;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace FordFulkersonAlgo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FordFulkersonAlgo algo = new FordFulkersonAlgo();
            algo.GetData();
            Console.WriteLine("Max network flow: " + algo.GetMaxGraphFlow());

        }



    }
}
