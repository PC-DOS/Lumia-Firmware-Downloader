using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumiaFirmwareDownloader
{
    class Program
    {
        public class Options
        {
            [Option('c', "product-code", Required = false, HelpText = "Product Code.")]
            public string ProductCode { get; set; }

            [Option('t', "product-type", Required = true, HelpText = "Product Type.")]
            public string ProductType { get; set; }

            [Option('o', "operator-code", Required = false, HelpText = "Operator Code.")]
            public string OperatorCode { get; set; }

            [Option('r', "revision", Required = false, HelpText = "Revision (useful for getting older package versions).")]
            public string Revision { get; set; }

            [Option('f', "firmware-revision", Required = false, HelpText = "Phone Firmware Revision (useful only for changing what Test package you get for ENOSW/MMOS).")]
            public string FirmwareRevision { get; set; }

            [Option('v', "verbose-output", Default = "true", Required = false, HelpText = "Enable verbose output.")]
            public string VerboseOutput { get; set;}
        }

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       bool IsVerboseOutputOn = true;
                       try
                       {
                           if (string.IsNullOrEmpty(o.VerboseOutput))
                           {
                               IsVerboseOutputOn = true;
                           }
                           else if (o.VerboseOutput.ToUpper() == "TRUE" || o.VerboseOutput.ToUpper() == "ON" || o.VerboseOutput.ToUpper() == "YES" || o.VerboseOutput.ToUpper().StartsWith("ENABLE"))
                           {
                               IsVerboseOutputOn = true;
                           }
                           else if (o.VerboseOutput.ToUpper() == "FALSE" || o.VerboseOutput.ToUpper() == "OFF" || o.VerboseOutput.ToUpper() == "NO" || o.VerboseOutput.ToUpper().StartsWith("DISABLE"))
                           {
                               IsVerboseOutputOn = false;
                           }
                           else
                           {
                               IsVerboseOutputOn = Convert.ToBoolean(o.VerboseOutput);
                           }
                       }
                       catch (Exception ex)
                       {
                           IsVerboseOutputOn = true;
                       }

                       if (IsVerboseOutputOn)
                       {
                           Console.WriteLine();
                           Console.WriteLine("Lumia Firmware Downloader");
                           Console.WriteLine("Adapted for .NET 4.5 by Picsell Dois (PC-DOS)");
                           Console.WriteLine();
                           Console.WriteLine("Based on:");
                           Console.WriteLine("Software Repository (SoRe) Fetch Utility");
                           Console.WriteLine("Copyright 2021 (c) Gustave Monce");
                           Console.WriteLine();
                           Console.WriteLine("This software uses code from the following open source projects released under the MIT license:");
                           Console.WriteLine();
                           Console.WriteLine("WPinternals - Copyright (c) 2018, Rene Lergner - wpinternals.net - @Heathcliff74xda");
                           Console.WriteLine();
                           Console.WriteLine("Please see 3RDPARTY.txt for more information");
                           Console.WriteLine();
                       }

                       string foundType = string.Empty;
                       string ffu = string.Empty;
                       string enosw = string.Empty;
                       List<string> emergency = new List<string>();

                       try
                       {
                           if (IsVerboseOutputOn)
                           {
                               Console.WriteLine();
                               Console.WriteLine("Searching FFU");
                               Console.WriteLine();
                           }
                           ffu = LumiaDownloadModel.SearchFFU(o.ProductType, o.ProductCode, o.OperatorCode, foundType, o.Revision, !IsVerboseOutputOn);
                       }
                       catch (DetailedException ex)
                       {
                           if (IsVerboseOutputOn)
                           {
                               Console.WriteLine("Error while searching FFU");
                               Console.WriteLine(ex.Message);
                               Console.WriteLine(ex.SubMessage);
                           }
                       }
                       catch (Exception ex)
                       {
                           if (IsVerboseOutputOn)
                           {
                               Console.WriteLine("Error while searching FFU");
                               Console.WriteLine(ex.Message);
                               Console.WriteLine(ex.StackTrace);
                           }
                       }

                       try
                       {
                           if (IsVerboseOutputOn)
                           {
                               Console.WriteLine();
                               Console.WriteLine("Searching ENOSW");
                               Console.WriteLine();
                           }
                           enosw = LumiaDownloadModel.SearchENOSW(o.ProductType, o.FirmwareRevision, o.Revision, !IsVerboseOutputOn);
                       }
                       catch (DetailedException ex)
                       {
                           if (IsVerboseOutputOn)
                           {
                               Console.WriteLine("Error while searching ENOSW");
                               Console.WriteLine(ex.Message);
                               Console.WriteLine(ex.SubMessage);
                           }
                       }
                       catch (Exception ex)
                       {
                           if (IsVerboseOutputOn)
                           {
                               Console.WriteLine("Error while searching ENOSW");
                               Console.WriteLine(ex.Message);
                               Console.WriteLine(ex.StackTrace);
                           }
                       }

                       try
                       {
                           if (IsVerboseOutputOn)
                           {
                               Console.WriteLine();
                               Console.WriteLine("Searching Emergency");
                               Console.WriteLine();
                           }
                           emergency = LumiaDownloadModel.SearchEmergencyFiles(o.ProductType, !IsVerboseOutputOn);
                       }
                       catch (DetailedException ex)
                       {
                           if (IsVerboseOutputOn)
                           {
                               Console.WriteLine("Error while searching Emergency");
                               Console.WriteLine(ex.Message);
                               Console.WriteLine(ex.SubMessage);
                           }
                       }
                       catch (Exception ex)
                       {
                           if (IsVerboseOutputOn)
                           {
                               Console.WriteLine("Error while searching Emergency");
                               Console.WriteLine(ex.Message);
                               Console.WriteLine(ex.StackTrace);
                           }
                       }

                       if (IsVerboseOutputOn)
                       {
                           Console.WriteLine();
                           Console.WriteLine("Results");
                           Console.WriteLine();
                       }

                       if (!string.IsNullOrEmpty(foundType))
                       {
                           if (IsVerboseOutputOn)
                           {
                               Console.WriteLine("Product Type: " + foundType);
                               Console.WriteLine();
                           }
                       }

                       if (!string.IsNullOrEmpty(ffu))
                       {
                           if (IsVerboseOutputOn)
                           {
                               Console.WriteLine("FFU: " + ffu);
                               Console.WriteLine();
                           }
                           else
                           {
                               Console.WriteLine(ffu);
                           }
                       }

                       if (!string.IsNullOrEmpty(enosw))
                       {
                           if (IsVerboseOutputOn)
                           {
                               Console.WriteLine("ENOSW: " + enosw);
                               Console.WriteLine();
                           }
                           else
                           {
                               Console.WriteLine(enosw);
                           }
                       }

                       if (emergency == null)
                       {
                           emergency = new List<string>();
                       }
                       if (emergency.Count != 0)
                       {
                           for (int i = 0; i < emergency.Count; i++)
                           {
                               if (IsVerboseOutputOn)
                               {
                                   Console.WriteLine("Emergency[" + i + "]: " + emergency[i]);
                               }
                               else
                               {
                                   Console.WriteLine(emergency[i]);
                               }
                           }
                       }
                   });
        }
    }
}
