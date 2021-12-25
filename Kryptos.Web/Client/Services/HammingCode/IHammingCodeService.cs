using Kryptos.Web.Client.Models.HammingCode;

namespace Kryptos.Web.Client.Services.HammingCode;

public interface IHammingCodeService
{
    /// <summary>
    /// Encodes the data using hamming encode.
    /// </summary>
    /// <param name="data">a binary number that's 4 bits long.</param>
    /// <param name="parity">parity to encode with. Must be odd or even</param>
    /// <returns>Encoded binary</returns>
    EncodingResult Encode(string data, Parity parity);
    
    /// <summary>
    /// Detects an error in a hamming encoded binary number.
    /// </summary>
    /// <param name="data">a binary number that's 7 bits long.</param>
    /// <param name="parity">parity to decode with. Must be odd or even.</param>
    /// <returns>A class with error position and fixed error</returns>
    DecodingResult DetectError(string data, Parity parity);
}