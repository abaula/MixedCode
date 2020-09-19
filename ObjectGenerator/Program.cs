using System;
using Microsoft.IO;

namespace ObjectGenerator
{
    class Program
    {
        public static readonly RecyclableMemoryStreamManager StreamManager = new RecyclableMemoryStreamManager();

        static void Main(string[] args)
        {
            var array = new [] {1,2,3,4,5,6};
            var span = new Span<int>(array, 1, 3);
            Byte[] bytes3 = BitConverter.GetBytes(2);

            foreach (var number in array)
            {
                var numberButes = ByteConverter.ValueToBytes(number);
                var number2 = ByteConverter.BytesToValue<int>(numberButes);
                
                if(number != number2)
                    throw new InvalidOperationException();
            }
            
            var arrayBytes = ByteConverter.ArrayToBytes(array);
            var array2 = ByteConverter.BytesToArray<int>(arrayBytes);

            var str = "Всем привет! @";
            var strBytes = ByteConverter.StringToBytes(str);
            var str2 = ByteConverter.BytesToString(strBytes);

            var strArr = new[] {"Всем поять привет!", "Всем оПять привет!"};
            var strArrBytes = ByteConverter.StringsToBytes(strArr);
            var strArr2 = ByteConverter.BytesToStrings(strArrBytes);


            var dt = DateTime.Now;
            var dtBytes = ByteConverter.ValueToBytes(dt);
            var dt2 = ByteConverter.BytesToValue<DateTime>(dtBytes);

            var g = Guid.NewGuid();
            var gBytes = ByteConverter.ValueToBytes(g);
            var g2 = ByteConverter.BytesToValue<Guid>(gBytes);


            Console.ReadKey();
        }


    }
}
