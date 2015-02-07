﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Common
{
    public static class MVCExtensions
    {
        public static SelectList ToSelectList<T>(this T enumObj
            , IList<T> skipValues = null
            , bool addChooseItem = false, string chooseItemName = "Choose...")
            where T : struct, IComparable, IFormattable, IConvertible
        {
            List<SelectListItem> collectionItems = new List<SelectListItem>();

            bool canConvertToInt = (Convert.ChangeType(enumObj, typeof(int)) != null);
            var values = Enum.GetValues(typeof(T)).Cast<T>();

            if (skipValues != null && skipValues.Count > 0)
            {
                values = values.Where(v => skipValues.Contains(v) == false);
            }

            foreach (var value in values)
            {
                collectionItems.Add(new SelectListItem()
                {
                    Text = (value as Enum).ToString(),
                    Value = (canConvertToInt == true) 
                        ? Convert.ChangeType(value, typeof(int)).ToString() 
                        : value.ToString()
                });
            }

            if (addChooseItem == true)
            {
                collectionItems.Insert(0, new SelectListItem()
                {
                    Text = chooseItemName,
                    Value = ""
                });
            }

            return new SelectList(collectionItems, "Value", "Text", enumObj);
        }
    }
}