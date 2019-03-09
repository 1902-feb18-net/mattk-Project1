using NLog;
using Project1.BLL;
using Project1.BLL.IDataRepos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using P1B = Project1.BLL;

namespace Project1.DataAccess.DataRepos
{
    public class LocationRepo : IProject1Repo, ILocationRepo
    {
        public static Project1Context Context { get; set; }

        public LocationRepo(Project1Context dbContext)
        {
            Context = dbContext;
        }

        public void SaveChangesAndCheckException()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                Context.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                logger.Error(ex);
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
            }
        }

        public bool CheckLocationExists(int locationId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Context.Location.Any(l => l.LocationId == locationId);
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return false;
            }
        }


        public void AddLocation(Project1.BLL.Location location)
        {
            var newLocation = Mapper.Map(location);
            Context.Location.Add(newLocation);
            SaveChangesAndCheckException();
        }

        public P1B.Location GetLocationById(int id)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.Location.Single(l => l.LocationId == id));
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
            
        }

        public int GetLastLocationAdded()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Context.Location.OrderByDescending(x => x.LocationId).First().LocationId;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return -1;
            }
        }

        public int? GetDefaultLocation(int customerId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Context.Customer.Single(c => c.CustomerId == customerId).DefaultLocation;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return -1;
            }
        }

        public IEnumerable<Project1.BLL.Location> GetAllLocations()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.Location.ToList());
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public IEnumerable<P1B.Order> GetLocationOrderHistory(int locationId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.CupcakeOrder.Where(co => co.LocationId == locationId).ToList());
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }
    }
}
