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
            Console.WriteLine("\t1 - Employee's data\n \t2 - Payroll List\n \t3 - Print Paycheck\n \t4 - Salary expenses\n \t5 - Add employee\n \t6 - Exit Application\n");

            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        List();
                        break;
                    case "2":
                        PayrollList();
                        break;
                    case "3":
                        Print();
                        break;
                    case "4":
                        Expenses();
                        break;
                    case "5":
                        Add();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                }
            }

            static void List()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Employee's Database");

                List<Workers> worker = new List<Workers>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Workers>));
                using (FileStream load = File.Open(@"Workers.xml", FileMode.Open))
                worker = (List<Workers>)xml.Deserialize(load);

                foreach (var pay in worker)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    pay.List();
                }
                Return();
            }

            static void PayrollList()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\t\tPAYROLL LIST\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("ID\tFull Name\t\tWorked Hours\tOvertime\tOvertime Pay\tGross Pay\tNet Pay ");
                Console.WriteLine("..............................................................................................................");

                List<Pay> pay = new List<Pay>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Pay>));
                using (FileStream load = File.Open(@"Pay.xml", FileMode.Open))
                    pay = (List<Pay>)xml.Deserialize(load);

                foreach (var p in pay)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    p.Payroll();
                }

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Pay>));
                StreamWriter sw = new StreamWriter("Pay.xml");
                xmlSerializer.Serialize(sw, pay);
                sw.Close();

                Return();
            }

            static void Print()
            {
                Console.Clear();
                Console.WriteLine("Write the employee's ID number you'd like to print");
                string id = Console.ReadLine();
                Console.Clear();
                List<Pay> payment = new List<Pay>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Pay>));
                using (FileStream load = File.Open(@"Pay.xml", FileMode.Open))
                payment = (List<Pay>)xml.Deserialize(load);
                
                foreach (var pay in payment)
                {
                    if (id.Equals(pay.ID))
                    {   
                        Console.WriteLine("...............................................");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("----- * PAYCHECK - BRIARCLIFF HOSPITAL * -----");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("...............................................");
                        Console.WriteLine(" Full Name: " + pay.Name + "\n PESEL Number: " + pay.Pesel + "\n Employee ID: " + pay.ID + "\n Profession: " + pay.Profession);
                        Console.WriteLine("...............................................");
                        Console.WriteLine(" Base Hours: " + pay.WorkedHours + "\n Base Hourly Pay: " + pay.HourRate + "zl" + "\n Overtime Hours: " + pay.OvertimeHours + "\n Overtime Pay Hourly: " + pay.OTHourly + "zl" + "\n Overtime total: " + pay.OvertimePay + "zl");
                        Console.WriteLine("................................................");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\t\t\tGross Total: " + pay.GrossPay + "zl");
                        Console.WriteLine("\t\t\tDeductions: " + pay.Deductions + "zl");
                        Console.WriteLine("\t\t\tNet Pay: " + pay.NetPay + "zl");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("................................................");
                    }
                }
                Return();
            }
        
            static void Expenses()
            {
                List<Pay> expenses = new List<Pay>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Pay>));
                using (FileStream load = File.Open(@"Employees.xml", FileMode.Open))
                    expenses = (List<Pay>)xml.Deserialize(load);
                foreach (var w in expenses)
                {
                    
                }
            }
            static void Add()
            {       
                Console.Clear();
                List<Workers> workerData = new List<Workers>();
                List<Pay> payData = new List<Pay>();

                XmlSerializer xml = new XmlSerializer(typeof(List<Workers>));
                using (FileStream load = File.Open(@"Workers.xml", FileMode.Open))
                workerData = (List<Workers>)xml.Deserialize(load);
                
                XmlSerializer Xml1 = new XmlSerializer(typeof(List<Pay>));
                using (FileStream load = File.Open(@"Pay.xml", FileMode.Open))
                payData = (List<Pay>)Xml1.Deserialize(load);

                Workers worker = new Workers();
                Console.Write("Enter worker ID: ");
                string ID = Console.ReadLine();
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Pesel Number: ");
                string pesel = Console.ReadLine();
                Console.Write("Profession: ");
                string prof = Console.ReadLine();
                worker.ID = ID; worker.Name = name; worker.Pesel = pesel; worker.Profession = prof;
                workerData.Add(worker);

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Workers>));
                StreamWriter sw = new StreamWriter("Workers.xml");
                xmlSerializer.Serialize(sw, workerData);
                sw.Close();

                
                Pay pay = new Pay();
                Console.Write("Hour Rate: ");
                pay.HourRate = Console.ReadLine();
                Console.Write("Hours worked this month: ");
                pay.WorkedHours = Console.ReadLine();
                Console.Write("Overtime worked this month: ");
                pay.OvertimeHours = Console.ReadLine();
                pay.Name = name; pay.ID = ID; pay.Pesel = pesel; pay.Profession = prof;
                payData.Add(pay);

               

                XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<Pay>));
                StreamWriter SW = new StreamWriter("Pay.xml");
                XmlSerializer.Serialize(SW, payData);
                SW.Close();

                Console.WriteLine("Employee added sucessfully");

                Return();
            }
        }
        public static void Return()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n\tPress enter to return to main menu...");
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.Enter)
            {   
                Start();
            }
        }
    }
}
