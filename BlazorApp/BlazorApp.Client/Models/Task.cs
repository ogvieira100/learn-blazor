using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Models
{

    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed
    }
    public class Tasks
    {
        public TaskStatus TaskStatus { get; set; }

        public required string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

      


        public Tasks()
        {
            TaskStatus = TaskStatus.NotStarted;
            DateCreated = DateTime.Now;
        }

        public Tasks(string name)
            : base()
        {
            Name = name;
        }
    }
}
