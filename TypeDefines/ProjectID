public readonly struct ProjectID
    {
        private ulong Value { get; }

        public ProjectID(ulong value)
        {
            Value = value;
        }

        public static implicit operator ulong(ProjectID id) => id.Value;
        public static explicit operator ProjectID(ulong value) => new(value);

        public override string ToString() => Value.ToString();
    }