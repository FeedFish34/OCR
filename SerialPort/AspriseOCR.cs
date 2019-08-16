using asprise_ocr_api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace OCRSerialPort
{
    public class AspriseOCR1
    {

        public static string ORCResult = string.Empty;
        private static AspriseOCR ocr;
        private static String currentLang = "eng";
        private static String currentEngineStartProps = "";

        private static Thread threadOcr;

        private static string requestLang;
        private static string requestPropsStart;
        private static string requestImgFile;
        private static string requestLayout;
        private static bool requestDataCapture;
        private static bool requestAutoRotate;
        private static bool requestWordLevel;
        private static string requestOutputFormat;
        private static bool requestPdfHighlight;
        private static string requestRecognizeType;
        private static string requestPropsRecognize;
        public static void GetOCRpart(string img_path)
        {
            //if (threadOcr != null && threadOcr.IsAlive)
            //{
            //    return;
            //}

            requestLang = "eng";
            if (requestLang == null || requestLang.Length == 0)
            {
                return;
            }
            requestPropsStart = "";
            requestImgFile = img_path;
            requestLayout = "auto";
            requestDataCapture = true;
            requestAutoRotate = false;
            requestWordLevel = false;
            requestOutputFormat = AspriseOCR.OUTPUT_FORMAT_PLAINTEXT;
            requestPdfHighlight = true;
            requestRecognizeType = AspriseOCR.RECOGNIZE_TYPE_ALL;
            requestPropsRecognize = "";


            AspriseOCR.SetUp();
            ocr = new AspriseOCR();
            ocr.StartEngine(currentLang, AspriseOCR.SPEED_FASTEST, "");

            doOcr();
        }

       static void doOcr()
        {
            if (requestImgFile.Length == 0)
            {
                return;
            }

            if (!File.Exists(requestImgFile))
            {
                return;
            }

            if (!requestLang.Equals(currentLang) || !requestPropsStart.Equals(currentEngineStartProps))
            {
                ocr.StopEngine();
                currentLang = null;

                ocr = new AspriseOCR();
                ocr.StartEngine(requestLang, AspriseOCR.SPEED_FASTEST, requestPropsStart);
                currentLang = requestLang;
                currentEngineStartProps = requestPropsStart;
            }

            if (ocr == null || !ocr.IsEngineRunning)
            {
                return;
            }

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add(AspriseOCR.PROP_OUTPUT_SEPARATE_WORDS, requestWordLevel ? "true" : "false");
            dict.Add(AspriseOCR.PROP_PAGE_TYPE, requestLayout);
            dict.Add(AspriseOCR.PROP_TABLE_SKIP_DETECTION, requestDataCapture ? "false" : "true");
            dict.Add(AspriseOCR.PROP_IMG_PREPROCESS_TYPE, requestAutoRotate ? AspriseOCR.PROP_IMG_PREPROCESS_TYPE_DEFAULT_WITH_ORIENTATION_DETECTION : AspriseOCR.PROP_IMG_PREPROCESS_TYPE_DEFAULT);

            string pdfOutputFile = null;
            if (requestOutputFormat.Equals(AspriseOCR.OUTPUT_FORMAT_PDF))
            {
                pdfOutputFile = Path.Combine(Directory.GetCurrentDirectory(), DateTime.Now.ToString("O").Replace(':', '-') + ".pdf");
                dict.Add(AspriseOCR.PROP_PDF_OUTPUT_FILE, pdfOutputFile);
                dict.Add(AspriseOCR.PROP_PDF_OUTPUT_TEXT_VISIBLE, "true");
                dict.Add(AspriseOCR.PROP_PDF_OUTPUT_IMAGE_FORCE_BW, "true");
            }
            string rtfOutputFile = null;
            if (requestOutputFormat.Equals(AspriseOCR.OUTPUT_FORMAT_RTF))
            {
                rtfOutputFile = Path.Combine(Directory.GetCurrentDirectory(), DateTime.Now.ToString("O").Replace(':', '-') + ".rtf");
                dict.Add(AspriseOCR.PROP_RTF_OUTPUT_FILE, rtfOutputFile);
            }

            string allRecogProps = AspriseOCR.dictToString(dict) +
                (requestPropsRecognize == null || requestPropsRecognize.Trim().Length == 0 ?
                    "" : AspriseOCR.CONFIG_PROP_SEPARATOR + requestPropsRecognize);
            String status = "Recognizing " + requestRecognizeType + " to output as " + requestOutputFormat + " on image: " + requestImgFile + " ...\n" +
                "OCR engine start props: " + requestPropsStart + "\n" +
                "OCR recognition props:  " + allRecogProps + "\n" +
                "Please stand by ...";


            DateTime timeStart = DateTime.Now;
            // Performs the actual recognition
            ORCResult = ocr.Recognize(requestImgFile, -1, -1, -1, -1, -1, requestRecognizeType, requestOutputFormat, AspriseOCR.dictToString(dict) + AspriseOCR.CONFIG_PROP_SEPARATOR + requestPropsRecognize);
            DateTime timeEnd = DateTime.Now;

            // open pdf file
            if (requestOutputFormat.Equals(AspriseOCR.OUTPUT_FORMAT_PDF))
            {
                if (ORCResult != null && ORCResult.Trim().Length > 0)
                {
                }
                else
                {
                    try
                    {
                        System.Diagnostics.Process.Start(@pdfOutputFile);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            else if (requestOutputFormat.Equals(AspriseOCR.OUTPUT_FORMAT_RTF))
            {
                if (ORCResult != null && ORCResult.Trim().Length > 0)
                {
                }
                else
                {
                    try
                    {
                        System.Diagnostics.Process.Start(@rtfOutputFile);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            else if (requestOutputFormat.Equals(AspriseOCR.OUTPUT_FORMAT_XML))
            {
                try
                {
                    string xmlOutputFile = Path.Combine(Directory.GetCurrentDirectory(), DateTime.Now.ToString("O").Replace(':', '-') + ".xml");
                    File.WriteAllText(xmlOutputFile, ORCResult, Encoding.UTF8);
                    AspriseOCR.saveAocrXslTo(Directory.GetCurrentDirectory(), false);
                    if (File.Exists(xmlOutputFile))
                    {
                        System.Diagnostics.Process.Start(@xmlOutputFile);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
            }

            // user preference
            //saveSettings();
        }
    }
}
