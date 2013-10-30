
using PBDesk.EFRepository;
namespace WW.EnvConfigs.DataModels
{
    public class WWApp : Entity
    {
        //public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description
        public int WWFrameworkId { get; set; } // WWFrameworkId

        // Foreign keys
        public virtual WWFramework WWFramework { get; set; }

        //// Reverse navigation
        ////public virtual ICollection<EnvKey> EnvKeys { get; set; }

        //public WWApp()
        //{
        //    this.EnvKeys = new List<EnvKey>();
        //}
    }
}
