using System;
using System.Diagnostics;

namespace Computer_Info
{
    internal class Program
    {

        static void netstat()
        {
            Process ipconfig = new Process();
            ipconfig.StartInfo.FileName = "ipconfig";
            ipconfig.StartInfo.Arguments = "/all";
            ipconfig.StartInfo.RedirectStandardOutput = true;
            ipconfig.StartInfo.UseShellExecute = false;
            ipconfig.StartInfo.CreateNoWindow = true;

            // Start het process
            ipconfig.Start();

            // Lees de output van het process
            string result = ipconfig.StandardOutput.ReadToEnd();
            ipconfig.WaitForExit();

            // Maak de output zichtbaar.
            foreach (var line in result.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (line.Contains("IPv4 Address") || line.Contains("IPv6 Address") || line.Contains("Subnet Mask") || line.Contains("DNS Server") || line.Contains("Physical Address"))
                {
                    Console.WriteLine("Netstat: " + line.Trim());
                }
            }

        }

        static void hostname()
        {
            Process hostname = new Process();
            hostname.StartInfo.FileName = "hostname";
            hostname.StartInfo.RedirectStandardOutput = true;
            hostname.StartInfo.UseShellExecute = false;
            hostname.StartInfo.CreateNoWindow = true;

            hostname.Start();

            string resulth = hostname.StandardOutput.ReadToEnd();
            hostname.WaitForExit();

            Console.WriteLine("De hostname: " + resulth.Trim());
        }
        private static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                // Groet gebruiker, En geef informatie.
                Console.WriteLine("Hallo Gebruiker,");
                Console.WriteLine("\nU hebt dit script geinstalleerd om informatie op te vragen over uw device op een makkelijkere en snellere manier.");

                // Geef de keuzes weer
                //string A = "1. Netstat (Ip Address (IPV4) / (IPV6), DNS Address, MAC Address, Subnet Mask)";
                //string B = "2. Hard Drive informatie";
                //string C = "3. Hostname (Naam van de laptop)";
                //string D = "4. Test optie";
                //string E = "9. Exit";

                string[] keuze = { "\nMaak nu uw keuze:\n", "1. Netstat (Ip Address (IPV4) / (IPV6), DNS Address, MAC Address, Subnet Mask);\n", "2. Hard Drive informatie\n", "3. Hostname (Naam van de laptop\n", "9. Exit\n" };

                foreach (string i in keuze)
                {

                    //Console.WriteLine("\nMaak nu uw keuze:\n");
                    //Console.WriteLine(A + "    |    " + B);
                    //Console.WriteLine(C + "    |    ");//D);
                    Console.WriteLine(i);

                }

                // Get user input
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                char inputChar = keyInfo.KeyChar;

                switch (inputChar)
                {
                    case '1':
                        netstat();
                        break;

                    case '2':
                        Console.WriteLine("Hard Drive informatie: Functie nog niet gemaakt.");
                        break;

                    case '3':
                        hostname();
                        break;

                    //case '4':
                    //Console.WriteLine("Onbekend wat ik hier ga maken");
                    //break;

                    case '9':
                        // Exit de loop
                        exit = true;
                        Console.WriteLine("Script wordt afgesloten! Bedankt voor het gebruiken.");
                        break;

                    default:
                        Console.WriteLine("Ongeldige invoer. Probeer opnieuw.");
                        break;
                }

                // Wacht tot de gebruik een knop heeft ingedrukt om het menu opnieuw zichtbaar te maken
                Console.WriteLine("\nDruk op een toets om verder te gaan...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}