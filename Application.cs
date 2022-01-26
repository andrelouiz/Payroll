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

                List<Workers> worker = new List<Workers>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Workers>));
                using (FileStream load = File.Open(@"Workers.xml", FileMode.Open))
                    worker = (List<Workers>)xml.Deserialize(load);
      


                foreach (var w in worker)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(w.ID + "\t" + w.Name + "\t \t " + w.WorkedHours + "\t \t " + w.OvertimeHours + "\t \t " + w.OvertimePay + "\t \t " + w.GrossPay + "\t\t" + w.NetPay);
                }

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Workers>));
                StreamWriter sw = new StreamWriter("Workers.xml");
                xmlSerializer.Serialize(sw, worker);
                sw.Close();

                Return();
            }

            static void Print()
            {
                Console.Clear();
                Console.WriteLine("Write the employee's ID number you'd like to print");
                string id = Console.ReadLine();
                Console.Clear();
                List<Workers> workers = new List<Workers>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Workers>));
                using (FileStream load = File.Open(@"Workers.xml", FileMode.Open))
                workers = (List<Workers>)xml.Deserialize(load);
                
                foreach (var w in workers)
                {
                    if (id.Equals(w.ID))
                    {
                        using (var writer = new StreamWriter(@"Payslip.txt")) //NOT WORKING
                        {
                            Console.WriteLine("...............................................");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("----- * PAYCHECK - BRIARCLIFF HOSPITAL * -----");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("...............................................");
                            Console.WriteLine(" Full Name: " + w.Name + "\n Employee ID:" + w.ID + "\n Profession: "+ w.Profession);
                            Console.WriteLine("...............................................");
                            Console.WriteLine(" Base Hours: " + w.WorkedHours + "\n Base Hourly Pay: " + w.HourRate + "zl" + "\n Overtime Hours: " + w.OvertimeHours + "\n Overtime Pay Hourly: " + w.OTHourly + "zl" + "\n Overtime total: " + w.OvertimePay + "zl");
                            Console.WriteLine("................................................");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\t\t\tGross Total: " + w.GrossPay + "zl");
                            Console.WriteLine("\t\t\tDeductions: " + w.Deductions + "zl");
                            Console.WriteLine("\t\t\tNet Pay: " + w.NetPay + "zl");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("................................................");
                        }
                    }
                }

                Return();
            }
        

            static void Expenses()
            {
                List<Workers> expenses = new List<Workers>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Workers>));
                using (FileStream load = File.Open(@"Employees.xml", FileMode.Open))
                    expenses = (List<Workers>)xml.Deserialize(load);
                foreach (var w in expenses)
                {
                    
                }
            }
            static void Add()
            {       
                Console.Clear();
                List<Workers> workerData = new List<Workers>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Workers>));
                using (FileStream load = File.Open(@"Workers.xml", FileMode.Open))
                workerData = (List<Workers>)xml.Deserialize(load);

                Workers worker = new Workers();
                Console.Write("Enter worker ID:");
                worker.ID = Console.ReadLine();
                Console.Write("Name: ");
                worker.Name = Console.ReadLine();
                Console.Write("Pesel Number:");
                worker.Pesel = Console.ReadLine();
                Console.Write("Profession: ");
                worker.Profession = Console.ReadLine();
                Console.Write("Hour Rate: ");
                worker.HourRate = Console.ReadLine();
                Console.Write("Hours worked this month: ");
                worker.WorkedHours = Console.ReadLine();
                Console.Write("Overtime worked this month: ");
                worker.OvertimeHours = Console.ReadLine();
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
