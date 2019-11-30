using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Data.Models;
using Data.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
  public class ColorRepo : BaseRepository, IColor
  {
    public IEnumerable<Color> GetColors()
    {
      using (SqlConnection connection = new SqlConnection(ConnectionString))
      {
        IEnumerable<Color> colors = connection.Query<Color>("uspGetCalendarColors",
          commandType: CommandType.StoredProcedure).ToList();
        return colors;
      }
    }

    public Color GetColorById(int colorId)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            string sql = "Select * from Color where Id = @colorId";
            var color = connection.Query<Color>(sql, new { colorId }).FirstOrDefault();
            return color;
        }
    }
  }
}