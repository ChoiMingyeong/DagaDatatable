using DagaDatatable.TypeDefines;

namespace DagaDatatable.Models
{

    public class Account
    {
        public AccountID Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public Dictionary<ProjectID, string> Projects { get; set; } = [];
    }

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

    public class Project
    {
        public ProjectID Id { get; set; }
    }
}
