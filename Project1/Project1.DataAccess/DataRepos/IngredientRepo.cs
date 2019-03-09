using NLog;
using Project1.BLL;
using Project1.BLL.IDataRepos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Project1.DataAccess.DataRepos
{
    public class IngredientRepo : IProject1Repo, IIngredientRepo
    {
        public static Project1Context Context { get; set; }

        public IngredientRepo(Project1Context dbContext)
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

        public IEnumerable<Ingredient> GetIngredients()
        {
            return Mapper.Map(Context.Ingredient);
        }
    }
}
