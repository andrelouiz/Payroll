using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Payroll
{   
    [Serializable]
    public class Workers
    {
        [XmlElement("ID")]
        public string ID { get; set; }
        [XmlElement("Firstname")]
        public string Firstname { get; set; }
        [XmlElement("Lastname")]
        public string Lastname { get; set; }
        [XmlElement("Profession")]
        public string Profession { get; set; }
        [XmlElement("HourRate")]
        public string HourRate { get; set; }
        protected string pesel;
        [XmlElement("Pesel")]
        public string Pesel 
        {
            get
            { return pesel;  }
            set
            { pesel = value; }
        }
        [XmlElement("WorkedHours")]
        public string WorkedHours;

        protected double grossPay;
        public double GrossPay
        {
            get { return grossPay; }
            set { grossPay = value; }
        }
        protected double netPay;
        public double NetPay
        {
            get { return netPay; }
            set { netPay = value; }
        }
        protected double taxes = 0.35;
        public double Taxes
        {
            get { return taxes; }
            set { taxes = value; }
        }

        public void List()
        {
            Console.WriteLine("Employee ID: "+ ID + "\tFirst Name: "+ Firstname+"\tLast Name: "+Lastname+"\tPESEL: "+Pesel+"\t Profession: "+Profession);
        }
        public void Payroll()
        {
            double hrate = Convert.ToDouble(HourRate);
            double hworked = Convert.ToDouble(WorkedHours);
            double gpay = hrate * hworked;
            GrossPay = gpay;

            double tax1 = GrossPay * Taxes;
            double npay = GrossPay - tax1;
            NetPay = npay;

            Console.WriteLine(ID+"\t"+Firstname+"\t\t"+HourRate+"zl"+"\t\t"+WorkedHours+"\t\t"+GrossPay+"\t\t"+NetPay);
        }
    }
  
    [Serializable]
    public class Paycheck : Workers
    {
        protected double TotalPay { get; set; }
        public double Check
        {   get { return GrossPay; }
            set { GrossPay = value; }
        }
        public void Printing()
        {
            Console.WriteLine(Firstname, Check);
        }
    }
}
