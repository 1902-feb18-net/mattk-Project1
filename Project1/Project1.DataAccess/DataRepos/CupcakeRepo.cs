using NLog;
using Project1.BLL;
using Project1.BLL.IDataRepos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Project1.DataAccess.DataRepos
{
    public class CupcakeRepo : IProject1Repo, ICupcakeRepo
    {
        public static Project1Context Context { get; set; }

        public CupcakeRepo(Project1Context dbContext)
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

        public bool CheckCupcakeExists(int cupcakeId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Context.Cupcake.Any(l => l.CupcakeId == cupcakeId);
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        public Cupcake GetCupcake(int cupcakeId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.Cupcake.Single(c => c.CupcakeId == cupcakeId));
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public IEnumerable<Cupcake> GetAllCupcakes()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.Cupcake.ToList());
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        
       
    }
}
