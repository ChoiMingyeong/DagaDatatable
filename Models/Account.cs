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
}
