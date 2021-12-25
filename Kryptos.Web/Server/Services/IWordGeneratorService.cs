using System.Collections;

namespace Kryptos.Web.Server.Services;

public interface IWordGeneratorService : IEnumerable<string>
{
    new IEnumerator<string> GetEnumerator();
}