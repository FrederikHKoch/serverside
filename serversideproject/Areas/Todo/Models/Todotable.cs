using System;
using System.Collections.Generic;

namespace serversideproject.Areas.Todo.Models
{
    public partial class Todotable
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int UserId { get; set; }
    }
}
