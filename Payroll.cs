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
        public string ID { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public string HourRate { get; set; }
        protected string pesel;
        public string Pesel
        {
            get
            { return pesel; }
            set
            { pesel = value; }
        }
      
        public void List()
        {
            Console.WriteLine("Employee ID: " + ID + "\tName: " + Name + "\tPESEL: " + Pesel + "\t Profession: " + Profession);
        }      
    }

    [Serializable]
    public class Pay : Workers
    {
        public string WorkedHours { get; set; }
        public string OvertimeHours { get; set; }
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
        public double OvertimePay { get; set; }
        public double OTHourly { get; set; }
        public double Deductions { get; set; }

        public void Payroll()
        {
            double hourRate = Convert.ToDouble(HourRate);
            double hourWorked = Convert.ToDouble(WorkedHours);
            double otHours = Convert.ToDouble(OvertimeHours);

            double otPay = hourRate * 1.5;
            double otTotal = otPay * otHours;

            double grossPay = (hourRate * hourWorked) + otTotal;
            GrossPay = grossPay;

            double fedTaxes = GrossPay * Taxes;
            double netPay = GrossPay - fedTaxes;

            NetPay = netPay;
            OvertimePay = otTotal;
            OTHourly = otPay;
            Deductions = fedTaxes;

            Console.WriteLine(ID + "\t" + Name + "\t \t " + WorkedHours + "\t \t " + OvertimeHours + "\t \t " + OvertimePay + "\t \t " + GrossPay + "\t \t" + NetPay);
        }
    }

    [Serializable]
    public class PaySlip : Pay
    {
        public double TotalExpense { get; set; }

    }
}
