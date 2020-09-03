namespace HotelReservationsApp.Misc
{
    internal class Integer
    {
        public int Value { get; set; }
        public bool HasValue { get; private set; }

        public Integer(int value)
        {
            Value = value;
            HasValue = true;
        }

        public Integer()
        {
            HasValue = false;
        }

        public static implicit operator int(Integer d) => d.Value;

        public static implicit operator Integer(int b) => new Integer(b);
    }
}