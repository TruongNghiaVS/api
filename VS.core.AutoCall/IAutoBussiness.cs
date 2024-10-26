namespace VS.core.AutoCall
{
    public interface IAutoBussiness
    {
        public Task<List<PhoneLog>> LoadData();
        public Task<bool> MakeCall();

        public Task<bool> TurnOffAutoCall(bool turnof);
        public Task<dynamic> GetInfomation(string lineCode);
    }
}
