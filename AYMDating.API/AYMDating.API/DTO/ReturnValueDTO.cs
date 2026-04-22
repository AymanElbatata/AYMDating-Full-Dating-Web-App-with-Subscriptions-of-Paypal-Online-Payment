namespace AYMDating.API.DTO
{
    public static class EnumHelpers
    {

        //public static T GetEnumObjectByValue<T>(int valueId)
        //{
        //    return (T)Enum.ToObject(typeof(T), valueId);
        //}

    }
    public enum ReturnType : byte
    {
        Success,
        Failed
    }

    public class ReturnValueDTO
    {
        //public ReturnType Type { get; set; }
        public string? Type{ get; set; }
        public object? Data { get; set; }

        public ReturnValueDTO(ReturnType TypeValue, object Data)
        {
            //this.Type = Type;
            Type = EnumToString(TypeValue);
            this.Data = Data;
        }

        public static string EnumToString(ReturnType val)
        {
            switch (val)
            {
                case ReturnType.Success:
                    return "Success";
                case ReturnType.Failed:
                    return "Failed";
                default:
                    return "Unknown value";
            }
        }

    }
}
