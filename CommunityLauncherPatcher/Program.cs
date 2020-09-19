using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityLauncherPatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                return;
            }

            string caller = args[0];
            string callerId = args[1];
            string sourceFile = args[2];
            string targetFolder = args[3];

            Console.WriteLine("Caller: " + caller);
            Console.WriteLine("callerId: " + callerId);
            Console.WriteLine("sourceFile: " + sourceFile);
            Console.WriteLine("targetFolder: " + targetFolder);

            Console.ReadLine();
        }
    }
}
