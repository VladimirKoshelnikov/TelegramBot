using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FFMpegCore;
using TelegramBot.Extentions;

namespace TelegramBot.Utilities
{
    public class AudioConverter
    {
        public static void TryConvert(string inputFile, string outputFile){
            GlobalFFOptions.Configure(options => options.BinaryFolder = Path.Combine(DirectoryExtentions.GetSolutionRoot(), "FFmpeg-win64", "bin"));

            FFMpegArguments
                .FromFileInput(inputFile)
                .OutputToFile(outputFile, true, options => options.WithFastStart())
                .ProcessSynchronously();
        }
    }
}