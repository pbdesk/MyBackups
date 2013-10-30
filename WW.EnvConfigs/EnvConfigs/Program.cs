using System;
using WW.EnvConfigs.Utils;


namespace EnvConfigs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Normalizer.RunNormalizer();
            //return;

            EIParameters parameters = null;
            try
            {
               parameters =  ReadArgs(args);
               Console.WriteLine("Process:" + parameters.process);
               Console.WriteLine("Directory:" + parameters.dir);
               Console.WriteLine("File:" + parameters.file);
               Console.WriteLine("Locale:" + parameters.locale);
               Console.WriteLine("Build:" + parameters.build);
               Console.WriteLine("Schema:" + parameters.schema);
               Console.WriteLine("Track:" + parameters.track);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in reading input arguments");
                WriteException(ex);
                return;
                
            }

            

            switch (parameters.process)
            {
                case "IMPORT":
                    {
                        try
                        {
                            Import.RunImport(parameters);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Error in Import");
                            WriteException(ex);
                        }
                        break;
                    }
                case "EXPORT":
                    {
                        try
                        {
                            Export.RunExport(parameters);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Error in EXPORT");
                            WriteException(ex);
                        }
                        break;
                    }
                default:
                    {

                        break;
                    }
            }

           
        }

        public static void WriteException(Exception ex)
        {
            Console.WriteLine("ERROR:");
            do
            {
                Console.WriteLine(ex.Message);
                ex = ex.InnerException;
            } while (ex != null);
        }

        public static EIParameters ReadArgs(string[] args)
        {
            EIParameters parameters = new EIParameters();
            foreach (string arg in args)
            {
                var split = arg.Split(new char[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length <= 1)
                {
                    throw new Exception("Invalid arguments passed.");
                }

                try
                {
                    EIHelper.ValidateEIParameters(split[0], split[1], ref parameters);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error in Reading Args");
                    WriteException(ex);
                }
                //if (ArgsHelper.ValidArgs[split[0].ToLower()] != null && split.Length >= 2)
                //{
                //    string argName = ArgsHelper.ValidArgs[split[0].ToLower()].ToString();
                //    string argValue = split[1];
                //    try
                //    {
                //        ArgsValueValidator(argName, argValue);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new Exception("Error while reading input arguments", ex);
                //    }
                //}
            }

            return parameters;
        }
    }
}
