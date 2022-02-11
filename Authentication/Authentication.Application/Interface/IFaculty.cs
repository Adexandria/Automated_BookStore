using Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Application.Interface
{
    public interface IFaculty
    {
        IEnumerable<Faculty> GetFaculties { get; }
        IEnumerable<Department> GetDepartments(Guid facultyId);
    }
}
