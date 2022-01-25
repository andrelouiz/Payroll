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
                Console.WriteLine("ID\tFirst Name\tHour Rate\tWorked Hours\tGross Pay\tNet Pay ");
                Console.WriteLine("......................................................................................");

                List<Workers> worker = new List<Workers>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Workers>));
                using (FileStream load = File.Open(@"Workers.xml", FileMode.Open))
                worker = (List<Workers>)xml.Deserialize(load);
                foreach (var w in worker)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    w.Payroll();
                }
                Return();
            }

            static void Print()
            {
                Console.WriteLine("Write the employee's ID number you'd like to print");
                var id = Console.ReadLine();

                List<Paycheck> workers = new List<Paycheck>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Paycheck>));
                using (FileStream load = File.Open(@"Workers.xml", FileMode.Open))
                    workers = (List<Paycheck>)xml.Deserialize(load); // fix the 

                foreach (var w in workers)
                {
                    w.Printing();
                    string fileName = @"Payslip.txt";
                    StreamWriter writer = new StreamWriter(fileName);
                }
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
