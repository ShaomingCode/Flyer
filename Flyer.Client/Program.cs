using System;
using System.Collections.Generic;
using Flyer.Extensions;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace Flyer.Client
{
    class Program
    {
        public static ConcurrentDictionary<long, long> dict = new ConcurrentDictionary<long, long>();
        static void Main(string[] args)
        {
            int i = 10;
            while (i > 0)
            {
                Task.Factory.StartNew(SequenceTest);
                --i;
            }

            Console.WriteLine("Hello World!");

            Console.ReadLine();
        }

        public static void SequenceTest()
        {
            int i = 100000;
            int failNum = 0;
            while (i > 0)
            {
                if (!dict.TryAdd(SequenceHelper.NextId(), Thread.CurrentThread.ManagedThreadId))
                {
                    ++failNum;
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} {failNum}");
                }
                --i;
            }
            Console.WriteLine($" {Thread.CurrentThread.ManagedThreadId} {failNum}");
        }

        public static void SerializerTest()
        {
            Console.WriteLine("Json");
            var aStr = SerializerHelper.ToJson(GetTestA());
            Console.WriteLine(aStr);
            var a = SerializerHelper.FromJson<TestA>(aStr);
            Console.WriteLine($"{a.Name} {a.Age} {a.BirthDate} {string.Join("'", a.Skills)}");


            Console.WriteLine("xml");
            aStr = SerializerHelper.ToXml(a);
            Console.WriteLine(aStr);
            a = SerializerHelper.FromXml<TestA>(aStr);
            Console.WriteLine($"{a.Name} {a.Age} {a.BirthDate} {string.Join("'", a.Skills)}");
        }


        public static TestA GetTestA()
        {
            var result = new TestA()
            {
                Name = "shaoming",
                Age = 27,
                BirthDate = DateTime.Parse("1990-11-14"),
                Skills = new List<string>() { "C#", "SQL", "dotnet", "db" }
            };
            return result;
        }




    }


    public class TestA
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public IList<string> Skills { get; set; }

    }

}
