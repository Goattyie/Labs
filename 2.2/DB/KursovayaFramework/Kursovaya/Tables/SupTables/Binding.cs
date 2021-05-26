namespace Kursovaya
{
    class Binding : SupTable
    {
        public override string ClassName => "binding";
        public Binding() { }
        public Binding(string value) { Value = value; }
    }
}
