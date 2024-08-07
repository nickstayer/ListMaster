using System.Collections.Generic;

namespace ListMaster
{
    class RegistrationComparer : IEqualityComparer<Registration>
    {
        public bool Equals(Registration x, Registration y)
        {
            return x.Address == y.Address && x.DateIn == y.DateIn && x.DateOut == y.DateOut && x.Role == y.Role && x.Period == y.Period;
        }

        public int GetHashCode(Registration obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }
    }
}
