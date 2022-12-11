using System;
using System.Collections.Generic;

namespace BackendEmployeeAPI.Models;

public partial class Department
{
    public int IdDepartment { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
