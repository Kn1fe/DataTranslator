using Elements;
using System;
using System.Collections.Generic;

namespace DataTranslator
{
    public struct ElementTable
    {
        public string list;
        public int id;
        public List<Fields> f;
    }

    public struct Fields
    {
        public string field;
        public string value;
    }

    class ElementsData
    {
        public eListCollection LoadElements(string FilePath)
        {
            return new eListCollection(FilePath);
        }

        public List<ElementTable> CreateElTable(eListCollection e)
        {
            List<ElementTable> etl = new List<ElementTable>();
            for (int a = 0; a < e.Lists.Length; ++a)
            {
                for (int b = 0; b < e.Lists[a].elementValues.Length; ++b)
                {
                    ElementTable et = new ElementTable();
                    et.list = e.Lists[a].listName;
                    if (e.Lists[a].elementFields[0] == "ID")
                    {
                        et.id = Convert.ToInt32(e.GetValue(a, b, 0));
                    }
                    et.f = new List<Fields>();
                    for (int c = 0; c < e.Lists[a].elementFields.Length; ++c)
                    {
                        if (e.Lists[a].elementTypes[c].Contains("string"))
                        {
                            Fields f = new Fields()
                            {
                                field = e.Lists[a].elementFields[c],
                                value = e.GetValue(a, b, c)
                            };
                            if (!f.value.Contains(".dds") && !f.value.Contains(".ecm"))
                            {
                                et.f.Add(f);
                            }
                        }
                    }
                    etl.Add(et);
                }
            }
            return etl;
        }
    }
}
