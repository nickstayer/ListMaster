namespace ListMaster
{
    public class Document
    {
        public string Series { get; set; }
        public string Number { get; set; }
        public string SeriesNumber { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string DepartmentCodeAndName { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public Department Department { get; set; }
        public string Country { get; set; }
        public string Comment { get; set; }
    }
}
