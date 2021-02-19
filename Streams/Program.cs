using System;
using System.IO;
using System.Threading;

namespace Streams
{
    internal class Program
    {
        private static int x = 0;
        private static int sch = 0;
        private static Mutex mutex = new Mutex();
        private static void Main(string[] args)
        {
            File.Delete("state.out.txt");
            Thread thread1 = new Thread(Thread1);
            thread1.Start();
            Thread thread2 = new Thread(Thread2);
            thread2.Start();
        }
         
        private static void Thread1()
        {
            var str = File.ReadAllText("number.in.T1.txt");
            //File.Delete("state.out.txt");
            for (int i = 0; i < str.Length; i++)
            {
                mutex.WaitOne();
                if (int.Parse(str[i].ToString()) % 2 == 0)
                {
                    x += int.Parse(str[i].ToString());
                }
                File.AppendAllText("state.out.txt",$"Время {DateTime.Now.ToString("hh:mm:ss:ffff")} Процент {String.Format("{0:f4}",(i+1)/double.Parse(str.Length.ToString()) *100)} Решение {x}\n");
                sch = i;
                Thread.Sleep(900);
                mutex.ReleaseMutex();
            }
        }

        private static void Thread2()
        {
            var str = File.ReadAllText("number.in.T1.txt");
            for (int i = 0; i < str.Length; i++)
            {
                mutex.WaitOne();
                File.AppendAllText("state.out.txt", $"Делегат Время {DateTime.Now.ToString("hh:mm:ss:ffff")} Процент {String.Format("{0:f4}", (sch + 1) / double.Parse(str.Length.ToString()) * 100)} Решение {x}\n");
                sch = i;
                Thread.Sleep(1000);
                mutex.ReleaseMutex();
            }
        }
    }
}