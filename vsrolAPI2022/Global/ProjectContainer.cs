namespace VS.core.API.Global
{


    public class ProjectContainer
    {

        private static ProjectContainer _instance;



        public ProjectContainer()
        {


        }

        public static ProjectContainer GetProject()
        {
            if (_instance == null)
            {
                return new ProjectContainer();
            }
            return _instance;
        }

        public int GetCampagnByProject(int? idproject)
        {
            if (idproject == -1)
            {
                return -1;
            }
            if (idproject == 2)
            {
                return 1052;
            }

            if (idproject == 1)
            {
                return 1051;
            }
            return -1;
        }

    }
}
