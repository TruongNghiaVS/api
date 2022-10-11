namespace VS.core.API.Resource
{
    public class MessageInfomation
    {

        private static MessageInfomation? _privateIns;

        public MessageInfomation()
        {

        }
        public static MessageInfomation GetInstance()
        {

            if (_privateIns == null)
            {
                _privateIns = new MessageInfomation();
            }
            return _privateIns;
        }

        public string CommonError_NotFound = "Dữ liệu tìm không thấy";

        public string CommonError_ErrorRequestInput = "Dữ liệu tìm không thấy";
    }
}

