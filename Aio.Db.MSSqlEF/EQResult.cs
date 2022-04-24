namespace Aio.Db.MSSqlEF
{ //executed query result
    public class EQResult
    {
        public bool SUCCESS { get; set; }
        public string MESSAGES { get; set; }
        public int ROWS { get; set; }
        public EQResult()
        {
            SUCCESS = true;
            MESSAGES = AppKeys.SUCCESS_MESSAGES;
            ROWS = 0;
        }
    }
}
