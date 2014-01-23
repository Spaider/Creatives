
namespace Creatives.Models
{
    public interface ICreativesRepository
    {
        User GetUserByName(string name);
    }
}
