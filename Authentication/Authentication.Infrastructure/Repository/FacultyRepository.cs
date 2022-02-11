using Authentication.Application.Interface;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Authentication.Infrastructure.Repository
{
    public class FacultyRepository : IFaculty
    {
        private readonly AuthDbService authDb;
        public FacultyRepository(AuthDbService authDb)
        {
            this.authDb = authDb;
        }
        public IEnumerable<Faculty> GetFaculties
        {
            get
            {
                return authDb.Faculties.OrderBy(s => s.FacultyId);
            } 
        }

        public IEnumerable<Department> GetDepartments(Guid facultyId)
        {
            return authDb.Departments.Where(s => s.FacultyId == facultyId).OrderBy(s => s.DepartmentId);
        }
    }
}
