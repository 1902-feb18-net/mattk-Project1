using Microsoft.Extensions.Logging;
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
        public readonly ILogger<IngredientRepo> _logger;

        public static Project1Context Context { get; set; }

        public IngredientRepo(Project1Context dbContext)
        {
            Context = dbContext;
        }

        public void SaveChangesAndCheckException()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.ToString());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        public IEnumerable<Ingredient> GetIngredients()
        {
            return Mapper.Map(Context.Ingredient);
        }
    }
}
