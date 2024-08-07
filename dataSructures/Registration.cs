using System;

namespace ListMaster
{
    public class Registration
    {
        public string Address { get; set; }
        public string Type { get; set; }
        public string Period { get; set; }
        public string Role { get; set; }
        public string Source { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }

        public override bool Equals(object obj)
        {
            var registration = obj as Registration;
            return Address.Equals(registration.Address)
                && Period.Equals(registration.Period)
                && Type.Equals(registration.Type);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}
