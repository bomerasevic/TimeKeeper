using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class ProjectsRepository : Repository<Project>
    {
        public ProjectsRepository(TimeKeeperContext context) : base(context) { }

        private void Build(Project project)
        {
            project.Customer = _context.Customers.Find(project.Customer.Id);
            project.Team = _context.Teams.Find(project.Team.Id);
            project.Status = _context.ProjectStatuses.Find(project.Status.Id);
            project.Pricing = _context.ProjectPricings.Find(project.Pricing.Id);
        }
        public override void Insert(Project project)
        {
            Build(project);
            base.Insert(project);
        }
        public override void Update(Project project, int id)
        {
            Project old = Get(id);
            Build(project);
            _context.Entry(old).CurrentValues.SetValues(project);
            old.Customer = project.Customer;
            old.Team = project.Team;
            old.Status = project.Status;
            old.Pricing = project.Pricing;
        }
        public override void Delete(int id)
        {
            Project old = Get(id);

            if (old.Tasks.Count != 0)
                throw new Exception("Object cannot be deleted because it has children entities!");

            Delete(old);
        }
    }
}
