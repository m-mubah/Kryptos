using Kryptos.Web.Client.Models.HammingCode;

namespace Kryptos.Web.Client.Services.HammingCode;

public interface IHammingCodeService
{
    EncodingResult Encode(string data, Parity parity);
    DecodingResult DetectError(string data, Parity parity);
}