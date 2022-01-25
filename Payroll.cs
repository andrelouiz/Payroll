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
    }
    [Serializable]
    public class Payroll : Workers
    {
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
    }
    [Serializable]
    public class Paycheck : Payroll
    {
        protected double TotalPay { get; set; }
    }
}
