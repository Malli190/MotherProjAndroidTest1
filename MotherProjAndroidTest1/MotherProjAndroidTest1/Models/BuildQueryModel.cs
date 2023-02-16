namespace Game.Models
{
    public class BuildQueryModel
    {
        public int query_id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public int progress { get; set; }

        public GameBuildTypeModel gameModel { get; set; }
        public BuildQueryModel()
        {
            query_id = 0;
            name = "-";
            progress = 0;

            gameModel = new GameBuildTypeModel();
        }
    }
}
