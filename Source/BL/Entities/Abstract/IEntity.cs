﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;

namespace BL.Entities
{
    public interface IEntity
    {
        int ID { get; set; }

        bool Deleted { get; set; }

        DateTime CreatedDate { get; set; }
        int? CreatedBy { get; set; }

        DateTime? LastModifiedDate { get; set; }
        int? LastModifiedBy { get; set; }
    }
}
