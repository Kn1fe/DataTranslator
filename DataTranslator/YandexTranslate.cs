using System;
using System.IO;
using System.Net;
using System.Xml;

namespace DataTranslator
{
    class YandexTranslate
    {
        public string key = "trnsl.1.1.20151007T122310Z.25c0ddf918024872.5126c7545c55ed1f1bc397bbfd25ae5bf50a5412";

            /// <summary>
            /// Определение языка
            /// </summary>
            /// <param name="text"></param>
            /// <returns>Язык строки</returns>
            public string Detect(string text)
            {
                WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr/detect?key=" + key + ";text=" + text);
                WebResponse response = request.GetResponse();

                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    var fetchedXml = sr.ReadToEnd();

                    XmlDocument d = new XmlDocument();
                    d.LoadXml(fetchedXml);

                    XmlNodeList langNodes = d.GetElementsByTagName("DetectedLang");
                    XmlNode node = langNodes.Item(0);

                    return node.Attributes[1].Value;
                }
            }


            /// <summary>
            /// Получить перечень доступных направлений для перевода
            /// </summary>
            public void GetLangs()
            {
                WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr/getLangs?key=" + key);
                WebResponse response = request.GetResponse();

                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    var fetchedXml = sr.ReadToEnd();

                    XmlDocument d = new XmlDocument();
                    d.LoadXml(fetchedXml);

                    XmlNodeList trDirectionNodes = d.GetElementsByTagName("string");

                    foreach (XmlNode trDirectionNode in trDirectionNodes)
                        Console.WriteLine("Dir: {0}", trDirectionNode.InnerText);
                }
            }
        /// <summary>
        /// Перевести текст
        /// </summary>
        /// <param name="lang">Язык на какой перевести ru</param>
        /// <param name="text">Текст для перевода</param>
        /// <returns>Возвращает NodeList переведенного текста</returns>
        public string Translate(string lang, string text)
        {
            WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr/translate?key=" + key + ";lang=" + lang + "&text=" + text + "&format=plain");
            WebResponse response = request.GetResponse();

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                var fetchedXml = sr.ReadToEnd();

                XmlDocument d = new XmlDocument();
                d.LoadXml(fetchedXml);

                return d.GetElementsByTagName("text")[0].InnerText;
            }
        }
    }
}
