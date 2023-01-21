using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Tests
{
    public class CustomEntity : Entity<int>
    {
    }

    public class Person : CustomEntity
    {
        public List<int> Ids { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        [NotSelected]
        [NotSaved]
        public double Amount { get; set; }
        //public Address Address { get; set; }
        //public Occupation Occupation { get; set; }
        public List<Address> Addresses { get; set; }

        public Person()
        {
            this.Addresses = new List<Address>();
            this.Ids = new List<int>();
        }
    }

    public class Address : CustomEntity
    {
        //[NotSelected]
        //[NotSaved]
        //public City City { get; set; }
        [NotSelected]
        [NotSaved]
        public string AddressLine { get; set; }
        [NotSelected]
        [NotSaved]
        public int ZipCode { get; set; }
    }

    //public class City : CustomEntity
    //{
    //    public string Name { get; set; }
    //}

    public class Occupation : CustomEntity
    {
        public string Name { get; set; }
    }

    public class PersonProvider : Provider<Person, PersonProvider>
    {
        protected override Entity<int> CreateEntity(Dictionary<string, object> DataItem, Type EntityType)
        {
            return new Person();
        }

        protected override Dictionary<string, object> GetCommonParameters(Person Entity)
        {
            Dictionary<string, object> parameters = base.GetCommonParameters(Entity);
            parameters.Add("Name", Entity.Name);
            return parameters;
        }

        protected override void InitializeEntity(Person Entity, Dictionary<string, object> Values, bool useAlias)
        {
            base.InitializeEntity(Entity, Values, useAlias);
        }
    }

    public class EmptyPersonProvider : Provider<Person, EmptyPersonProvider>
    {
    }

    public class PersonModel : Model<Person, int>
    {
        public string Name { get; set; }
        public List<AddressModel> Addresses { get; set; }

        public PersonModel(Person Item) : base(Item)
        {
            this.Name = Item.Name;
            this.Addresses = new List<AddressModel>();
            this.Addresses.Add(new AddressModel(Item.Addresses[0]));
        }

        public PersonModel() : base(null)
        {
        }
    }

    public class AddressModel : Model<Address, int>
    {
        public AddressModel(Address Item) : base(Item)
        {
            this.Id = Item.Id;
        }
    }

    public static class TestClassFactory
    {
        public static Person CreatePerson()
        {
            Person item = new Person()
            {
                Name = Helpers.CreateKey(10),
                Surname = Helpers.CreateKey(10),
                IsActive = true,
                BirthDate = DateTime.Today.AddYears(-20)
            };

            //item.Addresses.Add(new Address()
            //{
            //    City = new City()
            //    {
            //        Name = "Istanbul"
            //    },
            //    ZipCode = 34345,
            //    AddressLine = "Address line-1"
            //});

            //item.Addresses.Add(new Address()
            //{
            //    City = new City()
            //    {
            //        Name = "Ankara"
            //    },
            //    ZipCode = 16565,
            //    AddressLine = "Address line-2"
            //});

            return item;
        }
    }

    //public class Country : Entity<int>
    //{
    //    //public List<City2> Cities { get; set; }
    //    //public string ProjectName { get; set; }
    //    public CountryType Type { get; set; }
    //}

    //public enum CountryType
    //{

    //}

    //public class City2 : Entity<int>
    //{
    //    //public string Name { get; set; }
    //    public Country InnerType { get; set; }
    //}

    public class Container : Entity<int>
    {
        public Single SingleProperty { get; set; }
        public List<Many1> Many1List { get; set; }
        public List<Many2> Many2List { get; set; }
        public Container()
        {
            Many1List = new List<Many1>();
        }
    }

    public class Single : Entity<int>
    {
    }

    public class Many1 : Entity<int>
    {
        public List<Many2> Many2List { get; set; }
        public Many1()
        {
            Many2List = new List<Many2>();
        }
    }

    public class Many2 : Entity<int>
    {
        public List<Many3> Many3List { get; set; }
        public Many2()
        {
            Many3List = new List<Many3>();
        }
    }

    public class Many3 : Entity<int>
    {
    }

    public partial class Country : CustomEntity
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

    public partial class City : CustomEntity
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
