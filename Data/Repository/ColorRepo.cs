using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

    public Color GetColorById(int colorId)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            string sql = "Select Hex from Color where Id = " + colorId;
            var c = connection.Query<Color>(sql).Select(x => (new Color(colorId, x.Hex))).FirstOrDefault();
                return c;
        }
    }
  }
}