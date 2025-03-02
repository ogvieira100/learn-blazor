using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Models
{

    public enum TaskStatus
    {
        NotStarted = 1,
        InProgress = 2,
        Completed = 3
    }

    public class Tasks
    {
        [Required(ErrorMessage = " Atenção campo task requirido. ")]
        public TaskStatus? TaskStatus { get; set; }

        [Required(ErrorMessage = " Atenção campo task requirido. ")]
        public string? TaskStatusDescription
        {
            get
            ;
            set
           ;
        }
        [Required(ErrorMessage = " Atenção campo nome requirido. ")]
        public string? Name { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public Func<TaskStatus, Task>? UpdateTast;

        public Guid Id { get; set; }

        public Tasks()
        {
            TaskStatus = BlazorAppTreino.Domain.Models.TaskStatus.NotStarted;
            DateCreated = DateTime.Now;
        }

        public Tasks(string name)
            : base()
        {
            Name = name;
            Id = Guid.NewGuid();
        }
    }
}
