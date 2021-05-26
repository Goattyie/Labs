namespace Kursovaya
{
    class City : SupTable
    {
        public override string ClassName => "city";
        public City() { }
        public City(string value) { Value = value; }
    }
}
