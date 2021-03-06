using GenerateBackendService;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenerateBackendService
{
    class ItemProperty
    {
        public string itemName { get; set; }
        public List<Property>  listproperty{ get; set; }

        public ItemProperty(string _item, List<Property> _listproperty)
        {
            itemName = _item;
            listproperty = _listproperty;
        }

      

    }
}
