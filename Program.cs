using System;
using System.Diagnostics;

namespace Computer_Info
{
    internal class Program
    {

        static void Netstat()
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

        static void Hostname()
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

        static async Task Drive()
        {
            //Bericht dat de gegevens opgehaald worden omdat het even duurt.
            Console.WriteLine("Drive informatie aan het verkrijgen...");

            Process drive = new Process();

            //Zorg dat powershell opstart
            drive.StartInfo.FileName = "powershell.exe";

            //Commando wordt uitgevoerd
            drive.StartInfo.Arguments = "-Command Get-PhysicalDisk";
            drive.StartInfo.RedirectStandardOutput = true;
            drive.StartInfo.UseShellExecute = false;
            drive.StartInfo.CreateNoWindow = true;

            //start het process
            drive.Start();

            //sync de uitkomst.
            string resulth = await drive.StandardOutput.ReadToEndAsync();

            //wacht tot het process klaar is
            await drive.WaitForExitAsync();

            //Uitkomst
            Console.WriteLine(resulth.Trim());
        }

    private static async Task Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                // Groet gebruiker, En geef informatie.
                Console.WriteLine("Hallo Gebruiker,");
                Console.WriteLine("\nU hebt dit script geinstalleerd om informatie op te vragen over uw device op een makkelijkere en snellere manier.");

                string[] keuze = { "\nMaak nu uw keuze:\n", "1. Netstat (Ip Address (IPV4) / (IPV6), DNS Address, MAC Address, Subnet Mask);\n", "2. Hard Drive informatie\n", "3. Hostname (Naam van de laptop\n", "9. Exit\n" };

                foreach (string i in keuze)
                {
                    Console.WriteLine(i);
                }

                // Get user input
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                char inputChar = keyInfo.KeyChar;

                switch (inputChar)
                {
                    case '1':
                        Netstat();
                        break;

                    case '2':
                        await Drive();
                        break;

                    case '3':
                        Hostname();
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