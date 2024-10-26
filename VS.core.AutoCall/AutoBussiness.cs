namespace VS.core.AutoCall
{
    public class AutoBussiness : IAutoBussiness
    {
        private AutocallApi bussiness { get; set; }

        private DataAccess dataAccess;

        private readonly IServiceBussiness serviceBussiness;
        ModelData ModelData2;

        public AutoBussiness(IServiceBussiness _serviceBussiness)
        {
            bussiness = new AutocallApi();

            serviceBussiness = _serviceBussiness;
            dataAccess = new DataAccess();
            ModelData2 = ModelData.Instance;
        }
        public async Task<List<PhoneLog>> LoadData()
        {
            var listNew = await dataAccess.GetAllData();
            //listNew = new List<PhoneLog>()
            //{
            //    new PhoneLog()
            //    {
            //        NoAgreement ="abd",
            //        Id ="1273865",
            //        MobilePhone ="0383338840",
            //        Phone2 ="",
            //        Phone3 =""
            //    }
            //};
            foreach (var item in listNew)
            {
                ModelData2.DataCallQueue.Enqueue(item);
            }
            return listNew;
        }
        public async Task<bool> MakeCall()
        {
            if (ModelData2 == null)
            {
                ModelData2 = ModelData.Instance;
            }

            if (ModelData2.IsTurnOfAutocall == true)
            {
                return false;
            }
            if (ModelData2.DataCallQueue.Count < 1)
            {
                return false;
            }
            var listRun = new List<PhoneLog>();
            var indexRow = ModelData2.DataCallQueue.Count;
            if (indexRow > 20)
            {
                indexRow = 20;
            }
            for (int i = 0; i < indexRow; i++)
            {
                var itemIndex = ModelData2.DataCallQueue.Dequeue();
                listRun.Add(itemIndex);
            }
            foreach (var item in listRun)
            {
                ModelData2.DataCalled.Add(item);


                await serviceBussiness.CallNumber(item.MobilePhone);
            }
            return true;
        }

        public async Task<bool> TurnOffAutoCall(bool turnof)
        {

            ModelData2.IsTurnOfAutocall = turnof;
            return true;

        }


        public async Task<dynamic> GetInfomation(string lineCode)
        {
            var allResult = await serviceBussiness.GetStatus();
            var resultInfomation = allResult.Where(x => x.Sip == lineCode).FirstOrDefault();
            if (resultInfomation == null)
            {
                return "";
            }
            var itemCall = ModelData2.DataCalled.Where(x => x.MobilePhone == resultInfomation.Phone).FirstOrDefault();
            if (itemCall == null)
            {
                return "";
            }
            return "/follow-up/" + itemCall.Id;
        }

    }
}
