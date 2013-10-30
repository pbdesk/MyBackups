using System;
using System.Linq;
using WW.EnvConfigs.DataModels;
using WW.EnvConfigs.DAL;
using System.Collections.Generic;

namespace WW.EnvConfigs.Utils
{
    public class EIHelper
    {
        private  static RepoHelper RepoHelper = new RepoHelper();

        static private string[] ValidSchemas = new string[] { "APP1", "APP2", "APPJ" };
        static private string[] ValidProcesses = new string[] { "IMPORT", "EXPORT" };

        public static void ValidateEIParameters(EIParameters parameters)
        {
            if (parameters != null)
            {
                ValidateEIParameters("process", parameters.process, ref parameters);
                ValidateEIParameters("dir", parameters.dir, ref parameters);
                ValidateEIParameters("file", parameters.file, ref parameters);
                ValidateEIParameters("schema", parameters.schema, ref parameters);
                ValidateEIParameters("track", parameters.track, ref parameters);
                ValidateEIParameters("db", parameters.db, ref parameters);
                ValidateEIParameters("locale", parameters.locale, ref parameters);
                ValidateEIParameters("build", parameters.build, ref parameters);
                ValidateEIParameters("frameworkid", parameters.frameworkId.ToString(), ref parameters);
            }
            else
            {
                throw new Exception("EIParameters is null.");
            }
        }
        public static void ValidateEIParameters(string argName, string argValue, ref EIParameters parameters)
        {

            if (!string.IsNullOrWhiteSpace(argName))
            {
                switch (argName)
                {
                    case "p":
                    case "process":
                        {
                            argValue = argValue.ToUpper();
                            if (!ValidProcesses.Contains(argValue))
                            {
                                throw new Exception("Invalid process name provided for parameter p(process)");
                            }
                            else
                            {
                                parameters.process = argValue;
                            }
                            break;
                        }
                    case "d":
                    case "dir":
                        {
                            if (string.IsNullOrWhiteSpace(argValue))
                            {
                                parameters.dir = string.Empty;
                            }
                            else
                            {
                                parameters.dir = argValue;
                            }


                            break;
                        }
                    case "f":
                    case "file":
                        {
                            if (string.IsNullOrWhiteSpace(argValue))
                            {
                                parameters.file = string.Empty;
                            }
                            else
                            {
                                parameters.file = argValue;
                            }

                            break;
                        }
                    case "l":
                    case "locale":
                        {
                            ValidateLocalesParameter(argValue, ref parameters);
                            //argValue = argValue.ToUpper();
                            //Locale locale = null;
                            //if (argValue != "ALL")
                            //{
                            //    try
                            //    {
                            //        locale =  RepoHelper.Locales.GetSingle<Locale>(p => string.Compare(p.ShortName, argValue, true) == 0);
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        throw new Exception("Error while reading Locale from database.", ex);
                            //    }
                            //    if (locale != null && locale.Id > 0)
                            //    {
                            //        parameters.locale = argValue;
                            //        parameters.LocaleObjs = locale;
                            //        //Console.WriteLine("Locale(s): " + argValue);
                            //    }
                            //    else
                            //    {
                            //        throw new Exception("Invalid Locale. May be not defined in database.");
                            //    }

                            //}
                            //else
                            //{
                            //    locale = new Locale() { Id = 0, ShortName = "ALL" };
                            //    parameters.LocaleObjs = locale;
                            //}
                            break;
                        }
                    case "build":
                    case "b":
                        {
                            ValidateBuildsParameter(argValue, ref parameters);
                            //argValue = argValue.ToUpper();
                            //Build build = null;
                            //if (argValue != "ALL")
                            //{
                            //    try
                            //    {
                            //        build = RepoHelper.Locales.GetSingle<Build>(p => string.Compare(p.Name, argValue, true) == 0);
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        throw new Exception("Error while reading Build from database.", ex);
                            //    }
                            //    if (build != null && build.Id > 0)
                            //    {
                            //        parameters.build = argValue;
                            //        parameters.BuildObjs = build;
                            //        //Console.WriteLine("Build(s): " + argValue);
                            //    }
                            //    else
                            //    {
                            //        throw new Exception("Invalid Build. May be not defined in database.");

                            //    }

                            //}
                            //else
                            //{
                            //    build = new Build() { Id = 0, Name = "ALL" };
                            //    parameters.BuildObjs = build;
                            //}
                            break;
                        }
                    case "schema":
                    case "s":
                        {
                            argValue = argValue.ToUpper();
                            if (!ValidSchemas.Contains(argValue))
                            {
                                throw new Exception("Invalid Schema name provided with parameter s(schema)");
                            }
                            else
                            {
                                parameters.schema = argValue;
                            }
                            break;
                        }
                    case "track":
                    case "t":
                        {
                            parameters.track = argValue;
                            break;
                        }
                    case "db":
                        {
                            parameters.db = argValue;
                            break;
                        }
                    case "frameworkid":
                    case "fwk":
                    case "fwkid":
                        {
                            int fwkId = 0;
                            WWFramework f = null;
                            if(int.TryParse(argValue, out fwkId ))
                            {
                                try
                                {
                                    f = RepoHelper.WWFrameworks.GetSingle<WWFramework>(fwkId);
                                }
                                catch(Exception ex)
                                {
                                    throw new Exception("Error while reading framework from database", ex);
                                }
                                if(f != null && f.Id > 0)
                                {
                                    parameters.frameworkId = fwkId;
                                    parameters.FrameworkObj = f;
                                }
                                else
                                {
                                    throw new Exception("Not a valid framework.");
                                }
                            }
                            else
                            {
                                throw new Exception("Not a valid Framework Id");
                            }
                            
                            break;
                        }
                    default:
                        {
                            throw new Exception("Invalid paramter passed.");
                        }

                }
            }

        }

