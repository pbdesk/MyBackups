using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WW.EnvConfigs.DataModels;
using WW.EnvConfigs.DAL;

namespace WW.EnvConfigs.Utils
{
    public class Import
    {
        private static RepoHelper RepoHelper = new RepoHelper();

        public static void RunImport(EIParameters parameters)
        {
            try
            {
                ValidateImportArgs(parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid or insufficient parameters to call IMPORT process", ex);
            }


            ImportKeyValues(parameters);

        }

        public static void ImportKeyValues(EIParameters parameters)
        {
            List<EnvProperty> list = ImportFile(parameters.file);
            ImportKeys(list, parameters);
            foreach(Build bld in parameters.BuildObjs)
            {
                foreach(Locale loc in parameters.LocaleObjs)
                {
                    ImportValues(list, loc, bld);
                }
            }
            

        }

        public static void ImportKeys(List<EnvProperty> properties, EIParameters parameters)
        {
            if (properties != null && properties.Count() > 0)
            {
                foreach (EnvProperty p in properties)
                {
                    EnvKey k = RepoHelper.EnvKeys.GetSingle<EnvKey>(q => string.Compare(q.KeyName, p.Name, true) == 0 && q.WWFrameworkId == parameters.frameworkId);
                    if (k == null)
                    {
                        CreateKey(p, parameters);
                    }
                    else
                    {
                        UpdateKey(p, k, parameters);
                    }

                }

                RepoHelper.SaveChanges();

            }
        }

        public static void ImportValues(List<EnvProperty> properties, Locale loc, Build bld)
        {
            if (properties != null && properties.Count() > 0)
            {
                foreach (EnvProperty p in properties)
                {
                    EnvKey k = RepoHelper.EnvKeys.GetSingle<EnvKey>(q => string.Compare(q.KeyName, p.Name, true) == 0);
                    if (k != null)
                    {
                        var vals = RepoHelper.EnvValues.Filter<EnvValue>(q => q.EnvKeyId == k.Id && q.LocaleId == loc.Id && q.BuildId == bld.Id);

                        if (vals != null && vals.Count() > 0)
                        {
                            foreach (EnvValue v in vals)
                            {
                                UpdateKeyValue(v, k, p, loc, bld);
                            }

                        }
                        else
                        {
                            throw new Exception("Key Values records not found for key " + p.Name);
                            //CreateKeyValue(k, p, loc, bld);
                        }

                    }
                }
                RepoHelper.SaveChanges();
            }
        }

        public static void CreateKey(EnvProperty p, EIParameters parameters)
        {
            if (p != null)
            {
                EnvKey k = new EnvKey();
                k.KeyName = p.Name;
                k.IsProtected = p.IsProtected;
                k.IsActive = true;
                k.CreateDate = DateTime.Today;
                k.Protection = p.Protection;
                k.WWFrameworkId = parameters.frameworkId;
                RepoHelper.InsertKeyWithBlankValues(k);
            }
        }

        public static void UpdateKey(EnvProperty p, EnvKey k, EIParameters parameters)
        {
            if (p != null && k != null)
            {
                k.IsProtected = p.IsProtected;
                k.IsActive = true;
                k.Protection = p.Protection;
                k.WWFrameworkId = parameters.frameworkId;
                RepoHelper.EnvKeys.UpdateLite<EnvKey>(k);
            }
        }

        public static void CreateKeyValue(EnvKey k, EnvProperty p, Locale loc, Build b)
        {
            if (k != null && p != null && loc != null && b != null)
            {
                List<Locale> allLocales = new List<Locale>();
                List<Build> allBuilds = new List<Build>();

                if (loc != null)
                {
                    if (loc.Id == 0)
                    {
                        allLocales = RepoHelper.Locales.GetAll<Locale>().ToList<Locale>();
                    }
                    else
                    {
                        allLocales.Add(loc);
                    }
                }

                if (b != null)
                {
                    if (b.Id == 0)
                    {
                        allBuilds = RepoHelper.Builds.GetAll<Build>().ToList<Build>();
                    }
                    else
                    {
                        allBuilds.Add(b);
                    }
                }

                foreach (Locale l in allLocales)
                {
                    foreach (Build bld in allBuilds)
                    {
                        EnvValue v = new EnvValue();
                        v.EnvKeyId = k.Id;
                        v.KeyValue = p.Value;
                        v.BuildId = bld.Id;
                        v.LocaleId = l.Id;
                        RepoHelper.EnvValues.InsertLite<EnvValue>(v);
                    }
                }

            }
        }

        public static void UpdateKeyValue(EnvValue v, EnvKey k, EnvProperty p, Locale loc, Build b)
        {
            if (v != null && k != null && p != null && loc != null && b != null)
            {
                if (loc.SiteId > 0)
                {
                    v.KeyValue = p.Value;
                    v.BuildId = b.Id;
                    v.LocaleId = loc.Id;
                    RepoHelper.EnvValues.UpdateLite<EnvValue>(v);
                }
            }


        }

        public static List<EnvProperty> ImportFile(string path)
        {
            List<EnvProperty> result = new List<EnvProperty>();
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                XDocument xDoc = LoadFile(path);
                if (xDoc != null)
                {
                    var items = from item in xDoc.Descendants("property")
                                let pName = item.Attribute("name")
                                let pValue = item.Attribute("value")
                                let pProtected = item.Attribute("protected")
                                let pProtection = item.Attribute("encryption")
                                where !string.IsNullOrWhiteSpace(pName.Value)
                                select new EnvProperty
                                {
                                    Name = pName.Value.Trim().ToLower(),
                                    Value = pValue != null ? pValue.Value.Trim() : string.Empty,
                                    IsProtected = pProtected != null ? Boolean.Parse(pProtected.Value) : false,
                                    Protection = pProtection != null ? pProtection.Value.ToUpper() : ""
                                };

                    result = items.ToList<EnvProperty>();
                }
            }

            return result;
        }

        private static XDocument LoadFile(string path)
        {
            XDocument result = null;
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                try
                {
                    result = XDocument.Load(path);
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }
            return result;
        }
        public static void ValidateImportArgs(EIParameters  parameters)
        {
            if (parameters == null)
            {
                throw new Exception("Parameters object is null");
            }

            if (string.IsNullOrWhiteSpace(parameters.file))
            {
                throw new Exception("Error while validating import args. Invalid import file.");
            }
            else
            {
                if(!File.Exists(parameters.file))
                {
                    throw new Exception("Error while validating import args. Import file does not exists");
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
            if(parameters.frameworkId==0)
            {
                throw new Exception("Error while validating import args. Invalid FrameworkId");
            }

        }
    }

    public class EnvProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsProtected { get; set; }
        public string Protection { get; set; }
    }
}
