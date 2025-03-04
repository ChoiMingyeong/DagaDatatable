namespace DagaDatatable.TypeDefines
{
    public readonly struct AccountID
    {
        public uint Value { get; }

        public AccountID(uint value)
        {
            Value = value;
        }

        public static implicit operator uint(AccountID id) => id.Value;
        public static explicit operator AccountID(uint value) => new(value);

        public override string ToString() => Value.ToString();
    }
}
