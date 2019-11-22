using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Data.Models;
using Data.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
  public class ColorRepo : BaseRepository<Color>, IColor
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
  }
}