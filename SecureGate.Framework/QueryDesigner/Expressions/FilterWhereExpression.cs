using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SecureGate.Framework.QueryDesigner.Expressions
{
    public class FilterWhereExpression
    {
        public FilterContainer SearchFilter(dynamic searchDTO, object dtoData, string orderbyPropertyName, OrderFilterType filterType)
        {
            var filter = new FilterContainer { };
            TreeFilter Maintree = new TreeFilter();
            Maintree.OperatorType = TreeFilterType.And;
            List<TreeFilter> treeFilters = new List<TreeFilter>();

            foreach (var prop in searchDTO.GetType().GetProperties())
            {
                if (prop.Name == "CurrentState" || prop.Name == "OrderNumber")
                {

                    var value = prop.GetValue(searchDTO, null);
                    if (prop.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                    {
                        string ff = "This prop's type is Ienumerable";
                    }
                    else if (value != null && value != "")
                    {
                        Type t = dtoData.GetType();
                        PropertyInfo propInfo = t.GetProperty(prop.Name);

                        if (prop.PropertyType == typeof(string))
                        {

                            string[] words = value.Split(',');

                            if (words.Length == 1)
                            {
                                propInfo.SetValue(dtoData, Convert.ChangeType(value, propInfo.PropertyType), null);
                                value = dtoData.GetType().GetProperty(prop.Name).GetValue(dtoData, null);

                                var name = prop.Name;
                                var type = prop.PropertyType;
                                TreeFilter tree = new TreeFilter();
                                tree.Field = name;
                                tree.FilterType = WhereFilterType.Equal;
                                tree.Value = value;
                                treeFilters.Add(tree);
                            }
                            else
                            {
                                TreeFilter SecMaintree = new TreeFilter();
                                SecMaintree.OperatorType = TreeFilterType.Or;
                                List<TreeFilter> sectreeFilters = new List<TreeFilter>();
                                foreach (var word in words)
                                {

                                    propInfo.SetValue(dtoData, Convert.ChangeType(word, propInfo.PropertyType), null);
                                    var splitValue = dtoData.GetType().GetProperty(prop.Name).GetValue(dtoData, null);

                                    TreeFilter sectree = new TreeFilter();
                                    sectree.Field = prop.Name;
                                    sectree.FilterType = WhereFilterType.Equal;
                                    sectree.Value = splitValue;
                                    sectreeFilters.Add(sectree);
                                }

                                SecMaintree.Operands = sectreeFilters;
                                treeFilters.Add(SecMaintree);
                            }

                        }
                        else
                        {
                            propInfo.SetValue(dtoData, Convert.ChangeType(value, propInfo.PropertyType), null);
                            value = dtoData.GetType().GetProperty(prop.Name).GetValue(dtoData, null);

                            var name = prop.Name;
                            var type = prop.PropertyType;
                            TreeFilter tree = new TreeFilter();
                            tree.Field = name;
                            tree.FilterType = WhereFilterType.Equal;
                            tree.Value = value;
                            treeFilters.Add(tree);
                        }

                    }
                }

            }

            Maintree.Operands = treeFilters;

            if (treeFilters.Count > 0)
            {
                filter.Where = Maintree;
            }
            //

            ///Order By...............
            List<OrderFilter> OrderBy = new List<OrderFilter>();
            OrderFilter orderBy = new OrderFilter();
            orderBy.Field = orderbyPropertyName;
            orderBy.Order = filterType;
            OrderBy.Add(orderBy);
            filter.OrderBy = OrderBy;


            return filter;

        }



    }
}
