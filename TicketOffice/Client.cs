namespace TicketOffice
{
    class Client
    {
        public string Name { get; }
        public string MiddleName { get; }
        public string Surname { get; }

        public Client(string name, string middleName, string surname)
        {
            Name = name;
            MiddleName = middleName;
            Surname = surname;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
           
            Client other = (Client)obj;
            return string.Equals(Name, other.Name)
                && string.Equals(MiddleName, other.MiddleName)
                && string.Equals(Surname, other.Surname);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + (Name?.GetHashCode() ?? 0);
            hash = hash * 23 + (MiddleName?.GetHashCode() ?? 0);
            hash = hash * 23 + (Surname?.GetHashCode() ?? 0);
            return hash;
        }
    }
}
