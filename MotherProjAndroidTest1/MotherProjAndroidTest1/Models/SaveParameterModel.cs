namespace Game.Models
{
    public class SaveParameterModel
    {
        public int id { get; set; }
        public string parameterName { get; set; }
        public string parameterValue { get; set; }
        public SaveParameterModel()
        {
            id = 0;
            parameterName = "-";
            parameterValue = "-";
        }
    }
}
