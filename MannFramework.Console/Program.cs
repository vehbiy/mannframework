
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ProviderRepository.AddAllProviders();
            int subProjectCount;
            //JoinEntityManager man = new JoinEntityManager();
            EntityManager man = EntityManager.Instance;
            List<Project> items = null;
            Stopwatch watch = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("Name", "Garcia");

            System.Console.WriteLine("\nInitialization");
            watch = Stopwatch.StartNew();
            items = man.GetItemsFromDBJoin<Project>(parameters);
            subProjectCount = items.Sum(x => x.SubProjects.Count);
            watch.Stop();
            //System.Console.WriteLine(items.Count + " items, " + subProjectCount + " subitems, " + watch.ElapsedMilliseconds);

            System.Console.Write("Dapper, ");
            watch = Stopwatch.StartNew();
            var dapperItems = GetWitDapper();
            subProjectCount = dapperItems.Sum(x => x.SubProjects.Count);
            watch.Stop();
            System.Console.WriteLine(dapperItems.Count + " items, " + subProjectCount + " subitems, " + watch.ElapsedMilliseconds);

            System.Console.Write("Join with provider, ");
            watch = Stopwatch.StartNew();
            items = man.GetItemsFromDBJoin<Project>(parameters);
            subProjectCount = items.Sum(x => x.SubProjects.Count);
            watch.Stop();
            System.Console.WriteLine(items.Count + " items, " + subProjectCount + " subitems, " + watch.ElapsedMilliseconds);

            ProviderRepository.Providers.Clear();
            System.Console.Write("Join without provider, ");
            watch = Stopwatch.StartNew();
            items = man.GetItemsFromDBJoin<Project>(parameters);
            subProjectCount = items.Sum(x => x.SubProjects.Count);
            watch.Stop();
            System.Console.WriteLine(items.Count + " items, " + subProjectCount + " subitems, " + watch.ElapsedMilliseconds);

            System.Console.Write("Dapper, ");
            watch = Stopwatch.StartNew();
            dapperItems = GetWitDapper();
            subProjectCount = dapperItems.Sum(x => x.SubProjects.Count);
            watch.Stop();
            System.Console.WriteLine(dapperItems.Count + " items, " + subProjectCount + " subitems, " + watch.ElapsedMilliseconds);

            ProviderRepository.Providers.Clear();
            System.Console.Write("Join without provider, ");
            watch = Stopwatch.StartNew();
            items = man.GetItemsFromDBJoin<Project>(parameters);
            subProjectCount = items.Sum(x => x.SubProjects.Count);
            watch.Stop();
            System.Console.WriteLine(items.Count + " items, " + subProjectCount + " subitems, " + watch.ElapsedMilliseconds);

            ProviderRepository.AddAllProviders();
            System.Console.Write("Join with provider, ");
            watch = Stopwatch.StartNew();
            items = man.GetItemsFromDBJoin<Project>(parameters);
            subProjectCount = items.Sum(x => x.SubProjects.Count);
            watch.Stop();
            System.Console.WriteLine(items.Count + " items, " + subProjectCount + " subitems, " + watch.ElapsedMilliseconds);
            //watch = Stopwatch.StartNew();
            //var items2 = man.GetItemsFromDBJoin<SubProject>();
            //watch.Stop();
            //System.Console.WriteLine(items2.Count + " items, " + watch.ElapsedMilliseconds);

            System.Console.Write("Lazy load, ");
            watch = Stopwatch.StartNew();
            var items3 = EntityManager.Instance.GetItems<Project>(parameters);
            subProjectCount = items3.Sum(x => x.SubProjects.Count);
            watch.Stop();
            System.Console.WriteLine(items3.Count + " items, " + subProjectCount + " subitems, " + watch.ElapsedMilliseconds);
        }

        public static Func<DbConnection> ConnectionFactory = () => new SqlConnection(ConfigurationManager.ConnectionStrings["MannFramework.Sql"].ConnectionString);
        public static List<Project> GetWitDapper()
        {
            string sql = "SELECT * FROM Project AS A INNER JOIN SubProject AS B ON A.Id = B.ProjectId where A.DeleteTime is null and B.DeleteTime is null;";

            using (var connection = ConnectionFactory())
            {
                connection.Open();

                var invoiceDictionary = new Dictionary<int, Project>();

                var invoices = connection.Query<Project, SubProject, Project>(
                        sql,
                        (invoice, invoiceItem) =>
                        {
                            Project invoiceEntry;

                            if (!invoiceDictionary.TryGetValue(invoice.Id, out invoiceEntry))
                            {
                                invoiceEntry = invoice;
                                invoiceEntry.SubProjects = new List<SubProject>();
                                invoiceDictionary.Add(invoiceEntry.Id, invoiceEntry);
                            }

                            if (!invoiceEntry.SubProjects.Contains(invoiceItem))
                            {
                                invoiceEntry.SubProjects.Add(invoiceItem);
                            }

                            return invoiceEntry;
                        },
                        splitOn: "Id")
                    .Distinct()
                    .ToList();

                return invoices;
            }
        }
    }

    public class ProjectProvider : Provider<Project, ProjectProvider>
    {
        protected override Entity<int> CreateEntity(Dictionary<string, object> DataItem, Type EntityType)
        {
            //System.Console.WriteLine("ProjectProvider CreateEntity");
            return new Project();
        }

        protected override void InitializeEntity(Project t, Dictionary<string, object> dr, bool useAlias)
        {
            //System.Console.WriteLine("ProjectProvider InitializeEntity");
            t.Name = dr.GetValue<string>("Project.Name");
            t.Id = dr.GetValue<int>("Project.Id");
            t.InsertTime = dr.GetValue<DateTime>("Project.InsertTime");
        }
    }

    [Serializable]
    public partial class SubProjectProvider : Provider<SubProject, SubProjectProvider>
    {
        protected override Entity<int> CreateEntity(Dictionary<string, object> dataItem, Type entityType)
        {
            return new SubProject();
        }

        protected override void InitializeEntity(SubProject entity, Dictionary<string, object> values, bool useAlias)
        {
            entity.Id = values.GetValue<int>("SubProject.Id");
            entity.InsertTime = values.GetValue<DateTime>("SubProject.InsertTime");
            entity.ProjectType = values.GetValue<ProjectType>("SubProject.ProjectType");
            entity.Name = values.GetValue<string>("SubProject.Name");
            entity.Folder = values.GetValue<string>("SubProject.Folder");
            //entity.Project = EntityManager.Instance.GetItem<Project>(values.GetValue<int>("SubProject.ProjectId"));
        }

        protected override Dictionary<string, object> GetCommonParameters(SubProject entity)
        {
            Dictionary<string, object> parameters = base.GetCommonParameters(entity);
            // TODO;
            // TODO;
            // TODO;
            // TODO;
            return parameters;
        }
    }

    public partial class Project : MannFramework.Application.ApplicationEntity
    {
        [WhitespaceValidation]
        [Required]
        [StringLength(400, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        //public List<SubProject> SubProjects { get { return Get(ref _subProjects); } set { Set(ref _subProjects, value); } }
        public List<SubProject> SubProjects { get; set; }

        #region Lazy load
        private List<SubProject> _subProjects;
        #endregion

        public Project()
        {
            this.SubProjects = new List<SubProject>();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public partial class SubProject : Application.ApplicationEntity
    {
        public ProjectType ProjectType { get; set; }
        public string Name { get; set; }
        public string Folder { get; set; }
        //public Project Project { get { return Get(_projectId, ref _project); } set { Set(ref _project, ref _projectId, value); } }
        //[NotSelected]
        //[NotSaved]
        //public int? ProjectId { get { return _projectId; } set { _projectId = value; } }
        //public override bool CachingEnabled => true;

        #region Lazy load
        //private Project _project;
        //private int? _projectId;
        #endregion

        public SubProject()
        {
        }

    }

    [Flags]
    public enum ProjectType
    {
        //BL = 1,
        //MVC = 2,
        //WebApi = 4,
        //SocketIO = 8,
        //UnitTest = 16,
        //Desktop = 32
        BL = 1,
        MVC = 1 << 2,
        WebApi = 1 << 3,
        SocketIO = 1 << 4,
        UnitTest = 1 << 5,
        Desktop = 1 << 6,
        MSSQL = 1 << 7,
        MySql = 1 << 8,
        Angular2 = 1 << 9
    }
}
