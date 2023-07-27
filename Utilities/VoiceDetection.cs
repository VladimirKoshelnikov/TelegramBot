using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vosk;
using TelegramBot.Extentions;
using Newtonsoft.Json.Linq;
using System.Text;


namespace TelegramBot.Utilities
{
    public class VoiceDetection
    {

        public static string GetText(Model model, string pathToAudio, float inputBitrate){

            VoskRecognizer rec = new VoskRecognizer(model, inputBitrate);
            rec.SetMaxAlternatives(0);
            rec.SetWords(true);

            StringBuilder textBuffer = new();      

            using(Stream source = File.OpenRead(pathToAudio)) {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0) {
                    if (rec.AcceptWaveform(buffer, bytesRead)) {
                        var sentenceJson = rec.Result();
                        JObject sentenceObj = JObject.Parse(sentenceJson);
                        string sentence = (string)sentenceObj["text"];
                        textBuffer.Append(StringExtentions.UppercaseFirst(sentence) + ".");
                    
                    }
                }
            }

            var finalSentence = rec.FinalResult();
            JObject finalSentnceObj = JObject.Parse(finalSentence);
            textBuffer.Append((string)finalSentnceObj["text"]);
            return textBuffer.ToString();
        }
        public static string GetTextMessageFromVoice(string pathToAudio, float inputBitrate, string languageCode){

            Vosk.Vosk.SetLogLevel(-1);
            Model model = new Model(Path.Combine(DirectoryExtentions.GetSolutionRoot(), "Speech-models", $"vosk-model-small-{languageCode.ToLower()}"));
            return GetText(model,pathToAudio,inputBitrate);

        }
    }
}