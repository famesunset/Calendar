using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
  public interface IColor
  {
    IEnumerable<Color> GetColors();
    Color GetColorById(int colorId);
  }
}