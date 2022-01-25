using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Payroll
{
    class Application
    {
        public static void Start()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
    ┌─┐┌─┐┬ ┬┬─┐┌─┐┬  ┬    ┌─┐┬ ┬┌─┐┌┬┐┌─┐┌┬┐
    ├─┘├─┤└┬┘├┬┘│ ││  │    └─┐└┬┘└─┐ │ ├┤ │││
    ┴  ┴ ┴ ┴ ┴└─└─┘┴─┘┴─┘  └─┘ ┴ └─┘ ┴ └─┘┴ ┴
    ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t1 - Employee's data\n \t2 - Wage calculator\n \t3 - Salary expenses\n \t4 - Add employee\n \t5 - Exit Application");

            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        Wage();
                        break;
                    case "2":
                        Calculator();
                        break;
                    case "3":
                        Expenses();
                        break;
                    case "4":
                        Add();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                }
            }

            static void Wage()
            {
                Console.WriteLine("Write employee's name");
                List<Payroll> wage = new List<Payroll>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Payroll>));
                using (FileStream load = File.Open(@"Workers.xml", FileMode.Open))
                wage = (List<Payroll>)xml.Deserialize(load);
                
                foreach (var pay in wage)
                {
                    double hrate = Convert.ToDouble(pay.HourRate);
                    double hworked = Convert.ToDouble(pay.WorkedHours);
                    double gpay = hrate * hworked;
                    pay.GrossPay = gpay;

                    double tax1 = pay.GrossPay * pay.Taxes;
                    double npay = tax1 - pay.GrossPay;
                    pay.NetPay = npay;

                    Console.WriteLine("Pay" + pay.Lastname + pay.Profession + pay.WorkedHours + pay.HourRate + pay.GrossPay + pay.NetPay);
                };
            }

            static void Calculator()
            {

            }

            static void Expenses()
            {

            }

            static void Add()
            {       
                Console.Clear();
                List<Workers> workerData = new List<Workers>();
                /*XmlSerializer xml = new XmlSerializer(typeof(List<Workers>));
                using (FileStream load = File.Open(@"Worker.xml", FileMode.Open))
                workerData = (List<Workers>)xml.Deserialize(load);*/

                Workers worker = new Workers();
                Console.Write("Enter worker ID:");
                worker.ID = Console.ReadLine();
                Console.Write("First Name: ");
                worker.Firstname = Console.ReadLine();
                Console.Write("Last Name: ");
                worker.Lastname = Console.ReadLine();
                Console.Write("Pesel Number:");
                worker.Pesel = Console.ReadLine();
                Console.Write("Profession: ");
                worker.Profession = Console.ReadLine();
                Console.Write("Hour Rate: ");
                worker.HourRate = Console.ReadLine();
                Console.Write("Hours worked month: ");
                worker.WorkedHours = Console.ReadLine();
                
                workerData.Add(worker);

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Workers>));
                StreamWriter sw = new StreamWriter("Workers.xml");
                xmlSerializer.Serialize(sw, workerData);
                sw.Close();
                Console.WriteLine("Employee added sucessfully");

                Return();
            }
        }
        public static void Return()
        {
            Console.WriteLine("Press enter to return to main menu...");
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.Enter)
            {
                Start();
            }

        }
        
    }
}
