using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalScrumBoard.Business
{
    public enum TaskType
    {
        None = 0,
        UserStory = 1,
        SatisfactionConditions = 2,
        Development = 3,
        Test = 4,
        Block = 5
    }
}