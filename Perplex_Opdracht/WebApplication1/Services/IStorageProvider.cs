using System.Threading.Tasks;
using WebApplication1.Models;

public interface IStorageProvider
{
    Task<object> SaveIdeaAsync(SuggestionModel submission);
}