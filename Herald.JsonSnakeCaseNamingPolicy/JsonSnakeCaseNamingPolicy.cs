using System;
using System.Text.Json;

namespace Herald.JsonSnakeCaseNamingPolicy
{
    public sealed class JsonSnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            int upperCaseCount = 0;

            for (var i = 0; i < name.Length; i++)
            {
                if (name[i] >= 'A' && name[i] <= 'Z' && name[i] != name[0])
                {
                    upperCaseCount++;
                }
            }

            int bufferSize = name.Length + upperCaseCount;

            Span<char> buffer = new char[bufferSize];

            int bufferPosition = 0;

            int namePosition = 0;

            while (bufferPosition < buffer.Length)
            {
                if (name[namePosition] >= 'A' && name[namePosition] <= 'Z')
                {
                    if (namePosition == 0)
                    {
                        buffer[bufferPosition] = Char.ToLower(name[namePosition]);
                        bufferPosition++;
                        namePosition++;
                        continue;
                    }

                    buffer[bufferPosition] = '_';

                    buffer[bufferPosition + 1] = Char.ToLower(name[namePosition]);
                    bufferPosition += 2;
                    namePosition++;
                    continue;
                }

                buffer[bufferPosition] = name[namePosition];
                bufferPosition++;
                namePosition++;
            }

            return buffer.ToString();
        }
    }
}