        public static void ValidateLocalesParameter(string argValue, ref EIParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(argValue) && parameters != null)
            {
                argValue = argValue.ToUpper();
                var arrValues = argValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (arrValues != null && arrValues.Length > 0)
                {
                    arrValues = arrValues.Select(s => s.ToUpper()).ToArray();
                    
                    if(parameters.LocaleObjs == null)
                    {
                        parameters.LocaleObjs = new List<Locale>();
                    }
                    else
                    {
                        parameters.LocaleObjs.Clear();
                    }
                    if(arrValues[0] == "ALL")
                    {
                        var allLocales = RepoHelper.Locales.GetAll<Locale>().ToList<Locale>();
                        parameters.LocaleObjs.AddRange(allLocales);
                    }
                    else
                    {
                        var allLocales = RepoHelper.Locales.Filter<Locale>(p =>  arrValues.Contains(p.ShortName.ToUpper()) );
                        parameters.LocaleObjs.AddRange(allLocales);                        
                    }

                }              
              
            }
            else
            {
                throw new Exception("ArgsValue for Locales is null or Empty or EIParameters is null");
            }
        }

        public static void ValidateBuildsParameter(string argValue, ref EIParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(argValue) && parameters != null)
            {
                argValue = argValue.ToUpper();
                var arrValues = argValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (arrValues != null && arrValues.Length > 0)
                {
                    arrValues = arrValues.Select(s => s.ToUpper()).ToArray();

                    if (parameters.BuildObjs == null)
                    {
                        parameters.BuildObjs = new List<Build>();
                    }
                    else
                    {
                        parameters.LocaleObjs.Clear();
                    }
                    if (arrValues[0] == "ALL")
                    {
                        var allLocales = RepoHelper.Builds.GetAll<Build>().ToList<Build>();
                        parameters.BuildObjs.AddRange(allLocales);
                    }
                    else
                    {
                        var allLocales = RepoHelper.Builds.Filter<Build>(p => arrValues.Contains(p.Name.ToUpper())).ToList<Build>();
                        parameters.BuildObjs.AddRange(allLocales);
                    }

                }

            }
            else
            {
                throw new Exception("ArgsValue for Builds is null or Empty or EIParameters is null");
            }
        }
    }
}
