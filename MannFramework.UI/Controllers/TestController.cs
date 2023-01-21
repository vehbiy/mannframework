using MannFramework.Application;
using MannFramework.Application.Manager;
using MannFramework.CodeGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MannFramework.Macondo.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            PushManager.SendSocketIONotification("test", "10", "100"); ;
            return View();
        }

        public ActionResult List()
        {
            Project project = EntityManager.Instance.GetItem<Project>(1);
            ProjectSetting setting = project.GetProjectSetting();
            GarciaConfigurationManager.SetConfigurationValues(typeof(GarciaConfiguration), setting);
            //GarciaConfigurationManager.SetConfigurationValues(typeof(GarciaApplicationConfiguration), setting);
            GarciaConfigurationManager.SetConfigurationValues(typeof(GarciaGeneratorConfiguration), setting);

            //JoinEntityManager mana = new JoinEntityManager();
            //List<City> items = mana.GetItemsFromDBJoin<City>();
            return View();
        }
    }

    public partial class Country : ApplicationEntity
    {
        //[Required]
        //public List<City> Cities { get { return Get(ref _cities); } set { Set(ref _cities, value); } }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string CountryName { get; set; }

        #region Lazy load
        //private List<City> _cities;
        #endregion

        public Country()
        {
            //this.Cities = new List<City>();
        }
    }

    public partial class City : ApplicationEntity
    {
        public Country Country { get { return Get(_countryId, ref _country); } set { Set(ref _country, ref _countryId, value); } }
        [NotSelected]
        [NotSaved]
        public int? CountryId { get { return _countryId; } set { _countryId = value; } }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string CityName { get; set; }

        #region Lazy load
        private Country _country;
        private int? _countryId;
        #endregion

        public City()
        {
        }

    }
}