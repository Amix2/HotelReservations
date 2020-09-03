using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationsApp.Model.Validator
{
    public enum ResultType
    {
        NONE,
        SUCCESS,
        ALREADY_IN_DATABASE,
        WRONG_PARAMETER,
        NOT_IN_DATABASE
    }

    public class Result
    {
        public ResultType type;
        public string details;

        public Result(ResultType type, string details)
        {
            this.type = type;
            this.details = details;
        }

        public Result(ResultType type)
        {
            this.type = type;
            details = string.Empty;
        }
    }

    public static class ErrorLevelExtensions
    {
        public static string ToFriendlyString(this ResultType me)
        {
            switch (me)
            {
                case ResultType.NONE:
                    return "None";
                case ResultType.SUCCESS:
                    return "Success";
                case ResultType.ALREADY_IN_DATABASE:
                    return "Already in database";
                case ResultType.WRONG_PARAMETER:
                    return "Wrong parameter";
                case ResultType.NOT_IN_DATABASE:
                    return "Not in database";
                default:
                    return me.ToString();
            }
        }
    }
}
