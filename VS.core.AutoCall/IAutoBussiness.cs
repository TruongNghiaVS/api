namespace VS.core.AutoCall
{
    public interface IAutoBussiness
    {
        public Task<List<PhoneLog>> LoadData();
        public Task<bool> MakeCall();

        public Task<bool> TurnOffAutoCall(bool turnof);
        public Task<DurationTalkReponse> GetDurationAutocall(string noAgreeId);
        public Task<dynamic> GetInfomation(string lineCode);


        public Task<bool> SetChanel(int limitChange = 25);
        public Task<bool> SetLoadDataSip(bool loadSip);
    }
}
