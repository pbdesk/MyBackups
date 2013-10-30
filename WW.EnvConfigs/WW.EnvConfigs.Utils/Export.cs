using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using WW.EnvConfigs.DataModels;
using WW.EnvConfigs.DAL;

namespace WW.EnvConfigs.Utils
{
    public class Export
    {
        private static RepoHelper RepoHelper = new RepoHelper();

        public static void RunExport(EIParameters parameters)
        {
            try
            {
                ValidateExportArgs(parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid or insufficient parameters to call EXPORT process", ex);
            }



            List<Build> buildsToProcess = new List<Build>();
            List<Locale> localesToProcess = new List<Locale>();

            ////Handling Locales
            //if (parameters.LocaleObjs.Id == 0)
            //{
            //    localesToProcess = RepoHelper.Locales.GetAll<Locale>().ToList<Locale>();
            //}
            //else
            //{
            //    localesToProcess.Add(parameters.LocaleObjs);
            //}

            ////Handling Build
            //if (parameters.BuildObjs.Id == 0)
            //{
            //    buildsToProcess = RepoHelper.Builds.GetAll<Build>().ToList<Build>();
            //}
            //else
            //{
            //    buildsToProcess.Add(parameters.BuildObjs);

            //}

            foreach (var bld in parameters.BuildObjs)
            {
                foreach (var loc in parameters.LocaleObjs)
                {

                    RunExport2(parameters, loc,  bld);
                }
            }


        }

        public static void RunExport2(EIParameters parameters, Locale locale, Build build)
        {
            if (!parameters.dir.EndsWith("\\"))
            {
                parameters.dir += "\\";
            }
            
            Hashtable replaceItems = new Hashtable();

            replaceItems["${schema}"] = parameters.schema;
            replaceItems["${track}"] = parameters.track;
            replaceItems["${Track}"] = parameters.track;
            replaceItems["${domain}"] = locale.Domain;
            replaceItems["${oracle}"] = locale.OracleName;
            replaceItems["${country}"] = locale.ShortName;
            replaceItems["${lcid}"] = locale.LCID.ToString();
            replaceItems["${language}"] = locale.Language;
            replaceItems["${siteid}"] = locale.SiteId;
            replaceItems["${build}"] = build.Name;
            replaceItems["${Env}"] = build.Name;

            List<EnvValue> allValues = new List<EnvValue>();

            try
            {
                allValues = RepoHelper.EnvValues.Filter<EnvValue>(p => p.EnvKey.WWFrameworkId == parameters.FrameworkObj.Id && p.LocaleId ==  locale.Id && p.BuildId == build.Id && p.EnvKey.IsActive == true, null, "EnvKey").ToList<EnvValue>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading Values from database. " + ex.Message);
                return;
            }

            try
            {
                WriteXml(allValues, replaceItems, parameters.dir);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while writing xml file. " + ex.Message);
                
            }


        }

        public static void WriteXml(List<EnvValue> allValues, Hashtable replaceItems, string path)
        {
            string fileName = string.Format("environment.{0}.{1}.{2}.config", replaceItems["${country}"], replaceItems["${build}"], replaceItems["${track}"]);
            string nameAttr = string.Format("{0}_{1}_{2}_{3}", replaceItems["${country}"], replaceItems["${build}"], replaceItems["${schema}"], replaceItems["${track}"]);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(path + fileName, settings))
            {


                writer.WriteStartDocument();
                writer.WriteStartElement("properties");
                writer.WriteAttributeString("name", nameAttr);
                writer.WriteAttributeString("version", DateTime.Now.ToLongDateString());

                foreach (EnvValue v in allValues)
                {
                    writer.WriteStartElement("property");
                    writer.WriteAttributeString("name", v.EnvKey.KeyName);
                    writer.WriteAttributeString("value", Replacer(v.KeyValue, replaceItems));
                    if (v.EnvKey.IsProtected)
                    {
                        writer.WriteAttributeString("protected", "true");
                    }
                    if (!string.IsNullOrWhiteSpace(v.EnvKey.Protection))
                    {
                        writer.WriteAttributeString("protection", v.EnvKey.Protection);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public static string Replacer(string str, Hashtable replaceItems)
        {
            if (!String.IsNullOrWhiteSpace(str) && replaceItems != null && replaceItems.Count > 0)
            {
                foreach (DictionaryEntry item in replaceItems)
                {
                    //str = Regex.Replace(str, item.Key.ToString(), item.Value.ToString(), RegexOptions.IgnoreCase);
                    str = str.Replace(item.Key.ToString(), item.Value.ToString());
                }
            }
            return str;
        }

        public static void ValidateExportArgs(EIParameters parameters)
        {
            if(parameters == null)
            {
                throw new Exception("Parameters object is null");
            }
            if (string.IsNullOrWhiteSpace(parameters.dir))
            {
                throw new Exception("Error while validating import args. Invalid target directory.");
            }
            else
            {
                if(!Directory.Exists(parameters.dir))
                {
                    throw new Exception("Error while validating import args. Target directory does not exists.");
                }
            }

            if (parameters.LocaleObjs == null)
            {
                throw new Exception("Error while validating import args. Invalid Loale");
            }
            if (parameters.BuildObjs == null)
            {
                throw new Exception("Error while validating import args. Invalid Build");
            }
            if (string.IsNullOrWhiteSpace(parameters.schema))
            {
                throw new Exception("Error while validating import args. Invalid schema.");
            }
            if (string.IsNullOrWhiteSpace(parameters.track))
            {
                throw new Exception("Error while validating import args. Invalid track.");
            }
            if(parameters.FrameworkObj == null )
            {
                throw new Exception("Error while validating import args.  Framework object is null");
            }
            else
                if (parameters.FrameworkObj.Id == 0)
                {
                    throw new Exception("Error while validating import args.  Invalid Framework");
                }

        }
    }
}
