namespace BithdayAPP
{
    internal class Birthday
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public override string ToString()
        {
            return $"{Name} - {Date.ToShortDateString()} - {Notes}";
        }
    }
}
