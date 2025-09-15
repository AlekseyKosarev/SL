using Cysharp.Threading.Tasks;
using DS.Models;

// Единый интерфейс для всех сервисов
namespace SL
{
    public interface IService {
        UniTask<Result> InitAsync();
    }
}