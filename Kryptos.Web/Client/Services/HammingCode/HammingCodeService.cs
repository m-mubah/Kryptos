using Kryptos.Web.Client.Models.HammingCode;

namespace Kryptos.Web.Client.Services.HammingCode;

public class HammingCodeService : IHammingCodeService
{
    /// <summary>
        /// Uses the formula 2^r >= m + r + 1 to calculate the number of redundant bits.
        /// </summary>
        /// <param name="length">Length of the data to encode.</param>
        /// <returns>Number of redundant bits. Returns -1 if none, or an error has occured.</returns>
        private int CalculateRedundantBits(int length)
        {
            int result = -1;
            
            for (int i = 0; i < length; i++)
            {
                if (Math.Pow(2, i) >= length + i + 1)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }
        
        /// <summary>
        /// Generate positions for parity bits to be placed on.
        /// </summary>
        /// <param name="length">Length of the sequence.</param>
        /// <returns>An enumerable containing positions for parity bits.</returns>
        private IEnumerable<int> GenerateParityPositions(int length)
        {
            int index = 0;
            while (true)
            {
                int current = (int) Math.Pow(2, index);
                index += 1;
                if (current > length)
                {
                    break;
                }

                yield return current;
            }
        }

        /// <summary>
        /// Encodes a given binary string (string with only 1s and 0s) using hamming code.
        /// </summary>
        /// <param name="data">Data to be encoded.</param>
        /// <param name="parity">This flag determines whether to use even parity or odd parity for encoding.</param>
        /// <returns>Encoded binary string.</returns>
        public EncodingResult Encode(string data, Parity parity)
        {
            List<int> sequence = data.Select(i => int.Parse(i.ToString())).ToList();

            int sequenceLength = sequence.Count;
            int numOfRedundantBits = CalculateRedundantBits(sequenceLength);
            
            //temporary variables used for iteration
            int j = 0, k = sequenceLength - 1;

            List<int> result = new List<int>();
            
            //Build a new list and pad the positions of parity bits with zeroes.
            //This initialises the encoded list to the correct length.
            foreach (int i in Enumerable.Range(1, sequenceLength + numOfRedundantBits))
            {
                if (i == (int) Math.Pow(2, j))
                {
                    result.Add(0);
                    j += 1;
                }
                else
                {
                    result.Add(sequence[k]);
                    k -= 1;
                }
            }
            
            int length = result.Count;
            
            //Iterate over the parity positions and determine the parity bit value.
            foreach (var p in GenerateParityPositions(length))
            {
                int count = 0;
                for (int i = 0; i < result.Count; i++)
                {
                    if (i + 1 != p && (i + 1 & p) == p)
                    {
                        count += result[i];
                    }
                }
                
                //If the isEven flag is passed then calculate for even parity.
                if (parity == Parity.Even)
                {
                    result[p - 1] = count % 2;
                }
                else //calculate for odd parity.
                {
                    result[p - 1] = count % 2 != 1 ? 1 : 0;
                }
            }

            result.Reverse(); //The resulting list is reversed, so we reverse again to get the correct list.
            string resultString = string.Join("", result);

            return new EncodingResult(resultString, parity);
        }

        private List<int> Decode(List<int> data)
        {
            int dataIndex = -1;
            string result = "";
        
            int length = data.Count;
        
            foreach (var p in GenerateParityPositions(length))
            {
                var parityIndex = p - 1;
        
                while(dataIndex < length)
                {
                    dataIndex += 1;
                    if (dataIndex == parityIndex)
                    {
                        break;
                    }
        
                    result += data[dataIndex];
                }
            }
        
            result += string.Join("", data.Where((i, idx) => idx > dataIndex));
            return result.Select(i => int.Parse(i.ToString())).ToList();
        }
        
        public DecodingResult DetectError(string data, Parity dataParity)
        {
            List<int> sequence = data.Select(i => int.Parse(i.ToString())).ToList();
        
            int length = sequence.Count;
            int binary = 0;
            int index = 0;
        
            //Iterate over the parity positions and determine the parity bit value.
            foreach (var p in GenerateParityPositions(length))
            {
                int count = 0;
                int parity;
                
                for (int i = 0; i < sequence.Count; i++)
                {
                    if ((i + 1 & p) == p)
                    {
                        count += sequence[i];
                    }
                }
                
                //If the isEven flag is passed then calculate for even parity.
                if (dataParity == Parity.Even)
                {
                    parity = count % 2;
                }
                else //calculate for odd parity.
                {
                    parity = count % 2 != 1 ? 1 : 0;
                }
        
                binary += (int) (parity * Math.Pow(10, index));
                index ++;
            }
        
            //char[] array = binary.ToString().ToCharArray();
            //Array.Reverse(array);
            //string binaryString = new String(array);
        
            int position = Convert.ToInt32(binary.ToString(), 2);
            
            if (position > 0)
            {
                sequence[position - 1] ^= 1;
            }
        
            return new DecodingResult 
            {
                OriginalSequence = data.Select(i => int.Parse(i.ToString())).ToList(),
                FixedSequence = sequence,
                ErrorPosition = position,
                DecodedSequence = Decode(sequence).Select(i => int.Parse(i.ToString())).ToList()
            };
        }
}